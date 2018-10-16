using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXE : MonoBehaviour
{

    public float explosionRadius;//彈藥半徑
    public LayerMask damageMask;
    public float damage = 20;//傷害
    public AudioSource explosionAudioSource;
    public ParticleSystem explosionEffect;
    public bool isRotate = false;



    // Use this for initialization
    private void Start ()
    {
        Destroy(gameObject, 3.5f);
        if (isRotate)
        {
            GetComponent<Rigidbody>().AddTorque(transform.right * 1000);//增加力矩
        }
	}
	
    
    private void OnTriggerEnter(Collider other)
    {
        //3D球體碰撞檢測
        var colliders = Physics.OverlapSphere(transform.position, explosionRadius, damageMask);
        //position  碰撞球體中心
        //radius    碰撞球體半徑
        //layerMask 在某Layer進行碰撞檢測，例如當前選中Player層，則只會回傳周圍半徑內              
        //          Layer標示為Player的GameObject的碰撞體的集合

        foreach(var collider in colliders)
        {

            var target = collider.GetComponent<TankCharacter>();
            if (target)
            {
                target.TakeDamage(damage);
            }

        }

        explosionAudioSource.Play();

        explosionEffect.transform.parent = null;
        explosionEffect.Play();

        ParticleSystem.MainModule mainModule = explosionEffect.main;
        Destroy(explosionEffect.gameObject, mainModule.duration);//mainModule.duration粒子持續時間

        Destroy(gameObject);
    }
}
