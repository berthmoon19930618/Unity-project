using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour {

    public static float bottomY = -20f;

	// Update is called once per frame
	void Update () {
        if (transform.position.y < bottomY)
        {
            Destroy(this.gameObject);
            //獲取對主象機的ApplePicker組件引用
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            //調用apScript的AppleDestroyed方法
            apScript.AppleDestroyed();
        }
	}
}
