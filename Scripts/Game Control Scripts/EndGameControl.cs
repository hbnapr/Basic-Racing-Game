using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameControl : MonoBehaviour {

    public bool win { get; set; }
    GameObject endCamera;
    public AudioClip gameOverClip;
    public AudioClip winClip;
    GameObject gc;
    public string finalTime;

    public void Start()
    {
        win = true;

        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings-1)
        {
            gc = GameObject.FindGameObjectWithTag("GameController");
            gc.GetComponent<GameController>().setEnd(this);
           
        }
    }

    public void StartEnd()
    {
        GameObject camera = GameObject.Find("Main Camera");
        MusicController mc = gc.GetComponent<MusicController>();
        mc.updateCamera(camera);

        GameObject[] hide = GameObject.FindGameObjectsWithTag("GameOverHide");

        if (win)
        {
            //camera.GetComponent<Camera>().backgroundColor = new Color(1f, 1f, 1f);
            mc.updateClip(winClip);
        }
        else
        {
            camera.GetComponent<Camera>().backgroundColor = new Color(0f, 0f, 0f);
            mc.updateClip(gameOverClip);

            foreach (var item in hide)
            {
                item.SetActive(false);
            }
        }
    }

    public void FixText(GameObject end)
    {
        Text t = end.GetComponent<Text>();

        if (!win)
        {
            t.text = "You let them win... How could you?\nGame Over";
            t.color = new Color(242 / 255f, 12 / 255f, 12 / 255f, 255 / 255f);
        }
        else
        {
            t.text = "You Win!\n" + finalTime;
        }
        
    }
}
