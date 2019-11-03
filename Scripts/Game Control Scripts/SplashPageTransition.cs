using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashPageTransition : MonoBehaviour {

    public string nextSceneName;
    public float transitionInterval = 3f;
    public Text splashText;
    public GameObject startParticles;
    GameObject[] selectStartObjects;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Use this for initialization
    IEnumerator Start () {
        selectStartObjects = GameObject.FindGameObjectsWithTag("Selctable_Start");
        //print(selectStartObjects.Length);
        foreach (var item in selectStartObjects)
        {
            //print("Turn off" + item.name);
            item.SetActive(false);
        }
        yield return StartCoroutine("LoadScene");
    }

    // Update is called once per frame
    IEnumerator LoadScene() {
        yield return new WaitForSeconds(transitionInterval);
        //SceneManager.LoadScene(nextSceneName);
        splashText.enabled = false;
        startParticles.SetActive(false);
        //print("after turn off");
        foreach (var item in selectStartObjects)
        {
            //print("Turn on " + item.name);
            item.SetActive(true);
        }
    }
}
