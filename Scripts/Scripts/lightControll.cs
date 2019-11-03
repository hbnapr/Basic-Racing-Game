using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightControll : MonoBehaviour {
   public Light left_light, right_light;
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            left_light.enabled = !left_light.enabled;
            right_light.enabled = !right_light.enabled;

        }
	}
}
