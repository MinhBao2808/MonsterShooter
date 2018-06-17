using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (EventGameManager.instance.BossKill == true) {
            anim.SetBool("DoorOpen",true);
        }
        else {
            anim.SetBool("DoorOpen", false);
        }
	}
}
