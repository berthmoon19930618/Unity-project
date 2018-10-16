using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //獲勝文字
    public Text winText;
    //計分UI
    public Text countText;
    //2D物理組件
    private Rigidbody2D rb2d;
    //計分
    private int count;
    //移動速度
    public float moveSpeed;
    void Start()
    {
        //計分歸0
        count = 0;
        //找到Rigidbody2D組件
        rb2d = GetComponent<Rigidbody2D>();
        //計分顯示
        SetCountText();
        winText.text= "";
    }
    void FixedUpdate()
    {
        //水平移動座值
        float moveHorizontal = Input.GetAxis("Horizontal");
        //垂直移動值
        float moveVertical = Input.GetAxis("Vertical");
        //移動座標
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        //推動物體
        rb2d.AddForce(movement * moveSpeed);
        
    }
    //當此類去觸發到其他類的觸發器
    void OnTriggerEnter2D(Collider2D other)
    {
        //如果被觸發到tag為PickUp
        if (other.gameObject.CompareTag("PickUp"))
        {
            //將被觸發方關掉
            other.gameObject.SetActive(false);
            //計分+1
            count++;
            //計分顯示
            SetCountText();

        } 
    }
    //計分顯示方法
    void SetCountText()
    {
        countText.text = "Count = " + count.ToString();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }
    }
}