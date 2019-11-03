using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionText : MonoBehaviour {

    GameController manager;
    public GameObject Selection;
    public string selection;
    public string nextSceneName;
    public bool transition = false;
    public GameObject transitonButton;
    public Transform choicePosition;
    bool selected;
    Text t;

    // Use this for initialization
    public void Start () {
        //print(selection + " " + Selection.name);

        GameObject m = GameObject.FindGameObjectWithTag("GameController");

        if (m != null)
        {
            manager = m.GetComponent<GameController>();
        }

        if (!transition)
        {
            if (selection == Selection.name)
            {
                Selection = GameObject.Find(selection);
            }
            Selection.SetActive(false);
            Selection.transform.position = choicePosition.position;
            Selection.transform.rotation = choicePosition.rotation; 
            //Selection.GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            if(manager != null && SceneManager.GetActiveScene().buildIndex != 0)
            {
                GameObject button = gameObject.GetComponentsInParent<Transform>()[1].gameObject;
                //print(button.name);
                manager.SetTransitionButton(button);
            }
        }

        t = GetComponent<Text>();
	}  

    void Update()
    {
        if (manager != null)
        {
            if (manager.selectedCar != null)
            {
                if (manager.selectedCar.name != selection && !transition)
                {
                    if (t.color != Color.cyan)
                        t.color = Color.white;
                    selected = false;
                }
            }

        }
        else
        {
            gameObject.GetComponentsInParent<Transform>()[1].gameObject.SetActive(false);
        }
    }

    public void OnMyButtonClick()
    {
        //print("Clicked: " + gameObject.name);
        if (transition)
        {
            DontDestroyOnLoad(manager.selectedCar);
            //Selection.SetActive(false);
            //manager.LoadNextScene(nextSceneName);
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                manager.ExitTransition();
            }
            manager.LoadNextScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

            transitonButton.SetActive(true);
            t.color = Color.blue;
            manager.selectedCar = Selection;
            selected = true;     
        }
    }

    public void PointerEnter()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        //print("Pointer Enter text " + selection);
        if (manager.selectedCar != null)
        {
            if (manager.selectedCar.name != selection)
            {
                manager.selectedCar.SetActive(false);
            }
        }
        Selection.SetActive(true);
        Selection.transform.position = choicePosition.position;
        //print(t.color);
        if (t.color != Color.blue)
        {
            //print("enter color");
            t.color = Color.cyan;
        }
    }

    public void PointerExit()
    {
        //print("Pointer Exit text " + selection);
        if (!selected)
        {
            Selection.SetActive(false);
            Selection.transform.position = choicePosition.position;
        }

        if (manager.selectedCar != null)
        {
            manager.selectedCar.SetActive(true);
        }

        if (t.color != Color.blue)
        {
            t.color = Color.white;
        }
    }
}
