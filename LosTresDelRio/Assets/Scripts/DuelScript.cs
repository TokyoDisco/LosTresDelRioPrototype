using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DuelScript : MonoBehaviour {

    public GameObject MainCamera;
    public GameObject DuelCamera;
    public GameObject Zoom1;
    public GameObject Zoom2;
    public Vector3 origPos;
    public Quaternion origRotation;
    public AudioSource DuelMusic;
    public GameObject QTEbtn;
    public GameObject Opponent;
    public GameObject Player;
    public AudioSource Shoot;

    public bool win;
    bool QTE;

	// Use this for initialization
	void Start () {
        //
        QTE = false; 
        win = false;
        MainCamera.SetActive(false);
        QTEbtn.SetActive(false);
        DuelMusic.Play();
        origPos = DuelCamera.transform.position;
        origRotation = DuelCamera.transform.rotation;
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        StartCoroutine(DuelFlow());
        	
	}
	
	// Update is called once per frame
	void Update () {
        DuelMusic.Play();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot.Play();
            StopAllCoroutines();
            win = true;
        }
        if (win)
        {
            DuelWon();
        }
       


    }


    public  IEnumerator DuelFlow()
    {
        DuelMusic.Play();
        yield return new WaitForSeconds(5f);
        DuelMusic.Play();
        DuelCamera.transform.position = Zoom1.transform.position;
        DuelCamera.transform.rotation = Zoom1.transform.rotation;
        yield return new WaitForSeconds(3f);
        DuelMusic.Play();
        DuelCamera.transform.position = Zoom2.transform.position;
        DuelCamera.transform.rotation = Zoom2.transform.rotation;
        yield return new WaitForSeconds(3f);
        DuelMusic.Play();
        DuelCamera.transform.position = origPos;
        DuelCamera.transform.rotation = origRotation;
        QTEbtn.SetActive(true);
     //   QTE = true;
        yield return new WaitForSeconds(3f);
        
        DuelMusic.Play();
        DuelLost();

    }

    public void QTEtrigger()
    {
        win = true;
    }

    public void DuelLost()
    {
        Player.GetComponent<PlayerStatsHolder>().Death();
    }


    public void DuelWon()
    {
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        if (Opponent != null)
        {
            if (Opponent.name == "DiegoTheBlueMouth")
            {
                Player.GetComponent<QuestTrigger>().QuestTrigger4();
            }

            else
            {
                Player.GetComponent<QuestTrigger>().QuestTrigger3();
            }
            Destroy(Opponent);

            if (Opponent.name == "JuenosIronSkin")
            {
                Player.GetComponent<QuestTrigger>().QuestTrigger5();
            }
        }
        MainCamera.SetActive(true);
        gameObject.SetActive(false);
    }

}
