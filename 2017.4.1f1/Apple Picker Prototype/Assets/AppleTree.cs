using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour {

    //用來初始化蘋果實例的預設
    public GameObject applePrefab;

    //蘋果樹移動，單位：米／秒
    public float speed = 1f;

    //蘋果樹活動區域，到達邊界時則改變方向
    public float leftAndRightEdge = 10f;

    //蘋果樹改變方向概率
    public float chanceToChangeDirections = 0.1f;

    //蘋果出現的時間間格
    public float secondsBetweenAppleDrops = 1f;


	// Use this for initialization
	void Start ()
    {
     
        InvokeRepeating("DropApple", 2f, secondsBetweenAppleDrops);//InvokeRepeating重複調用(哪個方法,幾秒後調用,每隔多久調用)//每秒掉落一個蘋果
       
    }
	void DropApple()
    {
        GameObject apple = Instantiate(applePrefab) as GameObject;//Instantiat動態載入(要複製的物件)克隆體類型指定成GameObject
        apple.transform.position = transform.position;
    }
    // Update is called once per frame
    void Update ()
    {
        //基本運動
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        //改變方向
        if (pos.x<-leftAndRightEdge)
        {
            speed = Mathf.Abs(speed);//向右移動 Mathf.Abs(speed)=speed的絕對值 Mathf.Abs為絕對值語法
        }
        else if (pos.x >leftAndRightEdge)
        {
            speed= -Mathf.Abs(speed);//向左移動 
        }
       
    }
    private void FixedUpdate()
    {
       if (Random.value < chanceToChangeDirections)//Random.value為亂數隨機Random的靜態屬性往返0與1之間浮點數(包含0與1)
        {
            speed *= -1;//改變方向
        }
    }
}
