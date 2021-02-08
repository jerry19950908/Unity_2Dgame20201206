using System.Collections;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [Header("要傳送的另一個傳送門")]
    public Transform otherteleport;

    private bool playerIn;
    private Transform player;


    /// <summary>
    /// 傳送方法
    /// </summary>
    private void Teleport()
    {
        //若戰士進入傳送區域 並且 按下 w鍵
        if (playerIn && Input.GetKeyDown(KeyCode.W))
        {
            //玩家座標 = 另一個傳送門的座標 * 三維向量.上方 * 值
            player.position = otherteleport.position + Vector3.up * 1.5f;
        }
    }


    private void Awake()
    {
        player = GameObject.Find("戰士").transform;
    }


    private void Update()
    {
        Teleport();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "戰士") playerIn = true;
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "戰士") playerIn = false;
    }

}
