using System.Collections;
using UnityEngine;

public class Car : MonoBehaviour
{
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



}
