using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region 欄位
    [Header("角色血量"), Range(0, 200)]
    public float hp = 100;

    [Header("角色移動速度"), Range(0, 1000f)]
    public float speed = 10.5f;

    [Header("跳越高度"), Range(0, 3000)]
    public int height = 100;

    [Header("是否在地板上")]
    [Tooltip("是否跳躍在空中")]
    public bool isGround ;

    [Header("子彈")]
    [Tooltip("子彈")]
    public GameObject bullet;

    [Header("子彈生成點")]
    [Tooltip("子彈生成的位置")]
    public Transform bulletpos;

    [Header("子彈速度"), Range(0, 5000)]
    public int bulletspeed = 800;

    [Header("子彈傷害"), Range(0, 5000)]
    public int bulletdamage = 50;


    [Header("地面判定位移")]
    public Vector3 offset;
    [Header("地面判定半徑")]
    public float radius = 0.3f;

    [Header("音效")]
    [Tooltip("開槍音效")]
    public AudioClip bulletsound;
    [Tooltip("撿鑰匙音效")]
    public AudioClip keysound;



    //角色元件
    private Rigidbody2D rig;
    private Animator ani;
    private AudioSource aud;
    private SpriteRenderer spr;
    #endregion



    /// <summary>
    /// 取得玩家水平軸向的值
    /// </summary>
    public float h;

    private void Start()
    {
        //欄位 = 取得元件<泛型>();
        //泛型 : 泛指所有類型
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        hpmax = hp;
        spr = GetComponent<SpriteRenderer>();
    }  
    private void Update()
    {
        GetHorizontal();
        Move();
        Jump();
        Fire();
    }
    
    /// <summary>
    /// 觸發事件 : Enter 進入時執行一次
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //如果(碰撞物件.標籤 = 鑰匙)
        if (collision.tag == "鑰匙")
        {
            //刪除(碰撞物件)
            Destroy(collision.gameObject);
            aud.PlayOneShot(keysound, Random.Range(1.2f, 1.5f));
        }
    }

    //在Unity內繪製圖示
    private void OnDrawGizmos()
    {
        //圖示.顏色 = 顏色
        Gizmos.color = new Color(1, 0, 0, 0.6f);
        //圖示.繪製球體(中心點(玩家位置), 半徑)
        Gizmos.DrawSphere(transform.position + offset, radius);
    }


    /// <summary>
    /// 取得水平軸向方法
    /// </summary>
    private void GetHorizontal()
    {
        //輸入.取得軸向("水平")
        h = Input.GetAxis("Horizontal");
    }
    
    
    #region 方法

    /// <summary>
    /// 移動方法
    /// </summary>
    private void Move()
    {
      
      //剛體.加速度 = 二維向量 ( 水平 * 速度, 原本加速度的y軸方向);
        rig.velocity = new Vector2(h * speed, rig.velocity.y);

       //如果玩家 按下 D鍵或者右鍵 就執行 {內容}
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            // 此 transform 指的是此腳本同一層的 transform元件，所以不需要命名欄位
            // 物件的Rotation 在程式裡是 localEulerAngles
            transform.localEulerAngles = Vector3.zero;
        }


        //如果玩家 按下 A鍵或者左鍵 就執行 {內容}
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.localEulerAngles = new Vector3(0, 180 , 0);
        }

        //動畫控制器.設定布林值(參數， 布林值)
        //當玩家按下左右鍵時，會起到跑步動畫(h不等於0)
        ani.SetBool("跑步開關", h != 0);


    }

    /// <summary>
    /// 跳躍方法
    /// </summary>
    private void Jump()
    {
        //如果 在地面上 並且 按下空白鍵 才可以執行跳躍動畫
        if (isGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            rig.AddForce(new Vector2(0, height));  // 剛體.添加推力(二維向量);
            isGround = false;                      // 不再地面上;

            ani.SetTrigger("跳躍觸發");
        }

        //碰撞物件 = 2D物理.覆蓋圓形(中心點(玩家位置), 半徑 , 圖層)                   1 << 圖層編號
        Collider2D hit = Physics2D.OverlapCircle(transform.position + offset, radius, 1 << 8);


        //如果 碰到物件 是存在的 就把 是否在地面上 設定為 true
        if (hit)
        {
            isGround = true;  //在地面上
        }
        //否則 沒有碰到物件 就把 是否在地面上 設定為 false
        else
        {
            isGround = false;  //不在地面上
        }

        ani.SetFloat("跳躍數值", rig.velocity.y);  //動畫控制器.設定浮點數(參數,數值)
        ani.SetBool("是否在地面上", isGround);

    }

    /// <summary>
    /// 開槍方法
    /// </summary>
    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            aud.PlayOneShot(bulletsound, Random.Range(1.2f, 1.5f));
            //區域變數 = 生成(物件, 座標, 角度)
            GameObject temp =  Instantiate(bullet, bulletpos.position, bulletpos.rotation);
            //變數.取得元件(剛體).添加推力(生成點右邊 * 速度 + 生成點上方 * 高度)
            temp.GetComponent<Rigidbody2D>().AddForce(bulletpos.right * bulletspeed + bulletpos.up * 150);

            temp.AddComponent<Bullet>().atk = bulletdamage;
        }
    }

    [Header("血量文字")]
    public Text texthp;
    [Header("血量圖片")]
    public Image imghp;
    private float hpmax;  //最大血量


    /// <summary>
    /// 受傷方法
    /// </summary>
    /// <param name="getdamage">受到傷害值</param>
    public void Damage(float getdamage)
    {
        hp -= getdamage;                   //魔王血量遞減 
        ani.SetTrigger("受傷觸發");      //受傷動畫
        texthp.text = hp.ToString();    //血量文字.文字內容 = 血量.轉為字串();
        imghp.fillAmount = hp / hpmax;  //血量圖片.填滿長度 = 目前血量 / 最大血量;

        StartCoroutine(DamageEffect()); //啟動受傷效果協程

        if (hp <= 0) Dead();  //如果 血量 <= 0 執行死亡方法;
    }

    /// <summary>
    /// 受傷效果
    /// </summary>
    /// <returns></returns>
    private IEnumerator DamageEffect()
    {
        Color red = new Color(1, 0.1f, 0.1f);   //受傷顏色(紅)
        float interval = 0.05f;

        for (int i = 0; i < 5; i++)
        {
            spr.color = red;                        //指定紅色 
            yield return new WaitForSeconds(interval);  //等待
            spr.color = Color.white;                //恢復白色
            yield return new WaitForSeconds(interval);  //等待
        }
   
    }


    /// <summary>
    /// 死亡方法
    /// </summary>
    private void Dead()
    {
        hp = 0;
        texthp.text = 0.ToString();
        ani.SetBool("死亡開關", true);
        enabled = false;  //死亡時,玩家腳本關掉
        rig.Sleep();
        transform.Find("槍").gameObject.SetActive(false); //死亡時, 槍消失
    }
    #endregion



}
