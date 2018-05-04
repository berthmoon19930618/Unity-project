using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

	// Use this for initialization
    Material mt;
	float y = 0;
	public float moveSpeed = 0.1f;
	void Start () 
	{
    //找到材質
	mt=this.renderer.material;
	}
	// Update is called once per frame
	
	void Update () 
	{
	 Movement();
	}
	void Movement()
	{
        //依造機體效能做同樣的移動距離
		y = y - Time.deltaTime * moveSpeed;
	  if(y<=-1)
	  {
	   y=0;
	  } 
      //賦予材質新座標數值
	 mt.mainTextureOffset=new Vector2(0,y);
	}
}
