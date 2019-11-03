using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UiInteraction : MonoBehaviour
{
    public string nextSceneName;
    GameController manager;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    
    public void OnMyButtonClick()
    {
        print("Button Pressed");
        SceneManager.LoadScene(nextSceneName);
    }

    public void PointerEnter()
    {
        print("Pointer Enter text");
    }

    public void PointerExit()
    {
        print("Pointer Exit text");
    }
}
