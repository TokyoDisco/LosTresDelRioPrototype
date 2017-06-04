using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolZone : MonoBehaviour {
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponentInParent<NavigationScript>().target = GameObject.Find("Player").GetComponent<Transform>();
            GetComponentInParent<NavigationScript>().agent.destination = GetComponentInParent<NavigationScript>().target.position;


        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponentInParent<NavigationScript>().target = GameObject.Find("Player").GetComponent<Transform>();
            GetComponentInParent<NavigationScript>().agent.destination = GetComponentInParent<NavigationScript>().target.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponentInParent<NavigationScript>().target = null;
           // GetComponentInParent<NavigationScript>().agent.destination = GetComponentInParent<NavigationScript>().target.position;
        }
    }
}
