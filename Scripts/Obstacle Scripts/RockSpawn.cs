using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawn : MonoBehaviour
{
    public GameObject dustDespawnPrefab;

    public GameObject dustRespawnPrefab;

    public GameObject rockPrefab;
    public GameObject smallRockPrefab;

    public AudioClip breakSound;
    public float breakDistance = 1f;

    public GameObject respawnZone;
    float minRespawnX, minRespawnY, minRespawnZ, maxRespawnX, maxRespawnY, maxRespawnZ;

    float respawnTime;
    public float respawnInterval = 10f;

    int numberStartingRocks;

    public AudioClip respawnSound;

    public int maxNumRocks = 15;
    int currentNumRocks;

    void Start()
    {
        float width = respawnZone.transform.localScale.x / 2f;
        minRespawnX = respawnZone.transform.position.x - width;
        maxRespawnX = respawnZone.transform.position.x + width;

        float height = respawnZone.transform.localScale.y / 2f;
        minRespawnY = respawnZone.transform.position.y - height;
        maxRespawnY = respawnZone.transform.position.y + height;

        float length = respawnZone.transform.localScale.z / 2f;
        minRespawnZ = respawnZone.transform.position.z - length;
        maxRespawnZ = respawnZone.transform.position.z + length;

        numberStartingRocks = GameObject.FindGameObjectsWithTag("Rock").Length;

        currentNumRocks = numberStartingRocks;
    }

    void Update()
    {
        if (respawnTime >= respawnInterval)
        {
            respawn(rockPrefab);
            respawnTime = 0f;
        }

        if (currentNumRocks > maxNumRocks)
        {
            Destroy(GameObject.FindGameObjectWithTag("Rock"));
            --currentNumRocks;
        }

        respawnTime += Time.deltaTime;
    }

    void respawn(GameObject rock)
    {
        if (currentNumRocks <= maxNumRocks)
        {
            float x, y, z;

            x = Random.Range(minRespawnX, maxRespawnX);
            y = Random.Range(minRespawnY, maxRespawnY);
            z = Random.Range(minRespawnZ, maxRespawnZ);


            GameObject clone = Instantiate(rock, new Vector3(x, y, z), rock.transform.rotation) as GameObject;
            GameObject dustCloud = Instantiate(dustRespawnPrefab, clone.transform.position, clone.transform.rotation) as GameObject;
            Destroy(dustCloud, 3.0f);
            AudioSource.PlayClipAtPoint(respawnSound, clone.transform.position);

            Destroy(clone, 100f);
            ++currentNumRocks;
        }
    }

    public void breakSphere(GameObject rock, float x, float y, float z)
    {
        float x1 = Random.Range(x - (2f * breakDistance), x - breakDistance);
        float z1 = Random.Range(z - (2f * breakDistance), z - breakDistance);

        despawn(rock);

        if (currentNumRocks <= maxNumRocks)
        {
            GameObject clone = Instantiate(smallRockPrefab, new Vector3(x1, y, z1), rock.transform.rotation) as GameObject;
            GameObject dustCloud = Instantiate(dustDespawnPrefab, clone.transform.position, clone.transform.rotation) as GameObject;
            Destroy(dustCloud, 3.0f);
            AudioSource.PlayClipAtPoint(breakSound, clone.transform.position);

            float x2 = Random.Range(x + breakDistance, x + (2f * breakDistance));
            float z2 = Random.Range(z + breakDistance, z + (2f * breakDistance));

            GameObject clone2 = Instantiate(smallRockPrefab, new Vector3(x2, y, z2), rock.transform.rotation) as GameObject;
            GameObject dustCloud2 = Instantiate(dustDespawnPrefab, clone2.transform.position, clone2.transform.rotation) as GameObject;
            Destroy(dustCloud2, 3.0f);
            AudioSource.PlayClipAtPoint(breakSound, clone.transform.position);

            Destroy(clone, 20f);
            Destroy(clone2, 20f);
            currentNumRocks += 2;
        }
    }

    public void despawn(GameObject boluder)
    {
        GameObject dustCloud = Instantiate(dustDespawnPrefab, boluder.transform.position, boluder.transform.rotation) as GameObject;
        Destroy(dustCloud, 3.0f);

        Destroy(boluder);
        --currentNumRocks;

    }
}
