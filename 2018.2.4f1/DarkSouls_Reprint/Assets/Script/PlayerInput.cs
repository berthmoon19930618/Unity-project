using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("===== Key Setting =====")]
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyRight = "d";
    public string keyLeft = "a";

    public string keyJUp = "up";
    public string keyJDown = "down";
    public string keyJRight = "right";
    public string keyJLeft = "left";

    public string keyA;
    public string keyB;
    public string keyC;
    public string keyD;

    [Header("===== Output Signal =====")]
    public float Dmag;//原點至作用點之距離(作用力)
    public float DUp;//垂直方向力量
    public float DRight;//水平方向力量
    public float JUp;//攝影機垂直方向力量
    public float JRight;//攝影機水平方向力量
    public Vector3 Dvec;//方向向量

    //1.pressing signal
    public bool isHighRun;
    //2.trigger once signal

    public bool isJump;
    private bool lastJump;

    public bool isAttack;
    private bool lastAttack;

    //3.double trigger

    [Header("===== Others =====")]
    public bool inputEnabled = true;//Flag On
    private float targetDUp;//垂直上限
    private float targetDRight;//水平上限
    private float velocityDUp;//給參考空間用
    private float velocityDRight;//給參考空間用

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //將訊號轉成雙軸訊號
        //攝影機
        JUp = (Input.GetKey(keyJUp) ? 1.0f : 0) - (Input.GetKey(keyJDown) ? 1.0f : 0);
        JRight = (Input.GetKey(keyJRight) ? 1.0f : 0) - (Input.GetKey(keyJLeft) ? 1.0f : 0);
        //角色
        targetDUp = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);//?():() 三元表示式   條件運算子 (?:) 通稱為三元條件運算式，是根據布林運算式的值傳回兩個值的其中一個。
        targetDRight = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);      
        
        //Flag Off
        if (inputEnabled == false)
        {
            targetDUp = 0;
            targetDRight = 0;
        }

        //垂直方向力量
        DUp = Mathf.SmoothDamp(DUp, targetDUp, ref velocityDUp, 0.1f);
        //水平方向力量
        DRight = Mathf.SmoothDamp(DRight, targetDRight, ref velocityDRight, 0.1f);

        ////原點至作用點之距離(作用力)//(舊方法，會有斜方移動大於直線移動問題(1.414....倍))
        //Dmag = Mathf.Sqrt(DUp * DUp + DRight * DRight);
        ////方向向量
        //Dvec = transform.right * DRight + transform.forward * DUp;

        //因為用方轉圓公式來解決45度斜方移動較多問題的關係DUp與DRight成了新的數值了
        Vector2 tempDAxis = SquareToCircle(new Vector2(DRight, DUp));

        float DRight2 = tempDAxis.x;
        float DUp2 = tempDAxis.y;

        //原點至作用點之距離(作用力)
        Dmag = Mathf.Sqrt(DUp2 * DUp2 + DRight2 * DRight2);

        //方向向量
        Dvec = transform.right * DRight2 + transform.forward * DUp2;
        isHighRun = Input.GetKey(keyA);

        //單擊觸發式信號跳躍按鈕事件
        bool newJump= Input.GetKey(keyB);
        if (newJump!= lastJump && newJump == true)
        {
            isJump = true;
            //print("jump trigger!!!!!!!!");
        }
        else
        {
            isJump = false;
        }
        lastJump = newJump;
        //單擊觸發式信號攻擊1hA按鈕事件
        bool newAttack = Input.GetKey(keyC);
        if (newAttack != lastAttack && newAttack == true)
        {
            isAttack = true;
            //print("jump trigger!!!!!!!!");
        }
        else
        {
            isAttack = false;
        }
        lastAttack = newAttack;

    }

    //方形座標轉圓形座標
    private Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1.0f - (input.y * input.y) / 2);//方轉圓數學公式
        output.y = input.y * Mathf.Sqrt(1.0f - (input.x * input.x) / 2);//方轉圓數學公式
        return output;
    }

}
