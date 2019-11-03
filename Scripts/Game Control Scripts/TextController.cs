using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(Text))]

public class TextController : MonoBehaviour {

    public List<string> textTypes;

    // Use this for initialization
    public void SetTextType(GameObject textObject, string textType)
    {
        GameController manager = GetComponent<GameController>();

        switch (textTypes.IndexOf(textType))
        {
            case 0:
                manager.SetLapsDisplay(textObject);
                break;
            case 1:
                manager.SetSpeedDisplay(textObject);
                break;
            case 2:
                manager.SetPlaceDisplay(textObject);
                break;
            case 3:
                //manager.SetPlaceDisplay(gameObject);
                break;
            case 4:
                manager.SetUpdateDisplay(textObject);
                break;
            case 5:
                manager.SetTimesDisplay(textObject);
                break;
            case 6:
                manager.SetStasDisplay(textObject);
                break;
            case 7:
                manager.setEndTextDisplay(textObject);
                break;
            default:
                print("TextType " + textType + " not found."); 
                break;
        }

       
        
        
    }
}
