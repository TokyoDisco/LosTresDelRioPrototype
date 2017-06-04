using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {
    public BaseEnemy enemyStats;
    public GameObject me;
    public GameObject target;
    public int healthPoints;
    public float damage;
    public GameObject bagOfPesos;
    
    public int level;





    // Use this for initialization
    void Start()
    {

        healthPoints = 150;
        damage = 10 ;
        
        level = 1;


    }

    // Update is called once per frame
    void Update()
    {

        if(healthPoints <= 0)
        {
            Death();
        }

        damage = level * 10;
        

    }



    public void DoDmg()
    {

    }

    public void TakeDmg(float damageAmount)
    {
        healthPoints = (int)(healthPoints - damageAmount);
    }

    public void Death()
    {
        GameObject dropBag = (GameObject)Instantiate(bagOfPesos, bagOfPesos.transform,true);
        dropBag.transform.position = gameObject.transform.position;
        int RandomID = Random.Range(0, 100);
        dropBag.name = "bagfullofPesos " + RandomID;
        target.GetComponent<PlayerStatsHolder>().experience = target.GetComponent<PlayerStatsHolder>().experience +(10 * level);
        target.GetComponent<PlayerStatsHolder>().Score = target.GetComponent<PlayerStatsHolder>().Score + 10;
        target.GetComponent<PlayerStatsHolder>().killsCount = target.GetComponent<PlayerStatsHolder>().killsCount + 1;
        this.GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("EnemyScanner").GetComponent<EnemyScanZone>().Enemies.Clear();
        Destroy(GameObject.Find(name));
    }
}
