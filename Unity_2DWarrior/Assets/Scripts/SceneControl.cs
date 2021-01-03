using System.Collections;
using UnityEngine.SceneManagement; //引用場景管理API
using UnityEngine;

public class SceneControl : MonoBehaviour
{
    //按鈕要呼叫方法，需把方法公開 public

  public void Startgame()
    { 
        //要先把製作好的場景放在 file - buildsettings...裡面
        //場景管理器.載入場景("遊戲場景");
        SceneManager.LoadScene("遊戲場景");
    }

  public void BacktoMenu()
    {
        //場景管理器.載入場景("選單");
        SceneManager.LoadScene("選單");
    }


    /// <summary>
    /// 離開遊戲方法
    /// </summary>
    public void QuitGame()
    {
        //應用程式.離開();
        Application.Quit();
    }
}
