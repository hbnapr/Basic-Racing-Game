using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RaceAI : MonoBehaviour
{
    //Agent will walk/run to this GameObject
    public GameObject player;
    NavMeshAgent agent;
    Transform goal;

    public List<GameObject> targets;
    GameObject target;

    public int index;

    GameController manager;

    bool begun;

    void Start()
    {

        GameObject m = GameObject.FindGameObjectWithTag("GameController");

        if (m != null)
        {
            manager = m.GetComponent<GameController>();
            manager.StartRace(GetComponent<RaceAI>());

            agent = GetComponent<NavMeshAgent>();

            index = 0;

            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            agent = GetComponent<NavMeshAgent>();

            index = 0;

            player = GameObject.FindGameObjectWithTag("Player");

            begun = true;

            getTarget();

            agent.SetDestination(goal.position);
        }
    }

    public void BeginRace()
    {
        getTarget();

        agent.SetDestination(goal.position);
        index--;

        begun = true;
    }

    void Update()
    {
        if (begun)
        {
            if (agent.remainingDistance != 0)
            {
                agent.SetDestination(goal.position);
                //print(agent.destination);
            }
            else
            {
                index++;
                getTarget();
                agent.SetDestination(goal.position);

            }

            if (manager != null)
            {
                DeterminePlace();
            }

        }

    }

    public void getTarget()
    {
        goal = targets[index%targets.Count].transform;
        print(goal.name + " " + index);
        //print(goal.position);
        //print(agent.remainingDistance);

    }

    void DeterminePlace()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        
        int playerLap = manager.lap;
        int agentLap = (index / 4) + 1;

        print("Player Lap: " + playerLap + " Agent Lap: " + agentLap);       

        if (agentLap != playerLap)
        {
            if (agentLap > playerLap)
            {
                manager.UpdatePlace(2);
            }else
            {
                manager.UpdatePlace(1);
            }
        }
        else
        {
            int playerPoint = manager.waypoint;
            int agentPoint = (index % 4);

            print("Player Point: " + playerPoint + " Agent Point: " + agentPoint);

            if (agentPoint != playerPoint)
            {
                if (agentPoint > playerPoint)
                {
                    manager.UpdatePlace(2);
                }else
                {
                    manager.UpdatePlace(1);
                }
            }
            else
            {
                float playerDistance = Vector3.Distance(player.transform.position, goal.position);
                float agentDistance = Vector3.Distance(transform.position, goal.position);

                //print("Player: " + playerDistance + " AI: " + agentDistance);

                if (playerDistance > agentDistance)
                {
                    manager.UpdatePlace(2);
                }
                else
                {
                    manager.UpdatePlace(1);
                }

            }

        }
    }
}
