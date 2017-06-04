using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceAndDiceTrigger : MonoBehaviour {

    public List<GameObject> Enemies = new List<GameObject>();
    

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

    private void Update()
    {

      

    }






}
