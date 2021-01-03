using System.Collections;
using UnityEngine.SceneManagement; //引用場景管理API
using UnityEngine;

public class SceneControl : MonoBehaviour
{
    [Header("音效來源")]
    public AudioSource aud;
    [Header("按鈕音效")]
    public AudioClip soundclick;


    
    /// <summary>
    /// 開始遊戲方法
    /// </summary>
    //按鈕要呼叫方法，需把方法公開 public
  public void Startgame()
    {
        //音效.播放一次(音效來源, 音量大小);
        aud.PlayOneShot(soundclick, 1.5f);

        Invoke("Delaystartgame", 1);
     
    }


    public void Delaystartgame()
    {
        //要先把製作好的場景放在 file - buildsettings...裡面
        //場景管理器.載入場景("遊戲場景");
        SceneManager.LoadScene("遊戲場景");
    }


  /// <summary>
  /// 返回選單方法
  /// </summary>
  public void BacktoMenu()
    {
        //音效.播放一次(音效來源, 音量大小);
        aud.PlayOneShot(soundclick, 1.5f);

        Invoke("DelayBacktoMenu", 1);

    }

    public void DelayBacktoMenu()
    {
        //場景管理器.載入場景("選單");
        SceneManager.LoadScene("選單");
    }


    /// <summary>
    /// 離開遊戲方法
    /// </summary>
    public void QuitGame()
    {
        //音效.播放一次(音效來源, 音量大小);
        aud.PlayOneShot(soundclick, 1.5f);

        Invoke("DelayQuitGame", 1);
    }

    public void DelayqQuitGame()
    {
        //應用程式.離開();
        Application.Quit();
    }
}
