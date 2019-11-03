using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class GameController : MonoBehaviour
{
    public int lap;
    public int numLaps = 3;
    GameObject lapTextDisplay;
    Text lapText;
    float lapTimer = 0f;
    float[] lapTimes;

    GameObject statsTextDisplay;
    Text statsText;


    public int speed;
    GameObject speedTextDisplay;
    Text speedText;

    public int place;
    GameObject placeTextDisplay;
    Text placeText;

    public string update;
    GameObject updateDisplay;
    Text updateText;

    float updateTimer = 0.0f;
    public float updateDelta = 2.5f;
    bool updating;

    GameObject updateImageDisplay;
    Image updateImage;
    bool image;

    EndGameControl end;

    public GameObject selectedCar;
    CarController cc;
    CarUserControl cuc;
    CharacterController chc;


    GameObject car;

    public List<int> Scores;

    GameObject timeTextDisplay;
    Text timeText;

    GameObject transitionButton;
    SelectionText transitionSelection;

    GameObject[] uis;

    public bool inTransition;

    GameObject endDisplay;
    Text endText;

    RaceAI ai;
    int count;
    bool countingDown;

    public int waypoint = 0;

    float finalTime = 0f;

    public bool lose;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        end = gameObject.GetComponent<EndGameControl>();
        selectedCar = new GameObject();
        lapTimes = new float[numLaps];
    }

    void Update()
    {
        if (updating)
        {
            updateTimer += Time.deltaTime;
        }

        if (updateTimer >= updateDelta && updateText != null)
        {
            if (countingDown)
            {
                CountDown();
            }
            else
            {
                updateText.text = "";
                updating = false;
                imageControl(false);
            }
            updateTimer = 0f;
            
        }

        if (Input.GetKeyDown("`"))
        {
            incrementLap();
        }

        if (Input.GetKeyDown("'"))
        {
            lose = true;
        }

        if (timeText != null && !inTransition)
        {
            lapTimer += Time.deltaTime;
            timeText.text = getTimeFromFloat(lapTimer);
        }

        
    }

    public void Spawn(Transform spawnTrans)
    {
        print("Spawned");
        if (selectedCar != null)
        {
            selectedCar.transform.position = spawnTrans.position;
            selectedCar.transform.rotation = spawnTrans.rotation;
            selectedCar.GetComponentInChildren<Camera>().depth = 10f;
            cc = selectedCar.GetComponent<CarController>();
            cuc = selectedCar.GetComponent<CarUserControl>();
            chc = selectedCar.GetComponent<CharacterController>();
        }
    }

    public void SetLapsDisplay(GameObject lapDis)
    {
        lap = 1;
        lapTextDisplay = lapDis;
        lapText = lapTextDisplay.GetComponent<Text>();
        lapText.text = "Lap: " + lap + " / " + numLaps;
    }

    public void SetStasDisplay(GameObject statDis)
    {
        statsTextDisplay = statDis;
        statsTextDisplay.SetActive(false);
        statsText = statsTextDisplay.GetComponent<Text>();
    }

    public void SetSpeedDisplay(GameObject speedDis)
    {
        //print("Speed Display Set");
        speed = 0;
        speedTextDisplay = speedDis;
        speedText = speedTextDisplay.GetComponent<Text>();
        speedText.text = speed + " mph";
    }

    public void UpdateSpeed(int spd)
    {
        //print("Update Speed " + spd);
        speed = spd;
        if (speedText != null)
        {
            speedText.text = speed + " mph";
        }
    }

    public void SetPlaceDisplay(GameObject placeDis)
    {
        place = 1;
        placeTextDisplay = placeDis;
        placeText = placeTextDisplay.GetComponent<Text>();
        placeText.text = place == 1 ? "1st" : "2nd";
    }

    public void UpdatePlace(int plc)
    {
        place = plc;
        placeText.text = place == 1 ? "1st" : "2nd";
    }

    public void incrementLap()
    {
        lapTimes[lap-1] = lapTimer;
        lapTimer = 0;
        lap++;

        if(lap > numLaps)
        {
            finalLap();
        }
        else
        {            
            lapText.text = "Lap: " + lap + " / " + numLaps;
        }

        print("Lap: " + lap + " / " + numLaps);

    }

    void finalLap()
    {
        statsTextDisplay.SetActive(true);
        int i = 1;

        float total = 0;
        foreach (var item in lapTimes)
        {
            total += item;
            print("\n\tLap " + i + ": " + getTimeFromFloat(item) + "\n");
            statsText.text = statsText.text + "\n\tLap " + i + ": " + getTimeFromFloat(item) + "\n";
            print(statsText.text);
            i++;
        }

        statsText.text = statsText.text + "\n\n Total: " + getTimeFromFloat(total); 

        finalTime += total;

        if (lose)
        {
            place = 2;
        }
        
        if(place == 2)
        {
            GameObject.Find("Canvas").SetActive(false);
            endGame(false);
        }

        Scores.Add(place);

        transitionButton.SetActive(true);
        transitionSelection.transition = true;
        transitionButton.GetComponentInChildren<Text>().text = "Click for next race!";

        EnterTransition();

        lapTimer = 0;
        timeText.text = getTimeFromFloat(lapTimes[numLaps - 1]);
        
        //LoadNextScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void EnterTransition()
    {
        inTransition = true;


        if (cc != null & cuc != null)
        {
            cc.enabled = false;
            cuc.enabled = false;
        }
        else
        {
            chc.enabled = false;
        }

        uis = GameObject.FindGameObjectsWithTag("UI_clear");

        foreach (GameObject item in uis)
        {
            item.SetActive(false);
        }

        DontDestroyOnLoad(GameObject.Find("Canvas"));
        DontDestroyOnLoad(GameObject.Find("EventSystem"));

    }

    public void ExitTransition()
    {
        inTransition = false;

        if (cc != null & cuc != null)
        {
            cc.enabled = true;
            cuc.enabled = true;
        }
        else
        {
            chc.enabled = true;
        }

        foreach (GameObject item in uis)
        {
            item.SetActive(true);
        }

        transitionSelection.Start();

        if(SceneManager.GetActiveScene().buildIndex + 2 == SceneManager.sceneCountInBuildSettings)
        {
            GameObject.Find("Canvas").SetActive(false);

            if (cc != null & cuc != null)
            {
                cc.enabled = false;
                cuc.enabled = false;
            }
            else
            {
                chc.enabled = true;
            }
        }
    }

    public void SetTimesDisplay(GameObject timeDis)
    {
        
        timeTextDisplay = timeDis;
        timeText = timeTextDisplay.GetComponent<Text>();
        timeText.text = getTimeFromFloat(lapTimer);
    }

    public string getTimeFromFloat(float seconds)
    {
        int min = (int)(seconds / 60f);

        int sec = (int)(seconds);

        int miliSec = (int) ((seconds - sec) * 60f);

        if(min > 0)
        {
            sec -= min * 60;
        }

        return (min < 10 ? ("0" + min) : "" + min) + " : " + (sec < 10 ? ("0" + sec) : "" + sec) + " : " + (miliSec < 10 ? ("0" + miliSec) : "" + miliSec);
    }

    public void SetTransitionButton(GameObject tb)
    {
        //print("gc: " + tb.name);
        transitionButton = tb;
        transitionSelection = tb.GetComponentInChildren<SelectionText>();
        tb.SetActive(false);
    }

    public void SetUpdateDisplay(GameObject updateDis)
    {
        updateDisplay = updateDis;
        //print("ud null ");print(updateDisplay == null);
        updateText = updateDisplay.GetComponent<Text>();
        //print("ut null ");print( updateText == null);
        //print("utt null "); print(updateText.text == null);
        updateText.text = update;
        updating = true;
        imageControl(true);
    }

    public void SetUpdateDisplay(GameObject updateDis, GameObject imageDis)
    {
        SetUpdateDisplay(updateDis);
        setUpdateImage(imageDis);
        image = true;
        imageControl(image);
    }

    public void setUpdateText(string update)
    {
        updateText.text = update;
        updating = true;
        imageControl(image);
    }

    public void setUpdateImage(GameObject updateImg)
    {
        updateImageDisplay = updateImg;
        updateImage = updateImg.GetComponent<Image>();
    }

    public void imageControl(bool active)
    {
        if (updateImageDisplay != null)
        {
            if (active)
            {
                updateImage.SetClipRect(updateText.rectTransform.rect, true);
            }

            updateImage.enabled = active;

        }
    }

    public void endGame(bool isWon)
    {
        end.win = isWon;

       
        SceneManager.LoadScene("EndGameScreen");

        //
    }

    public void setEnd(EndGameControl egc)
    {
        egc.win = end.win;
        egc.winClip = end.winClip;
        egc.gameOverClip = end.gameOverClip;
        egc.finalTime = "Final Time: " + getTimeFromFloat(finalTime);

        egc.StartEnd();
        egc.FixText(endDisplay);

    }


    public void setEndTextDisplay(GameObject endDis)
    {
        endDisplay = endDis;
        endText = endDisplay.GetComponent<Text>();

        //endText.text = "Final Time: " + getTimeFromFloat(finalTime);

    }

    public void setEndTextState(GameObject endText)
    {
        gameObject.GetComponent<EndGameControl>().FixText(endText);
        end.StartEnd();
    }

    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        GetComponent<MusicController>().updateClip(sceneName);
    }

    public void LoadNextScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        GetComponent<MusicController>().updateClip(sceneIndex);
    }

    public void StartRace(RaceAI rai)
    {
        ai = rai;

        if (cc != null && cuc != null)
        {
            cc.enabled = false;
            cuc.enabled = false;
        }
        else
        {
            chc.enabled = false;
        }

        count = 3;
        countingDown = true;
        updateText.text = "" + count;

        timeTextDisplay.SetActive(false);
    }

    void CountDown()
    {
        print("CountDown " + count);
        if(count == 1)
        {
            updateText.text = "Go";

            if (cc != null && cuc != null)
            {
                cc.enabled = true;
                cuc.enabled = true;
            }
            else
            {
                chc.enabled = true;
            }

            ai.BeginRace();
            countingDown = false;
            //updating = false;

            lapTimer = 0;

            timeTextDisplay.SetActive(true);            
        }
        else
        {
            count--;
            updateText.text = "" + count;
        }
    }
}
