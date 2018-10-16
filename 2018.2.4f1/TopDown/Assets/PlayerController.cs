using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //獲取玩家腳本
    PlayerCharacter character;

    public bool Go;

    private void Start()
    {
        character = GetComponent<PlayerCharacter>();
    }

    void Update()
    {
        if (Go == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                character.Attack();
            }

            var hor = Input.GetAxis("Horizontal");
            var ver = Input.GetAxis("Vertical");

            character.Move(new Vector3(hor, 0, ver));

            //方向移動
            var lookDir = Vector3.forward * ver + Vector3.right * hor;


            //magnitude(大小)
            if (lookDir.magnitude != 0)
            {
                character.Rotate(lookDir);
            }
    }



}
}
