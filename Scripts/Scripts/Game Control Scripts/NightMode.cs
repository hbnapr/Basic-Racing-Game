using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightMode : MonoBehaviour
{

    GameObject Sun;
    GameObject flashLight;
    GameObject flashLightLight;

    public Material nightSkybox;
    bool isNight;

    // Use this for initialization
    void InstantiateNightMode()
    {
        Sun = GameObject.Find("Sun");

        if (Sun != null)
        {
            flashLight = GameObject.Find("Flashlight");
            flashLightLight = GameObject.FindGameObjectWithTag("FlashLight");

            if(flashLight != null && flashLightLight != null)
            {
                ActivateNightMode();
            }           
        }

    }

    public bool getIsNight()
    {
        return isNight;
    }

    void ActivateNightMode()
    {
        isNight = true;
        Sun.SetActive(false);
        flashLight.GetComponent<MeshRenderer>().enabled = true;
        flashLightLight.GetComponent<Light>().enabled = true;
        RenderSettings.skybox = nightSkybox;
        //print(RenderSettings.skybox.name);
        RenderSettings.fog = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z")){
            InstantiateNightMode();
        }

    }
}
