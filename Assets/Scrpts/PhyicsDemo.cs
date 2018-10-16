using UnityEngine;
using System.Collections;

public class PhyicsDemo : MonoBehaviour {

    //Vector3 vect;//將xyz向量命名為vect
    public GameObject crater;//彈痕

    //OnCollisionEnter 運動中物體開始碰撞時偵測一次
    //OnCollisionStay 運動中物體碰撞中每幀偵測一次
    //OnCollisionExit 運動中物體結束碰撞時偵測一次
    //只要滿足了碰撞條件都會執行以下方法：

    void OnCollisionExit(Collision collOther)
    {
        //collOther 通過參數可以獲取掛腳本其他方的物體
        //gameObject 遊戲物體
        //name 物體名稱
        // Debug.Log(collOther.gameObject.name);

        //接獲第一個接觸點訊息
        ContactPoint cp = collOther.contacts[0];
       // vect = cp.normal;//法線

        //1.要創建的物件 crater
        //2.要創建在何處 cp.point
        //3.要創建的角度 物體y軸正方向與法線方向一致
        GameObject.Instantiate(crater, cp.point+cp.normal*0.03f, Quaternion.FromToRotation(Vector3.up,cp.normal));
    }
    //畫出法線
    //void Update()
    //{
    //    Debug.DrawLine(Vector3.zero, vect);
    //}

    //
    void OnTriggerEnter(Collider collOther)
    {
        //collOther 是對方的碰撞體套件

        Debug.Log(collOther.name);

        //銷毀對方物體
        GameObject.Destroy(collOther.gameObject);
    }
    
    //一樣有以下三種必然事件
    //OnTriggerEnter
    //OnTriggerStay
    //OnTriggerExit
}
