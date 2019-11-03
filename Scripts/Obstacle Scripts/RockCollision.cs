using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollision : MonoBehaviour {

    public GameObject terrian;
    public GameObject dustPrefab;
    public AudioClip groundHitClip;
    GameController manager;
    RockSpawn rs;

    void Start()
    {
        GameObject m = GameObject.FindWithTag("GameController");
        if(m !=null){
            manager = m.GetComponent<GameController>();
        }
        rs = GameObject.Find("RockSpawnerAndDespawner").GetComponent<RockSpawn>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Rock" && !collision.gameObject.name.Contains("Small"))
        {
            Vector3 t = collision.gameObject.transform.position;
            rs.breakSphere(collision.gameObject, t.x, t.y, t.z);
        }

        // Dust Clouds
        if(collision.gameObject == terrian)
        {
            hitTerrain();
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // Dust Clouds
        if (collision.gameObject == terrian)
        {
            //print(gameObject.name + "Hit Terrain");
            hitTerrain();
            
        }
    }


    void hitTerrain()
    {
        int x = Random.Range(0, 1000);
        if (x <= 3)
        {
            AudioSource.PlayClipAtPoint(groundHitClip, gameObject.transform.position);

            GameObject dustCloud = Instantiate(dustPrefab, transform.position, transform.rotation) as GameObject;
            Destroy(dustCloud, 1.5f);
        }
    }
}
