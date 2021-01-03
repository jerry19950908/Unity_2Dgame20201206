using System.Collections;
using UnityEngine;

public class API : MonoBehaviour
{

    //修飾詞 類別 欄位;
    public Transform Tra;
    public Transform tester;

    public Camera cam;

    public SpriteRenderer testerspr;
    public GameObject tester2;


    private void Start()
    {
        //非靜態屬性
        //取得: 欄位.屬性
        print("座標" + Tra.position);

        //設定: 欄位.屬性 = 值
        tester.position = new Vector3(2, 0, 0);


        //靜態屬性
        print("攝影機數量:" + Camera.allCamerasCount);
        //非靜態屬性(要先設定欄位)
        cam.backgroundColor = new Color(0.5f, 0.2f, 0.3f);


        //練習
        print("圖片顏色:" + testerspr.color);
        print("圖片圖層:" + tester2.layer);
        testerspr.flipX = true;
        tester2.layer = 5; 
    }


    private void Update()
    {
        //非靜態方法
        //欄位.方法(對應參數)
        //變形欄位.旋轉(x,y,z)
        tester.Rotate(0, 0, 30);

        //位移(x,y,z,空間)
        //空間有兩種: World世界, Self區域
        tester.Translate(0.5f, 0, 0, Space.World);

    }






}
