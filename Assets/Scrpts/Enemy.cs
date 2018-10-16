using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Update is called once per frame
	void Update ()
    {
        Movement();
    }
    public float moveSpeed = 0.5f;
    void Movement()
    {
        //通過正弦曲線獲取x軸1至-1之間來回的值
        Vector3 moveVector = new Vector3(Mathf.Sin(Time.time), 0, -moveSpeed);
        transform.Translate(moveVector * Time.deltaTime);
    }
    
    //觸發檢測
    void OnTriggerEnter(Collider collOther)
    {
        if (collOther.tag == "PlayerBullet")

        //假設對方tag是PlayerBullet或者Player
        //if (collOther.tag == "PlayerBullet"||collOther.tag == "Player") 
        {
            Death();
        }
    }
    void Death()
    {
        GameObject.Destroy(this.gameObject);
    }
}
