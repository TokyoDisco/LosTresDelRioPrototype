using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationScript : MonoBehaviour {
    public Transform target;
    public NavMeshAgent agent;
    public Vector3 origPosition;

  

    void Start () {
        origPosition = GetComponent<Transform>().position;
        agent = GetComponent<NavMeshAgent>();
       if(target !=null)
        {
            agent.destination = target.position;
        }
       else
        {
            agent.destination = origPosition;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        agent = GetComponent<NavMeshAgent>();
        
        if (target != null)
        {
            agent.destination = target.position;
        }
        else
        {
            agent.destination = origPosition;
        }


    }
}
