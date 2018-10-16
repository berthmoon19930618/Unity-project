using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorControll : MonoBehaviour
{

    public GameObject modle;//角色模型
    public PlayerInput pi;//控制信號模塊
    public float walkSpeed = 1.0f;//移動速度(為了吻合動畫節奏)
    public float turnSpeed = 0.3f;//轉身速度
    public float runMultiple;//跑步速度等
    public float jumpVelocity = 5.0f;//跳躍高度
    public float rollVelocity = 1.0f;//翻滾高度

    [SerializeField]//讓private能顯示在編輯器上
    private Animator anim;//動畫控制器

    //在unity裡做角色位移有兩種組件可以達成一種是用Rigidbody，
    //而另一種是用Character Controll各有優缺點，Rigidbody要做出真實走樓梯運算式非常麻煩但有寫好的重力功能可做勾選，
    //而使用Character Controll他而完美運算出角色走樓梯，且用此組件會有較好的效能分配，但要搞懂Move函式和SimpleMove函式的差別，例如SimpleMove有重力的運算Move則無等等……
    [SerializeField]
    private Rigidbody rigid;
    private Vector3 planarVec;//移動動量三維組件
    private Vector3 thrustVec;//推力向量
    private bool lockPlanar;//鎖定動量Flag (true = 鎖 , false = 開放)


    //要做走路控制要有的組件 1.物件本身GameObject 2.控制信號模塊(自寫) 3.物理系統Rigidbody 或 Character Control
    // Use this for initialization
    void Awake ()
    {
        anim = modle.GetComponent<Animator>();
        pi = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()   //兩幀間格Time.deTtatime
    {
        signalToAnimator();

        //jump觸發
        if (pi.isJump == true)
        {
            anim.SetTrigger("jump");
        }
        //attack觸發
        if (pi.isAttack == true)
        {
            anim.SetTrigger("attack");
        }
        //roll觸發
        if (rigid.velocity.magnitude > 1.0f)
        {
            anim.SetTrigger("roll");
        }

    }

    //將控制器訊號輸入至Animator做銜接
    private void signalToAnimator()
    {
        //print(pi.DUp); 
        //anim.SetFloat("forward", Mathf.Sqrt(pi.DUp * pi.DUp + pi.DRight * pi.DRight));//Mathf.Sqrt開平方根
        //modle.transform.forward = transform.right * pi.DRight + transform.forward * pi.DUp;
        //我們來重構它們 將這些運算式寫回PlayerInput

        //將走路至跑步做緩衝
        //將控制器訊號輸入至動畫控制組件
        //float runTergetMultiple = ((pi.isRun) ? 2.0f : 1.0f);
        //anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), runTergetMultiple, 0.5f));//作數值間直線差值緩衝
        
        //重構
        //將控制器訊號輸入至動畫控制組件
        anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), ((pi.isHighRun) ? 2.0f : 1.0f), 0.5f));

        //pi.Dvec的值若玩家沒按按鍵時，則會DUp與DRight會漸漸變回0，而變為0後就不能做四個向限的判斷，當每秒60幀的偵測到pi.Dmag的值為0，0不具有任何象限方向的，導致系統會將模型強制回歸原點，
        //因此我們要下個判斷式是pi.Dmag要大於0才做轉向。(要稍稍大於0的數去做判斷)
        if (pi.Dmag > 0.1f)
        {
            //Vector3 targetForward = Vector3.Slerp(modle.transform.forward, pi.Dvec, turnSpeed);//計算轉向差值
            //modle.transform.forward = targetForward;
            //重構
            modle.transform.forward = Vector3.Slerp(modle.transform.forward, pi.Dvec, turnSpeed);
        }
        ////移動動量 純量為pi.Dmg*當下模型向量*移動速度(為了吻合動畫節奏)。再用三元表示式判斷isRun是否為真 真:乘2 否:乘1
        //planarVec = pi.Dmag * modle.transform.forward * walkSpeed * ((pi.isRun) ? runMultiple : 1.0f);

        //為了讓跳躍起來能有移動動量
        if (lockPlanar == false)
        {
            planarVec = pi.Dmag * modle.transform.forward * walkSpeed * ((pi.isHighRun) ? runMultiple : 1.0f);
        }
    }

    void FixedUpdate()  //兩幀間格Time.fixedDeltaTime
    {
        //rigid.position += movingVec * Time.fixedDeltaTime;
        //另一種方法
        //使用rigid.velocity特別注意y值要指定為rigidbody的y值量，否則全覆寫成movingVec.y會使玩家角色由高處往低處走實會變成走在空中，再慢慢下落。
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;//thrustVec加個跳躍高度向量
        thrustVec = Vector3.zero;//在觸發那瞬間才會有推力，之後便歸0
        
    }


    ///
    /// Message processing block
    ///
    ///
    ///
    ///
    public void OnJumpEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, jumpVelocity, 0);//跳躍高度
    }
    //public void OnJumpExit()
    //{
    //    
    //    pi.inputEnabled = true;
    //    lockPlanar = false;
    //}

    public void isGround()
    {

        anim.SetBool("isGround", true);
    }
    public void isNotGround()
    {

        anim.SetBool("isGround", false);
        
    }

    public void OnGroundEnter()
    {
        //因為要保持跳躍後依然有移動動量，
        //因此使用了在Ground動畫模塊(在地上)時才發出訊號OnGroundEnter，
        //在這時才lockPlanar = false;
        pi.inputEnabled = true;
        lockPlanar = false;
    }

    public void OnFallEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
    }
    public void OnRollEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, rollVelocity, 0);//滾動高度衝力
    }
    //我自己寫的翻滾向前推力 非教程中的東西
    //攔截使用Update每幀發出訊號的方法，解決動作順移到定點問題
    public void OnRollUpdate()
    {
        thrustVec = modle.transform.forward * anim.GetFloat("rollVelocity");
    }
    public void OnJabEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
    }
    //攔截使用Update每幀發出訊號的方法，解決後跳動作順移到定點問題
    public void OnJabUpdate()
    {
        thrustVec = modle.transform.forward * anim.GetFloat("jabVelocity");
    }
    //指定動畫控制器圖層權重(權重切換)
    public void OnAttackIdleEnter()
    {
        anim.SetLayerWeight(1, 0f);//圖層1為attack層，因打數字不好判別所以使用anim.GetLayerIndex("Attack")如下方方法
        
    }
    public void OnAttack1hAEnter()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack"), 1.0f);//anim.GetLayerIndex("Attack")=圖層1=int 1
        
    }

}
