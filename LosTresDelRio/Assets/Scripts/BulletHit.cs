using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour {

    private void Update()
    {
        if (name != "Bullet")
        {
            StartCoroutine(FadeTime());

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(name != "Bullet" && other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyStats>().TakeDmg(GameObject.Find("Player").GetComponent<PlayerStatsHolder>().damage);
        }
    }


    IEnumerator FadeTime()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(GameObject.Find(name));
    }
}
