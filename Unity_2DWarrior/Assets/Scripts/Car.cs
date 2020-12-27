using System.Collections;
using UnityEngine;

public class Car : MonoBehaviour
{
    #region 練習欄位
    //註解
    //欄位語法 
    //修飾詞 類型 名稱;
    //公開 public  私人 private
    //指定符號 =  

    //欄位屬性
    //屬性語法，屬性名稱[("字串或對應的值")]，要寫在欄位上方
    //標題
    //提示
    //範圍
    [Header("高度"), Range(2, 5)]  
    [Tooltip("汽車高度")] 
    public int height = 3;

    [Header("重量，噸"), Range(3F, 10F)]  
    [Tooltip("汽車重量")]
    public float weight = 5.5f;

    [Header("品牌")]
    [Tooltip("汽車品牌")]
    public string brand = "BMW";

    [Header("是否有天窗")]
    [Tooltip("汽車是否有天窗")]
    public bool haswindow = true;


    //其他類型
    //顏色

    public Color mycolor1; //命名
    public Color red = Color.red; //指定顏色
    public Color mycolor2 = new Color(0.2f, 0.5f, 0.8f);  //光三原色(R,G,B)
    public Color mycolor3 = new Color(0.2f, 0.5f, 0.8f, 0.5f); //光三原色(R,G,B,A)  A = 透明度 

    //座標 向量 2維 - 4維
    public Vector2 V2 = Vector2.zero; 
    public Vector2 V21= Vector2.one;
    public Vector2 V22 = new Vector2(9, 10);  
    public Vector3 V3 = new Vector3(5.5f, 6.3f, 8.8f);
    public Vector4 v4 = new Vector4(2.8f, 3.5f, 4.7f, 5.3f);

   
    //圖片與音效
    public Sprite sprite;
    public AudioClip aud;

    
    //遊戲物件與元件
    //遊戲物件:可存放階層面板的物件與預製物
    public GameObject oba;
    public GameObject oba2;

    //元件:屬性面板上可摺疊的元件
    public Transform tra;
    public Camera cam;
    public Rigidbody rig;
    #endregion

    #region 練習方法

    //方法語法
    //修飾詞 傳回類型 名稱(參數類型 參數名稱, 參數類型 參數名稱.....) { 程式區塊，演算法 }
    // void 無傳回類型 : 呼叫此方法後不會傳回任何資料
    private void Test()
    {
        print("哈囉~~");
    }

    //有傳回類型 = 資料
    //{ 必須傳回相同資料類型 }
    private int Ten()
    {
        return 10;
    }

    private float onepointfive()
    {
        return 1.5f;
    }

    private string Name()
    {
        return "Jerry";
    }
    /// <summary>
    /// 開車方法
    /// </summary>
    /// <param name="diretion">方向</param>
    /// <param name="sound">音效</param>
    /// <param name="speed">速度</param>
    private void Drive(string diretion, string sound, string speed)
    {
        print("開車方向:" + diretion);
        print("開車音效:" + sound);
        print("開車速度:" + speed);
    }

    /// <summary>
    /// 開雨刷方法
    /// </summary>
    /// <param name="speed">開雨刷速度</param>
    /// 參數預設值 : 選填式參數，一定要填在參數最右方
    /// 若有兩個預設參數值, 則指定方法的預設參數 參數名稱 : 值
    private void openbrush(string sound, int count = 2, int speed = 50)
    {
        print("開啟雨刷");
        print("雨刷音效:" + sound);
        print("雨刷的數量" + count);
        print("雨刷的速度:" + speed);
    }
    #endregion


    //事件:在特定時間會執行的方法
    private void Start()
    {
        //取得欄位
        print("品牌:" + brand);
        print("高度:" + height);
        //設定欄位
        haswindow = true;
        height = 5;
       
        Test();


        //傳回方法使用方式
        //1.直接當作傳回類型資料使用
        print("整數傳回:" + Ten());
        print("浮點數傳回:" + onepointfive());

        //2. 儲存在區域(宣告)變數裡面，只能在此方法(事件)括號內使用
        string myname = Name();
        print(myname);


        //呼叫時輸入方法參數
        Drive("後方", "噗噗噗", "70公里");
        Drive("前方", "噗噗噗", "60公里");
        Drive("左方", "噗噗噗", "50公里");
        Drive("右方", "噗噗噗", "80公里");


        //指定方法的預設參數 參數名稱 : 值
        openbrush("唰唰唰", speed: 500);

    }

}
