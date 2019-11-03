using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class CameraController : MonoBehaviour
{
    public GameObject[] cameras;
    public int numberOfCameras = 3;
    public GameObject startCamera;
    public GameObject secondCamera;
    public GameObject lastCamera;
    int curentKeyIndex;

    GameObject manager;

    List<Grayscale> Grays;
    List<SepiaTone> Sepias;
    List<Bloom> Blooms;
    List<GlobalFog> Fogs;
    List<Antialiasing> Antis;
    List<DepthOfField> DOFs;
    List<Fisheye> Fishes;
    List<SunShafts> Shafts;

    enum VisualEffects
    {
        Normal = 0,
        GrayScale = 1,
        Sepia = 2,
        Bloom = 3,
        GlobalFog = 4,
        Anti = 5,
        DOF = 6,
        FishEye = 7,
        SunShaft = 8
    };

    VisualEffects ve = 0;

    int numVe = Enum.GetNames(typeof(VisualEffects)).Length;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.FindWithTag("GameController");
        if (numberOfCameras > 3)
        {
            cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        }
        else
        {
            cameras = new GameObject[numberOfCameras];

            if (numberOfCameras == 3)
            {
                cameras[0] = startCamera;
                cameras[1] = secondCamera;
                cameras[2] = lastCamera;
            }
            else if (numberOfCameras == 1)
            {
                cameras[0] = startCamera;
            }

            Grays = new List<Grayscale>();
            Sepias = new List<SepiaTone>();
            Blooms = new List<Bloom>();
            Fogs = new List<GlobalFog>();
            Antis = new List<Antialiasing>();
            DOFs = new List<DepthOfField>();
            Fishes = new List<Fisheye>();
            Shafts = new List<SunShafts>();

            foreach (var item in cameras)
            {

                item.AddComponent<Grayscale>();

                Grayscale gs = item.GetComponent<Grayscale>();

                Grayscale mgs = manager.GetComponent<Grayscale>();

                gs.shader = mgs.shader;
                gs.rampOffset = mgs.rampOffset;

                Grays.Add(gs);

                item.AddComponent<SepiaTone>();

                SepiaTone st = item.GetComponent<SepiaTone>();
                st.shader = manager.GetComponent<SepiaTone>().shader;

                Sepias.Add(st);

                item.AddComponent<Bloom>();

                //print(name + " " + item.name);

                Bloom bm = item.GetComponent<Bloom>();

                Bloom mbm = manager.GetComponent<Bloom>();

                bm.blurAndFlaresShader = mbm.blurAndFlaresShader;
                bm.brightPassFilterShader = mbm.brightPassFilterShader;
                bm.lensFlareShader = mbm.lensFlareShader;
                bm.screenBlendShader = mbm.screenBlendShader;

                bm.bloomThreshold = mbm.bloomThreshold;
                bm.bloomIntensity = mbm.bloomIntensity;

                Blooms.Add(bm);

                item.AddComponent<GlobalFog>();

                GlobalFog gf = item.GetComponent<GlobalFog>();

                GlobalFog mgf = manager.GetComponent<GlobalFog>();

                gf.fogShader = mgf.fogShader;
                gf.excludeFarPixels = mgf.excludeFarPixels;
                gf.height = mgf.height;
                gf.heightDensity = mgf.heightDensity;
                gf.startDistance = mgf.startDistance;

                Fogs.Add(gf);

                item.AddComponent<Antialiasing>();

                Antialiasing ai = item.GetComponent<Antialiasing>();

                Antialiasing mai = manager.GetComponent<Antialiasing>();

                ai.shaderFXAAIII = mai.shaderFXAAIII;
                ai.ssaaShader = mai.ssaaShader;
                ai.edgeSharpness = mai.edgeSharpness;
                ai.shaderFXAAII = mai.shaderFXAAII;
                ai.shaderFXAAPreset2 = mai.shaderFXAAPreset2;
                ai.shaderFXAAPreset3 = mai.shaderFXAAPreset3;
                ai.dlaaShader = mai.dlaaShader;
                ai.nfaaShader = mai.nfaaShader;

                Antis.Add(ai);

                item.AddComponent<DepthOfField>();

                DepthOfField dof = item.GetComponent<DepthOfField>();

                DepthOfField mdof = manager.GetComponent<DepthOfField>();

                dof.dofHdrShader = mdof.dofHdrShader;
                dof.dx11BokehShader = mdof.dx11BokehShader;

                dof.focalSize = mdof.focalSize;
                dof.aperture = mdof.aperture;
                dof.focalTransform = transform;

                DOFs.Add(dof);

                item.AddComponent<Fisheye>();

                Fisheye fe = item.GetComponent<Fisheye>();

                Fisheye mfe = manager.GetComponent<Fisheye>();

                fe.fishEyeShader = mfe.fishEyeShader;
                fe.strengthX = mfe.strengthX;
                fe.strengthY = mfe.strengthY;

                Fishes.Add(fe);

                item.AddComponent<SunShafts>();

                SunShafts ss = item.GetComponent<SunShafts>();

                SunShafts mss = manager.GetComponent<SunShafts>();

                ss.simpleClearShader = mss.simpleClearShader;
                ss.sunShaftsShader = mss.sunShaftsShader;

                ss.sunShaftIntensity = mss.sunShaftIntensity;
                ss.radialBlurIterations = mss.radialBlurIterations;
                ss.maxRadius = mss.maxRadius;
                ss.radialBlurIterations = mss.radialBlurIterations;
                ss.resolution = mss.resolution;
                ss.screenBlendMode = mss.screenBlendMode;
                ss.sunColor = mss.sunColor;
                ss.sunShaftBlurRadius = mss.sunShaftBlurRadius;
                ss.sunThreshold = mss.sunThreshold;
                ss.sunTransform = mss.sunTransform;
                ss.useDepthTexture = mss.useDepthTexture;

                Shafts.Add(ss);


            }

            foreach (Grayscale item in Grays)
            {
                item.enabled = false;
            }


            foreach (SepiaTone item in Sepias)
            {
                item.enabled = false;
            }

            foreach (Bloom item in Blooms)
            {
                item.enabled = false;
            }

            foreach (GlobalFog item in Fogs)
            {
                item.enabled = false;
            }

            foreach (DepthOfField item in DOFs)
            {
                item.enabled = false;
            }

            foreach (Antialiasing item in Antis)
            {
                item.enabled = false;
            }

            foreach (Fisheye item in Fishes)
            {
                item.enabled = false;
            }

            foreach (SunShafts item in Shafts)
            {
                item.enabled = false;
            }


        }

        curentKeyIndex = 0;
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            setTop();
        }
    }

    public void setTop()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (i == curentKeyIndex % numberOfCameras)
            {
                cameras[i].GetComponent<Camera>().depth = 0;
                cameras[i].GetComponent<AudioListener>().enabled = true;
                if (manager != null)
                {
                    //print("Changing music camera");
                    manager.GetComponent<MusicController>().updateCamera(cameras[i]);
                }
            }
            else
            {
                cameras[i].GetComponent<Camera>().depth = -1;
                cameras[i].GetComponent<AudioListener>().enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (Input.GetKeyDown("q"))
            {
                ++curentKeyIndex;
                setTop();
                setVisualEffect();
            }
            if (Input.GetKeyDown("1"))
            {
                curentKeyIndex = 0;
                setTop();
                setVisualEffect();
            }
            if (Input.GetKeyDown("2"))
            {
                curentKeyIndex = 1;
                setTop();
                setVisualEffect();
            }
            if (Input.GetKeyDown("3"))
            {
                curentKeyIndex = 2;
                setTop();
                setVisualEffect();
            }
        }

        if (Input.GetKeyDown("z"))
        {
            int veValue = (((int)ve + 1) % numVe);
            ve = (VisualEffects)veValue;
            print("z + " + ve + " : " + veValue);

            setVisualEffect();
        }
    }

    void setVisualEffect()
    {
        Grays[curentKeyIndex % numberOfCameras].enabled = false;
        Sepias[curentKeyIndex % numberOfCameras].enabled = false;
        Blooms[curentKeyIndex % numberOfCameras].enabled = false;
        Fogs[curentKeyIndex % numberOfCameras].enabled = false;
        Antis[curentKeyIndex % numberOfCameras].enabled = false;
        DOFs[curentKeyIndex % numberOfCameras].enabled = false;
        Fishes[curentKeyIndex % numberOfCameras].enabled = false;
        Shafts[curentKeyIndex % numberOfCameras].enabled = false;

        switch (ve)
        {
            case VisualEffects.Normal:
                break;
            case VisualEffects.GrayScale:
                Grays[curentKeyIndex % numberOfCameras].enabled = true;
                break;
            case VisualEffects.Sepia:
                Sepias[curentKeyIndex % numberOfCameras].enabled = true;
                break;
            case VisualEffects.Bloom:
                Blooms[curentKeyIndex % numberOfCameras].enabled = true;
                break;
            case VisualEffects.GlobalFog:
                Fogs[curentKeyIndex % numberOfCameras].enabled = true;
                break;
            case VisualEffects.Anti:
                Antis[curentKeyIndex % numberOfCameras].enabled = true;
                break;
            case VisualEffects.DOF:
                DOFs[curentKeyIndex % numberOfCameras].enabled = true;
                break;
            case VisualEffects.FishEye:
                Fishes[curentKeyIndex % numberOfCameras].enabled = true;
                break;
            case VisualEffects.SunShaft:
                Shafts[curentKeyIndex % numberOfCameras].enabled = true;
                break;
            default:
                print("Effect Index Out of Bounds");
                break;

        }

        //for (int i = 0; i < cameras.Length; i++)
        //{
        //    if (i != curentKeyIndex)
        //    {
        //        Sepias[i].enabled = false;
        //        Blooms[i].enabled = false;
        //        Fogs[i].enabled = false;
        //        Antis[i].enabled = false;
        //    }
        //}
    }

    public void setVisualEffect(int index)
    {
        ve = (VisualEffects)index;
    }
}
