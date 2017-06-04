using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaDelChiliEffect : MonoBehaviour {
    public float radius;
    public float power = 10;
    public List<GameObject> Enemies = new List<GameObject>();
    bool timeToBoom;
    bool timeToDestoryObject;
    public AudioSource BoomSound;

    private void OnTriggerEnter(Collider other)
    {
        int innerCounter = 0;
        if (other.gameObject.tag == "Enemy")
        {
            if (Enemies.Count > 0)
            {
                for (int i = 0; i < Enemies.Count; i++)
                {
                    if (Enemies[i] == other.gameObject)
                    {
                        innerCounter = innerCounter + 1;
                    }
                }

                if (innerCounter == 0)
                {
                    Enemies.Add(other.gameObject);
                }
            }
            else
            {
                Enemies.Add(other.gameObject);
            }






        }



    }

    private void OnTriggerStay(Collider other)
    {
        int innerCounter = 0;
        if (other.gameObject.tag == "Enemy")
        {
            if (Enemies.Count > 0)
            {
                for (int i = 0; i < Enemies.Count; i++)
                {
                    if (Enemies[i] == other.gameObject)
                    {
                        innerCounter = innerCounter + 1;
                    }
                }

                if (innerCounter == 0)
                {
                    Enemies.Add(other.gameObject);
                }
            }
            else
            {
                Enemies.Add(other.gameObject);
            }






        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                if (Enemies[i] == other.gameObject)
                {
                    Enemies.RemoveAt(i);
                }
            }
        }
    }
    // Use this for initialization
    void Start () {
        radius = GetComponent<SphereCollider>().radius;
        timeToBoom = false;
        timeToDestoryObject = false;
        if (name != "BombaDelCopia")
        {
            StartCoroutine(timeToExplosion());
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        if(timeToBoom)
        {
           
            explosion();
           
        }

        if(timeToDestoryObject)
        {
            Destroy(GameObject.Find(name));
        }
		
	}
    IEnumerator timeToExplosion()
    {
        
        
        yield return new WaitForSeconds(3f);
        timeToBoom = true;
        StartCoroutine(timeToDestory());
    }
    IEnumerator timeToDestory()
    {
        GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(3f);
        timeToDestoryObject = true;
    }

    public void explosion()
    {
        BoomSound.Play();
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach(Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(rb != null)
            {
                
                rb.AddExplosionForce(power*15, explosionPos, radius*10);
            }
        }
        foreach(GameObject enemy in Enemies)
        {
            
            if (enemy != null)
            {
                
                enemy.GetComponent<EnemyStats>().TakeDmg(100);
            }
        }
        timeToBoom = false;
    }
}
