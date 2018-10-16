using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankCharacter : MonoBehaviour
{

    public bool Go = true;
    public float moveSpeed;//移動速度
    public float turnSpeed;//選轉速度

    //攻擊子彈所需變數
    public Rigidbody shell;
    public Transform mizzle;
    public float launchForce = 10;//作用力

    public AudioSource shootAudioSource;//攻擊音源

    bool attacking = false;//判斷是否在攻擊中 (攻擊開關)
    public float attackingTime;//攻擊間格時間


    //HP
    public float hpMax = 100;
    float hp;
    //HP UI所需變數
    public Slider hpSlider;//屬性視窗之滑桿
    public Image hpFillImage;//血條圖片
    public Color hpColorNull = Color.red;//空血
    public Color hpColorFull = Color.green;//滿血

    //死亡
    public ParticleSystem explosionEffect;//死亡特效

    CharacterController characterController;
    Animator animator;
    public bool isAlive = true;//判斷是否死亡

    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        hp = hpMax;
        ReHpHUD();
    }
    //移動
    public void Move(Vector3 disv)
    {
        if (!isAlive)
        {
            return;
        }
        if (attacking)
        {
            return;
        }
        Vector3 movement = disv * moveSpeed;
        characterController.SimpleMove(movement);

        if (animator)
        {
            animator.SetFloat("Speed", characterController.velocity.magnitude);
        }
    }

    //攻擊
    public void Attack()
    {
        if (!isAlive)
        {
            return;
        }
        if (attacking)
        {
            return;
        }
        //實例化子彈
        var shellInstance = Instantiate(shell, mizzle.position, mizzle.rotation) as Rigidbody;
        //初始向前的速度
        shellInstance.velocity = launchForce * mizzle.forward;
        //播放攻擊動畫
        if (animator)
        {
            animator.SetTrigger("Attack");
        }
        attacking = true;
        shootAudioSource.Play();
        //調用(方法,延遲時間)
        Invoke("ReAttack", attackingTime);
    }
    //刷新攻擊開關
    void ReAttack()
    {
        attacking = false;
    }

    //選轉方向(不是很懂)
    public void Rotate(Vector3 lookdir)
    {
        //求出面向步驟
        var targetPos = transform.position + lookdir;//面相目標位置
        var characterPos = transform.position;//自身位置

        //去除y軸影響
        targetPos.y = 0;
        characterPos.y = 0;

        //得到角色面朝目標之方向(向量)
        var faceToTargetDir = targetPos - characterPos;

        //獲得面朝向量的四元數
        var faceToQuat = Quaternion.LookRotation(faceToTargetDir);
        //求球面線性插值
        Quaternion slerp = Quaternion.Slerp(transform.rotation, faceToQuat, turnSpeed * Time.deltaTime);

        transform.rotation = slerp;

    }

    //受擊
    public void TakeDamage(float amount)
    {
        hp = hp - amount;
        ReHpHUD();
        if (hp <= 0 && isAlive)
        {
            Death();
        }
    }
    //HP UI刷新
    public void ReHpHUD()
    {
        hpSlider.value = hp;
        hpFillImage.color = Color.Lerp(hpColorNull, hpColorFull, hp / hpMax);
    }

    //死亡
    public void Death()
    {
        isAlive = false;
        explosionEffect.transform.parent = null;
        explosionEffect.gameObject.SetActive(true);
        ParticleSystem.MainModule mainModule = explosionEffect.main;
        Destroy(explosionEffect.gameObject, mainModule.duration);//mainModule.duration粒子持續時間

        this.gameObject.SetActive(false);
        Go = false;

    }
}
