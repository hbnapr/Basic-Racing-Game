using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour {
    public GameObject cameraPosition;
    AudioClip clip;
    AudioSource audioComponent;
    public List<AudioClip> audioClips;
    public List<string> sceneNames;
    Dictionary<string, AudioClip> sceneNameToClip;

    void Start()
    {
        if(audioClips.Count != sceneNames.Count)
        {
            throw new System.Exception("audioClips.Count != sceneNames.Count");
        }

        updateCamera(cameraPosition);
        audioComponent = GetComponent<AudioSource>();

        updateClip(SceneManager.GetActiveScene().buildIndex);

        sceneNameToClip = new Dictionary<string, AudioClip>();

        //print(SceneManager.sceneCountInBuildSettings);

        for (int i = 0; i < sceneNames.Count; i++)
        {
            //print(sceneNames[i] + " " + audioClips[i].name);
            sceneNameToClip.Add(sceneNames[i], audioClips[i]);
        }
    }

   public void updateCamera(GameObject newCamera)
    {
        cameraPosition = newCamera;
        gameObject.transform.position = cameraPosition.transform.position;
    }

    public void updateClip(int scenebuildIndex)
    {
        audioComponent.clip = audioClips[scenebuildIndex];
        audioComponent.Play();
    }

    public void updateClip(string sceneName)
    {
        print(sceneName);
        audioComponent.clip = sceneNameToClip[sceneName];
        audioComponent.Play();
    }

    

    public void updateClip(AudioClip newClip)
    {
        audioComponent.clip = newClip;
        audioComponent.Play();
    }
}
