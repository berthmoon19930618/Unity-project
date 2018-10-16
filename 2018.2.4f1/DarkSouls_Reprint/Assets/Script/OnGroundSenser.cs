using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//膠囊碰撞偵測
public class OnGroundSenser : MonoBehaviour {

    public CapsuleCollider capcol;
    public float offset = 0.1f;

    private Vector3 point1;
    private Vector3 point2;
    private float capRadius;



	// Use this for initialization
	void Awake ()
    {
        capRadius = capcol.radius - 0.05f;//減去一點點去讓感測區沉降些
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        point1 = transform.position + transform.up * (capRadius-offset);//位置往上加個半徑，減去一點點offset去讓感測區沉降些
        point2 = transform.position + transform.up * (capcol.height-offset) - transform.up * capRadius;//位置+膠囊體高度減去一個半徑，減去一點點offset去讓感測區沉降些

        Collider[] outputCols = Physics.OverlapCapsule(point1, point2, capRadius,LayerMask.GetMask("Ground"));
        if (outputCols.Length != 0)
        {
            SendMessageUpwards("isGround");
            
        }
        else
        {
            SendMessageUpwards("isNotGround");
        }

       


    }
}
