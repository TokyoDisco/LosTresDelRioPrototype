using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TalkToUI : MonoBehaviour {
    public GameObject buyBtn1;
    public GameObject buyBtn2;
    public GameObject buyBtn3;
    public GameObject buyBtn4;
    public GameObject ExitBtn;
    public GameObject DuelBtn;
    public GameObject player;
    public GameObject MainCamera;
    public GameObject TalkToCamera;
    public GameObject DuelCamera;
    public GameObject Fabio;
    public Vector3 DuelPosEnemy;
    public Vector3 DuelPosPlayer;
    public Quaternion DuelPosEnemyRotation;
    public Quaternion DuelPosPlayerRotation;
    public GameObject TalkToUI1;
    public GameObject TalkToUI2;
    public GameObject TalkToUI3;


    // Use this for initialization
    void Start () {
        DuelPosEnemy = GameObject.Find("Duel1OpponentPosition").transform.position;
        DuelPosPlayer = GameObject.Find("Duel1PlayerPosition").transform.position;
        DuelPosEnemyRotation = GameObject.Find("Duel1OpponentPosition").transform.rotation;
        DuelPosPlayerRotation = GameObject.Find("Duel1PlayerPosition").transform.rotation;

    }
	
	// Update is called once per frame
	void Update () {
		
	}



    public void buyTaco()
    {
        if(player.GetComponent<PlayerStatsHolder>().gold >= 50 )
        {
            player.GetComponent<PlayerStatsHolder>().gold = player.GetComponent<PlayerStatsHolder>().gold - 50;
            player.GetComponent<PlayerStatsHolder>().healingTaco = player.GetComponent<PlayerStatsHolder>().healingTaco + 1;
        }
    }

    public void buyAmmoSize()
    {
        if(player.GetComponent<PlayerStatsHolder>().gold >= 500)
        {
            player.GetComponent<PlayerStatsHolder>().gold = player.GetComponent<PlayerStatsHolder>().gold - 500;
            player.GetComponent<PlayerStatsHolder>().maxammo = player.GetComponent<PlayerStatsHolder>().maxammo + 1;
            buyBtn2.GetComponent<Button>().interactable = false;
            buyBtn2.GetComponentInChildren<Text>().text = "Sold Out";
        }
    }

    public void buyReloadSpeed()
    {
        if(player.GetComponent<PlayerStatsHolder>().gold >= 1500)
        {
            player.GetComponent<PlayerStatsHolder>().gold = player.GetComponent<PlayerStatsHolder>().gold - 1500;
            player.GetComponent<PlayerStatsHolder>().CDreduction = 0.2f;
            buyBtn3.GetComponent<Button>().interactable = false;
            buyBtn3.GetComponentInChildren<Text>().text = "Sold Out";
        }
    }

    public void buyBonusHP()
    {
        if(player.GetComponent<PlayerStatsHolder>().gold >= 500)
        {
            player.GetComponent<PlayerStatsHolder>().gold = player.GetComponent<PlayerStatsHolder>().gold - 500;
            player.GetComponent<PlayerStatsHolder>().MaxHP = player.GetComponent<PlayerStatsHolder>().MaxHP + 100;
            buyBtn4.GetComponent<Button>().interactable = false;
            buyBtn4.GetComponentInChildren<Text>().text = "Sold Out";
        }
    }

    public void Duel()
    {
        
        player.transform.position = DuelPosPlayer;
        
        DuelCamera.SetActive(true);

        DuelCamera.GetComponent<DuelScript>().Opponent = Fabio;
        Fabio.transform.position = DuelPosEnemy;
        Fabio.transform.rotation = DuelPosEnemyRotation;
        Exit();



    }


    public void Exit()
    {
        MainCamera.SetActive(true);
        TalkToCamera.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && gameObject.name == "Fabio The Barman Cactus")
        {
            TalkToCamera.SetActive(true);
            MainCamera.SetActive(false);
            TalkToUI1.SetActive(true);
            TalkToUI2.SetActive(false);
            TalkToUI3.SetActive(false);
        }
        else
        {
            if (other.gameObject.tag == "Player" && gameObject.name == "DiegoTheBlueMouth")
            {
                TalkToCamera.SetActive(true);
                MainCamera.SetActive(false);
                TalkToUI1.SetActive(false);
                TalkToUI2.SetActive(true);
                TalkToUI3.SetActive(false);
            }
            else
            {
                if (other.gameObject.tag == "Player" && gameObject.name == "JuenosIronSkin")
                {
                    TalkToCamera.SetActive(true);
                    MainCamera.SetActive(false);
                    TalkToUI1.SetActive(false);
                    TalkToUI2.SetActive(false);
                    TalkToUI3.SetActive(true);
                }
            }
        }
    }

}
