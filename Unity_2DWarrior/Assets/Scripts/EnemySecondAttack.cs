using System.Collections;
using UnityEngine;

public class EnemySecondAttack : MonoBehaviour
{

    [Header("第二階段攻擊力")]
    public float atk = 20;

    // 事件:粒子碰撞事件
    // 粒子必須勾選 collision 與 send message
    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<Player>())
        {
            other.GetComponent<Player>().Damage(atk);
        }
    }




}
