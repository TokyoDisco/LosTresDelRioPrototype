using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {
    public GameObject CloneCubicCatus;
    public bool PlayerNear;
    public bool SpawnCDoff;
    public int SpawnCounter;


	// Use this for initialization
	void Start () {
        PlayerNear = false;
        SpawnCDoff = true;
        SpawnCounter = 0;
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerNear = false;
        }
    }

    // Update is called once per frame
    void Update () {
        if(SpawnCDoff && PlayerNear)
        {
            SpawnCounter = SpawnCounter + 1;
            Spawning();
            SpawnCDoff = false;
            StartCoroutine(SpawnTimeWait());
        }
		
	}

    public void Spawning()
    {
        GameObject NewCubicCatus;
        NewCubicCatus = (GameObject)Instantiate(GameObject.Find("CubicCatusClone"),transform.position,transform.rotation);
        NewCubicCatus.GetComponent<Transform>().position = gameObject.transform.position;
        NewCubicCatus.name = "CactusCubic" + SpawnCounter;
        NewCubicCatus.GetComponent<EnemyStats>().level = GameObject.Find("Player").GetComponent<PlayerStatsHolder>().level;
        
    }

    public void DestroySpawn()
    {
        Destroy(GameObject.Find(gameObject.name));
    }

    IEnumerator SpawnTimeWait()
    {
        yield return new WaitForSeconds(15f);
        SpawnCDoff = true;
    }
}
