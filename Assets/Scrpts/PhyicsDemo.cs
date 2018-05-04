using UnityEngine;
using System.Collections;

public class PhyicsDemo : MonoBehaviour {

    //OnCollisionEnter 運動中物體開始碰撞時偵測一次
    //OnCollisionStay 運動中物體碰撞中每幀偵測一次
    //OnCollisionExit 運動中物體結束碰撞時偵測一次
    //只要滿足了碰撞條件都會執行以下方法：
    void OnCollisionExit(Collision collOther)
    {
        //collOther 通過參數可以獲取掛腳本其他方的物體
        //gameObject 遊戲物體
        //name 物體名稱
        Debug.Log(collOther.gameObject.name);
	}

}
