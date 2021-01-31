using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private Player player;


    //會在Start事件之前執行一次,比Start還早
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    /// <summary>
    /// 介面啟動時 暫停遊戲
    /// </summary>
    public void Pausegame()
    {
        Time.timeScale = 0;    //時間暫停
        player.enabled = false;
    }

    /// <summary>
    /// 介面關閉時 繼續遊戲
    /// </summary>
    public void Restartgame()
    {
        Time.timeScale = 1;   //時間開始
        player.enabled = true;
    }

}
