using System.Collections;  //引用 系統.集合 API - 協同程序
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


//第一次套用腳本時執行
//添加元件(類型(元件), 類型(元件)......)
[RequireComponent(typeof(AudioSource), typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class Enemy : MonoBehaviour
{
    [Header("移動速度"), Range(0, 1000)]
    public float speed = 3;
    [Header("攻擊距離"), Range(0, 100)]
    public float atkrange = 10;
    [Header("攻擊力"), Range(0, 1000)]
    public float atk = 10;
    [Header("攻擊冷卻時間"), Range(0, 10)]
    public float atkCD = 3;
    [Header("攻擊範圍位移")]
    public Vector3 offsetAtk;
    [Header("攻擊範圍大小")]
    public Vector3 sizeAtk;
    [Header("攻擊延遲傳送傷害給玩家的時間"), Range(0, 3f)]
    public float atkdelay = 0.7f;
    [Header("血量"), Range(0, 5000)]
    public float hp;
    [Header("血量文字")]
    public Text texthp;
    [Header("血量圖片")]
    public Image imghp;
    [Header("死亡事件")]
    public UnityEvent ondead;

    private AudioSource aud;
    private Animator ani;
    private Rigidbody2D rig;
    private Player player;
    private CameraControl2D cam;
    private float hpmax;  //最大血量
    private float timer;  //計時器
    private bool Issecond; //是否發動第二次攻擊
    private ParticleSystem psSecond;


    private void Start()
    {
        aud = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();
        rig = GetComponent <Rigidbody2D>();
        hpmax = hp;
        player = FindObjectOfType<Player>();  // 透過類型尋找物件<類型>()  - 不能有重複物件
        cam = FindObjectOfType<CameraControl2D>();
        psSecond = GameObject.Find("魔王第二階段攻擊特效").GetComponent<ParticleSystem>();

    }

    private void Update()
    {
        if (ani.GetBool("死亡開關")) return;  //死亡時, 就自動跳出Update
       
        move();
    }


    /// <summary>
    /// 繪製魔王攻擊範圍
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);  //綠色
        Gizmos.DrawCube(transform.position + transform.right * offsetAtk.x + transform.up * offsetAtk.y, sizeAtk);  //繪製正方體(魔王攻擊中心點 + 尺寸);

        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, atkrange);
    }





    /// <summary>
    /// 受傷方法
    /// </summary>
    /// <param name="damage">接受傷害值</param>
    public void hit(float damage)
    {
        hp -= damage;                   //魔王血量遞減 
        ani.SetTrigger("受傷觸發");      //受傷動畫
        texthp.text = hp.ToString();    //血量文字.文字內容 = 血量.轉為字串();
        imghp.fillAmount = hp / hpmax;  //血量圖片.填滿長度 = 目前血量 / 最大血量;

        if (hp <= hpmax * 0.8f)
        {
            Issecond = true; //進入第二次攻擊
            atkrange = 20;  //血量低於八成, 進入攻擊第二階段
        }
        if (hp <= 0) dead();  //如果 血量 <= 0 執行死亡方法;
    }

   /// <summary>
   /// 死亡方法
   /// </summary>
    public void dead()
    {
        ondead.Invoke();   //觸發死亡事件

        hp = 0;
        texthp.text = 0.ToString();
        ani.SetBool("死亡開關", true);
        GetComponent<CapsuleCollider2D>().enabled = false;  //取得元件<膠囊碰撞器>().啟動 = 關閉
        rig.Sleep();                                        //剛體.睡著();
        rig.constraints = RigidbodyConstraints2D.FreezeAll;  //剛體.約束 = 約束.全部凍結(角度)
    }

    /// <summary>
    /// 追蹤玩家與面向玩家
    /// </summary>
    public void move()
    {
        //如果 動畫是 魔王攻擊 或者是 魔王受傷動畫 就跳出移動狀態
        // 區域變數 = 動畫.取的目前動畫控制器圖層編號(編號)
        AnimatorStateInfo info = ani.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("魔王_攻擊動畫") || info.IsName("魔王_受傷動畫")) return;

        /**  判斷式寫法
        if (transform.position.x > player.transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        */


        //三元運算子 - 布林值 ? 結果 1 : 2 ;
        // y = 本身座標的 x 大於 玩家座標的 x ?  是的話 y軸 = 180 : 不是的話 y = 0;
        float y = transform.position.x > player.transform.position.x ? 180 : 0;
        transform.eulerAngles = new Vector3(0, y, 0);

        //區域變數 = 二維.距離(A座標, B座標);
        float dis = Vector2.Distance(transform.position, player.transform.position);

        if (dis > atkrange)
        {
            //剛體.移動座標(座標 + 前方 * 一偵 * 移動速度)
            rig.MovePosition(transform.position + transform.right * Time.deltaTime * speed);
        }
        else
        {
            attack();
        }

        //動畫.設定布林值("走路開關" , 剛體.加速度.值 > 0);
        ani.SetBool("走路開關", rig.velocity.magnitude > 0);


    }

    /// <summary>
    /// 攻擊冷卻與攻擊行為
    /// </summary>
    public void attack()
    {
        rig.velocity = Vector3.zero;  //剛體.加速度 = 0 (當進入攻擊範圍時,加速度 = 0)
      
        if (timer < atkCD)             //計時器 小於 攻擊冷卻時間
        {
            timer += Time.deltaTime;   //計時器累加
        }
        else
        {
            ani.SetTrigger("攻擊觸發"); //觸發攻擊動畫
            timer = 0;                 //計時器歸零

            StartCoroutine(delaySendDamage());  //啟動協同程序(協程名稱());
        } 

    }



    // IEnumerator 允許傳回時間
    /// <summary>
    /// 延遲傳送傷害
    /// </summary>
    private IEnumerator delaySendDamage()
    {
        //等待延遲時間
        yield return new WaitForSeconds(atkdelay);

        //碰撞物件 = 2D物理.盒形覆蓋(中心點, 尺寸, 角度, 圖層);
        Collider2D hit = Physics2D.OverlapBox(transform.position + transform.right * offsetAtk.x + transform.up * offsetAtk.y, sizeAtk, 0, 1 << 9);

        if (hit) player.Damage(atk);
        StartCoroutine(cam.shakeCamera());  //啟動協程(攝影機晃動效果)

        //如果是第二階段 就播放第二次攻擊特效
        if (Issecond) psSecond.Play();
       
    }



}
