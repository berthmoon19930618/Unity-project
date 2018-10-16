using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{

    //在Unity中，想在NavMesh上使用導航功能的物件，需要在移動物件上新增Nav Mesh Agent元件。
    NavMeshAgent agent;
    TankCharacter Enemy;
    public Transform AttackTarget;



    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        Enemy = GetComponent<TankCharacter>();
        //程式直接指定目標物件
        //AttackTarget = GameObject.FindGameObjectWithTag("Player").transform;


        InvokeRepeating("FireControl", 1, 3);
	}
	void FireControl()
    {
        Enemy.Attack();
    }
	// Update is called once per frame
	void Update ()
    {
        //目的地
        agent.destination = AttackTarget.position;
        //面向目標
        transform.LookAt(AttackTarget);
	}
}
