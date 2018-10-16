using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject BulletB;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position += new Vector3(0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position += new Vector3(-0.1f, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector3 pos =gameObject.transform.position + new Vector3(0, 0.6f,0);
            Instantiate(Bullet, pos, Quaternion.identity);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            Vector3 pos = gameObject.transform.position + new Vector3(0, 0.6f, 0);
            Instantiate(BulletB, pos, Quaternion.identity);
        }
    }
}

