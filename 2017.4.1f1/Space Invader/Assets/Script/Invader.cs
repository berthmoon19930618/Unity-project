using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour {
    public GameObject Explode;
    public float moveSpeed = -0.01f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameFunction.Instance.Score >= 200)
        {
            moveSpeed = -0.02f;
        }
        if (GameFunction.Instance.Score >= 300)
        {
            moveSpeed = -0.03f;
        }
        if (GameFunction.Instance.Score >= 400)
        {
            moveSpeed = -0.04f;
        }
        if (GameFunction.Instance.Score >= 500)
        {
            moveSpeed = -0.05f;
        }
        if (GameFunction.Instance.Score >= 800)
        {
            moveSpeed = -0.06f;
        }
        gameObject.transform.position += new Vector3(0, moveSpeed, 0);
	}
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Bullet"|| collision.tag == "Ship")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            Instantiate(Explode, this.gameObject.transform.position, Quaternion.identity);
            if (collision.tag == "Ship")//如果打到太空船,太空船也播爆炸動畫
            {
                Instantiate(Explode, collision.gameObject.transform.position, Quaternion.identity);
                GameFunction.Instance.GameOver();
            }
        }
        if (collision.tag == "Bullet")
        {
            GameFunction.Instance.AddScore();
        }
    }
    

}
