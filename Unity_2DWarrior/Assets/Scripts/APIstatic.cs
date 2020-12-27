using System.Collections;
using UnityEngine;

/// <summary>
/// 認識API Static
/// </summary>
public class APIstatic : MonoBehaviour
{
    private void Start()
    {
        #region 靜態屬性存取
        //取得屬性(欄位)
        //靜態API 類別名稱.靜態屬性;  EX: Random.value, Mathf.PI
        print("隨機值:" + Random.value);
        print("拍:" + Mathf.PI);

        //設定屬性(欄位)
        //類別名稱.靜態屬性 = 值;

        //若出現(Read only),就是唯一值不能指定數值
        //ex: Time.deltatime = 0.5f , 是錯誤的

        //指標.可看見 = 看不見
        Cursor.visible = false;

        //時間.尺寸 = 2倍速
        Time.timeScale = 2;


        print("攝影機數量:" + Camera.allCamerasCount);

        Physics2D.gravity = new Vector2(0,-20f);

        print("顯示重力大小:"+ Physics2D.gravity);
        #endregion



        #region 靜態方法存取

        //靜態方法
        //類別名稱.靜態方法(對應參數)
        print("取得兩個數字隨機值:" + Random.Range(100, 500));

        int Number = Mathf.Abs(-199);
        print("取得絕對值:" + Number);


        print("9.99去掉小數點的值:" + Mathf.Floor(9.99f));


        float pos = Vector3.Distance(new Vector3(1, 1, 1), new Vector3(22, 22, 22));
        print("兩點之間的距離:" + pos);

        Application.OpenURL("https://unity.com/");


        #endregion
    }


    private void Update()
    {
        print("是否輸入任意鍵" + Input.anyKeyDown);

        print("遊戲經過的時間" + Time.time);

        print("是否輸入空白鍵" + Input.GetKeyDown(KeyCode.Space));
    }
}
