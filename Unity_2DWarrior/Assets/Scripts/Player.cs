using System.Collections;
using UnityEngine;

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


    [Header("音效")]
    [Tooltip("開槍音效")]
    public AudioClip bulletsound;
    private AudioSource aud;

    //角色元件
    private Rigidbody2D rig;
    private Animator ani;
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
    }



    private void Update()
    {
        GetHorizontal();
        Move();
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
    }

    /// <summary>
    /// 跳躍方法
    /// </summary>
    private void Jump()
    {

    }

    /// <summary>
    /// 開槍方法
    /// </summary>
    private void Fire()
    {

    }

    /// <summary>
    /// 受傷方法
    /// </summary>
    /// <param name="getdamage">受到傷害值</param>
    private void Damage(float getdamage)
    {

    }

    /// <summary>
    /// 死亡方法
    /// </summary>
    private void Dead()
    {

    }
    #endregion



}
