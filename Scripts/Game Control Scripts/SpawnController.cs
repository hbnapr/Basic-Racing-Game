using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject manager = GameObject.FindGameObjectWithTag("GameController");
        if (manager != null)
        {
            manager.GetComponent<GameController>().Spawn(gameObject.transform);
        }
        else
        {
            print("Michael here: There is no GameController, so I hope someone is debugging/testing or else we are in trouble...");
        }
    }
	
}
