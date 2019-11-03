using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    Camera c;

	// Use this for initialization
	void Start () {
        c = gameObject.GetComponent<Camera>();
        c.depth = -100;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("e") || Input.GetKeyDown("/"))
        {
            c.depth = c.depth == -100 ? 0 : -100;
        }

        if (Input.GetKey("f") || Input.GetKey("'"))
        {
            c.rect = new Rect(0, 0, 1, 1);
        }

        if (Input.GetKeyUp("f") || Input.GetKeyUp("'"))
        {
            c.rect = new Rect(0.6f, 0, 1, 0.5f);
        }
    }
}
