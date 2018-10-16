using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public Rigidbody PlayerRigidbody;
    public Animator PlayerAnimator;
    public float turnSpeed = 0.3f;
    public float runSpeed = 100;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Run(float h, float v)
    {
        //1.轉身
        //2.移動

        //1.轉身

        //要此判斷才做動作不然會有轉過去又再轉回來的bug
        if (h != 0 || v != 0)
        {
            //1.取得自身方向
            Vector3 dir = new Vector3(h, 0, v).normalized;//歸一化處理以免速度疊加
            //2.轉換成四元素
            Quaternion look = Quaternion.LookRotation(dir);
            //3.做差值計算
            Quaternion lookLerp = Quaternion.Slerp(transform.rotation, look, turnSpeed);
            //4.將轉向設成計算後的結果
            this.transform.rotation = lookLerp;

        }

        //2.移動

        //判斷移動速度小於0.5是轉身
        if (Mathf.Abs(h)<0.5f && Mathf.Abs(v) < 0.5f)
        {
            this.PlayerAnimator.SetBool("isRun", false);
        }
        else
        {
            this.PlayerAnimator.SetBool("isRun", true);
            //設置移動速度
            this.PlayerRigidbody.velocity = new Vector3(h, this.PlayerRigidbody.velocity.y, v) * this.runSpeed * Time.deltaTime;
        }


    }
}
