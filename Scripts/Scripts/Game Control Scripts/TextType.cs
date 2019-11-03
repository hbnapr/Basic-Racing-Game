using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextType : MonoBehaviour {

    public string textType = "";

	// Use this for initialization
	void Start () {
        GameObject manager = GameObject.FindWithTag("GameController");
        if (manager != null)
        {
            manager.GetComponent<TextController>().SetTextType(gameObject, textType);
        }
        else
        {

            if (textType == "Stats")
            {
                gameObject.SetActive(false);
            }
        }
    }
	
}
