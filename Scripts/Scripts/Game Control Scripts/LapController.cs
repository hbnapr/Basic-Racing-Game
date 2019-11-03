using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapController : MonoBehaviour {

    GameController manager;
    public bool debug = false;
    public LapController nextNode;
    public LapController previousNode;
    bool passed = false;
    public bool final = false;
    public int wayPoint;

    // Use this for initialization
    void Start () {
        GameObject m = GameObject.FindGameObjectWithTag("GameController");
        if(m != null && !debug)
        {
            manager = m.GetComponent<GameController>();

        }
        else
        {
            debug = true;
        }
    }
	
    public void OnTriggerEnter(Collider other)
    {
        //print(other.gameObject.name + " " + other.gameObject.tag);
        if(other.gameObject.tag == "Player")
        {
            print("In tag " + gameObject.name + " " + determinePass());
            if (!debug)
            {
                if (determinePass())
                {
                    if (final)
                    {
                        manager.waypoint = 0;
                        manager.incrementLap();
                        foreach (var item in GameObject.FindGameObjectsWithTag("Checkpoint"))
                        {
                            item.GetComponent<LapController>().passed = false;
                        }
                    }
                    else
                    {
                        passed = true;
                        previousNode.passed = false;
                        manager.waypoint = wayPoint+1;
                        print("Passed Waypoint: " + wayPoint + " new Waypoint = " + manager.waypoint);
                    }
                }
            }
            else
            {
                print("lap increase");
            }
        }
    }

    bool determinePass()
    {
        bool pass = false;
        if (previousNode.final)
        {
            pass = true;
            //print("final");
        }
        else {
            if (final)
            {
                pass = previousNode.passed;
            }
            else
            {
                foreach (var item in GameObject.FindGameObjectsWithTag("Checkpoint"))
                {
                    //print(item.name + " " + item.GetComponent<LapController>().previousNode.passed);
                    if (item.GetComponent<LapController>().previousNode.passed)
                    {
                        pass = true;
                        break;
                    }
                }
            }
            
        }

        return pass;
    }
}
