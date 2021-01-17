using System.Collections;
using UnityEngine;


//第一次套用腳本時執行
//添加元件(類型(元件), 類型(元件)......)
[RequireComponent(typeof(AudioSource), typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class Enemy : MonoBehaviour
{
    [Header("移動速度"), Range(0, 1000)]
    public float speed = 10;
    [Header("攻擊"), Range(0, 100)]
    public float atkrange = 10;
    [Header("攻擊力"), Range(0, 1000)]
    public float atk = 10;
    [Header("血量"), Range(0, 5000)]
    public float hp = 2500;


    private AudioSource aud;
    private Animator ani;
    private Rigidbody2D rig;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();
        rig = GetComponent <Rigidbody2D>();
        
    }

    /// <summary>
    /// 受傷方法
    /// </summary>
    /// <param name="damage">接受傷害值</param>
    public void hit(float damage)
    {
        hp -= damage;                   //魔王血量遞減 
        ani.SetTrigger("受傷觸發");      //受傷動畫
    }

   











}
