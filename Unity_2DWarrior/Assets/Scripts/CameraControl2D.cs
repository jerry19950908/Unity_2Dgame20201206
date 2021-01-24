using System.Collections;
using UnityEngine;

public class CameraControl2D : MonoBehaviour
{
    [Header("目標物件")]
    public Transform target;
    [Header("追蹤速度")]
    public float speed = 3.5f;
    [Header("晃動間隔"), Range(0, 1)]
    public float shakeinterval = 0.05f;
    [Header("晃動值"), Range(0, 5)]
    public float shakevalue = 0.5f;
    [Header("晃動次數"), Range(0, 10)]
    public int shakeCount = 3;

    /// <summary>
    /// 追蹤目標物件
    /// </summary>
    private void Track()
    {
        Vector3 posA = target.position;       //取得玩家座標
        Vector3 posB = transform.position;    //取得攝影機座標
        posA.z = -10;

        posB = Vector3.Lerp(posB, posA, 0.5F * speed * Time.deltaTime);  //攝影機更新座標 = 三維向量.插值(攝影機座標, 玩家座標 , 百分比) 
        transform.position = posB;                                       //更新攝影機座標 = 會不斷取的插值並靠近玩家座標,達到追蹤效果
    }
    

    //延遲更新: 在 Update執行後才執行
    private void LateUpdate()
    {
        Track();
    }

    /// <summary>
    /// 攻擊時晃動攝影機
    /// </summary>
    /// <returns></returns>
    public IEnumerator shakeCamera()
    {
        //迴圈執行次數
        for (int i = 0; i < shakeCount; i++)
        {
            //調整攝影機上下晃動
            transform.position += Vector3.up * shakevalue;
            yield return new WaitForSeconds(shakeinterval);
            transform.position -= Vector3.up * shakevalue;
            yield return new WaitForSeconds(shakeinterval);
        }
    }
}
