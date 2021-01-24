using System.Collections;
using UnityEngine;
using UnityEngine.UI;


//第一次套用腳本時執行
//添加元件(類型(元件), 類型(元件)......)
[RequireComponent(typeof(AudioSource), typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class Enemy : MonoBehaviour
{
    [Header("移動速度"), Range(0, 1000)]
    public float speed = 3;
    [Header("攻擊"), Range(0, 100)]
    public float atkrange = 10;
    [Header("攻擊力"), Range(0, 1000)]
    public float atk = 10;
    [Header("血量"), Range(0, 5000)]
    public float hp;
    [Header("血量文字")]
    public Text texthp;
    [Header("血量圖片")]
    public Image imghp;

    private AudioSource aud;
    private Animator ani;
    private Rigidbody2D rig;
    private float hpmax;  //最大血量
    private Player player;


    private void Start()
    {
        aud = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();
        rig = GetComponent <Rigidbody2D>();
        hpmax = hp;
        player = FindObjectOfType<Player>();  // 透過類型尋找物件<類型>()  - 不能有重複物件
    }

    private void Update()
    {
        move();
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

        if (hp <= 0) dead();  //如果 血量 <= 0 執行死亡方法;
    }

   /// <summary>
   /// 死亡方法
   /// </summary>
    public void dead()
    {
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

        //動畫.設定布林值("走路開關" , 剛體.加速度.值> 0);
        ani.SetBool("走路開關", rig.velocity.magnitude > 0);


    }

    /// <summary>
    /// 攻擊冷卻與攻擊行為
    /// </summary>
    public void attack()
    {
        rig.velocity = Vector3.zero;  //剛體.加速度 = 0 (當進入攻擊範圍時,加速度 = 0)
        ani.SetTrigger("攻擊觸發");

    }






}
