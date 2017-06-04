using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyScanZone : MonoBehaviour {


    public List<GameObject> Enemies = new List<GameObject>();
    public GameObject TargetForPlayer;
    public GameObject TargetPointer;

    private void OnTriggerEnter(Collider other)
    {
        int innerCounter = 0;
        if(other.gameObject.tag == "Enemy")
        {
            if(Enemies.Count > 0)
            {
                for(int i = 0; i < Enemies.Count; i++)
                {
                    if(Enemies[i] == other.gameObject)
                    {
                        innerCounter = innerCounter + 1;
                    }
                }

                if(innerCounter == 0)
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
        if(other.gameObject.tag == "Enemy")
        {
            for(int i = 0; i < Enemies.Count; i++)
            {
                if(Enemies[i] == other.gameObject)
                {
                    Enemies.RemoveAt(i);
                }
            }
        }
    }

    private void Update()
    {
        
        if (Enemies.Count > 0)
        {
            TargetPointer.SetActive(true);
            if (Enemies[0] != null)
            {
                if (TargetForPlayer == null)
                {
                    TargetForPlayer = Enemies[0];
                }
                Vector3 moveTo = new Vector3(0, 1.5f, 0);
                Vector3 SavedPosition = TargetForPlayer.GetComponent<Transform>().position + moveTo;
                TargetPointer.GetComponent<Transform>().position = SavedPosition;
            }
        }
        else
        {
               
            TargetForPlayer = null;
            GameObject.Find("Player").GetComponent<PlayerStatsHolder>().target = null;
            TargetPointer.SetActive(false);
        }

    }


    public void TargertsSwap()
    {
        int TargetIndex = 0;
        for (int i = 0; i < Enemies.Count; i++)
        {
            if(TargetForPlayer == Enemies[i])
            {
                TargetIndex = i;
            }
        }

        if(TargetIndex == Enemies.Count-1)
        {
            TargetIndex = 0;
            TargetForPlayer = Enemies[0];
        }
        else
        {
            TargetForPlayer = Enemies[TargetIndex + 1];
        }



    }


}
