using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("子彈攻擊力"), Range(10, 100)]
    public float atk = 50;


    /// <summary>
    /// 碰撞事件 : 兩者都沒有勾選 is Trigger 才可使用
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //如果碰撞物件有<Enemy>的腳本
        if (collision.gameObject.GetComponent<Enemy>())
        {
          //對 Enemy呼叫 hit(atk)方法       
          collision.gameObject.GetComponent<Enemy>().hit(atk);
        }
    }


}
