using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour {
    //在Unity檢視面板中設置字段
    public GameObject prefabProjectile;
    public float velocityMult = 10f;
    public bool _______________________________;
    //動態設置的字段
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;

    void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }
    void Update()
    {
        //如果彈弓位處於描準模式(aimingmode)，則跳過以下代碼
        if (!aimingMode)
        {
            return;//不管在哪一層，只要用了return都會跳出此函式
        }
        //獲取鼠標光標在二維視窗當前座標
        Vector3 mousePos2D = Input.mousePosition;
        //將鼠標光標位置轉換為三維座標
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        //計算launchPos到mousePos3D兩點之間的座標差
        Vector3 mouseDelta = mousePos3D - launchPos;
        //將mouseDelta座標差限制在彈弓的球狀碰撞器半徑範圍內
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }
        //將projectitle移動到新位置
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;
        if (Input.GetMouseButtonUp(0))
        {
            //如果鬆開鼠標
            aimingMode = false;
            projectile.GetComponent<Rigidbody>().isKinematic = false;
            projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * velocityMult;
            projectile = null;
        }
    }

    void OnMouseEnter()
    {
        launchPoint.SetActive(true);
        //print("Slingshot:MonoBehaviour()");
    }
    void OnMouseExit()
    {
        launchPoint.SetActive(false);
        //print("Slingshot:MonoBehaviour()");
    }
    void OnMouseDown()
    {
        //玩家在鼠標光標懸停在彈攻上方時按下滑鼠左鍵
        aimingMode = true;
        //實例劃一個彈丸
        projectile = Instantiate(prefabProjectile) as GameObject;
        //該實例的初始位置位於launchPoint處
        projectile.transform.position = launchPos;
        //設置當前的isKinematic屬性
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }
}
