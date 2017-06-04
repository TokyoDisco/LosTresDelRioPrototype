using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitDmg : MonoBehaviour {
    public GameObject body;
    public GameObject target;
    public Component other;
    public Component hitbox;
    


    private void Start()
    {
        hitbox = gameObject.GetComponent<BoxCollider>();
        target = GameObject.Find("Player");
        other = target.GetComponent<CapsuleCollider>();
    }


/*    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && !(other.GetComponent<SphereCollider>()))
        {
            target.GetComponent<PlayerStatsHolder>().TakeDmg(5f);
        }
    } */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target.GetComponent<PlayerStatsHolder>().TakeDmg(gameObject.GetComponent<EnemyStats>().damage);
        }
        
    }

}
