using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerCharacter))] //要求掛上組件
public class PlayerController : MonoBehaviour
{


    public float Move_h;
    public float Move_v;
    private PlayerCharacter player;

    // Use this for initialization
    void Start()
    {
        this.player = GetComponent<PlayerCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        this.Move_h = Input.GetAxis("Horizontal");
        this.Move_v = Input.GetAxis("Vertical");
        this.player.Run(this.Move_h, this.Move_v);
    }
}