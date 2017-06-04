using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootpickup : MonoBehaviour {
    public    GameObject player;
    public int Gold;
    public bool Taco;

    public void Start()
    {
        Gold = UnityEngine.Random.Range(1, 200);
        int i = UnityEngine.Random.Range(0, 2);
        if (i > 0)
        {
            Taco = true;
        }
        else
        {
            Taco = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            BagPickUp(Gold);

        }
    }

    public void BagPickUp(int gold)
    {
        if (Taco)
        {
            player.GetComponent<PlayerStatsHolder>().healingTaco = player.GetComponent<PlayerStatsHolder>().healingTaco + 1;
        }
        player.GetComponent<PlayerStatsHolder>().gold = player.GetComponent<PlayerStatsHolder>().gold + gold;
        Destroy(GameObject.Find(gameObject.name));
    }


}
