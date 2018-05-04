using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float liveTime = 2;
    // Use this for initialization
    //當前物體被呼叫時
    void Start ()
    {
        BulletDestory();
    }
    public float moveSpeed = 5;//移動數度
	// Update is called once per frame
	void Update ()
    {
        Movement();
    }
    void Movement()
    {
        //沿著z軸方向正向移動
        this.transform.Translate(0, 0, moveSpeed * Time.deltaTime);
    }
    //銷毀子彈
    void BulletDestory()
    {
        //銷毀物件(哪個物件,時間)
        GameObject.Destroy(this.gameObject, liveTime);
    }
}
