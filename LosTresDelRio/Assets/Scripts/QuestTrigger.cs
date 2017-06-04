using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTrigger : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && gameObject.name == "QuestTrigger1")
        {
            other.gameObject.GetComponent<PlayerStatsHolder>().InfoText3.GetComponentInChildren<Text>().text = "Head to the Los Baros Saloon and meet with local barman" + "\n" + "To discuss futher actions";
            other.gameObject.GetComponent<PlayerStatsHolder>().experience = other.gameObject.GetComponent<PlayerStatsHolder>().experience + 60;
        }

        if(other.gameObject.tag == "Player" && gameObject.name == "CameraSwitch1")
        {
            GameObject.Find("HandheldCamera").GetComponent<Transform>().position = GameObject.Find("SecondCameraPosition").GetComponent<Transform>().position;
        }

               
    }



    public void QuestTrigger2()
    {
        
            GameObject.Find("Player").GetComponent<PlayerStatsHolder>().InfoText3.GetComponentInChildren<Text>().text = "Defeat Barman Fabio The Cactus in Duel";
            GameObject.Find("Player").gameObject.GetComponent<PlayerStatsHolder>().experience = GameObject.Find("Player").gameObject.GetComponent<PlayerStatsHolder>().experience + 30;
            GameObject.Find("Player").gameObject.GetComponent<PlayerStatsHolder>().healingTaco = GameObject.Find("Player").gameObject.GetComponent<PlayerStatsHolder>().healingTaco + 1;
    
    }


    public void QuestTrigger3()
    {
        GameObject.Find("Player").GetComponent<PlayerStatsHolder>().InfoText3.GetComponentInChildren<Text>().text = "Fabio is dead now you must kill Diego. He is behind you at the end of road";
        GameObject.Find("Player").gameObject.GetComponent<PlayerStatsHolder>().experience = GameObject.Find("Player").gameObject.GetComponent<PlayerStatsHolder>().experience + 100;
        GameObject.Find("Player").gameObject.GetComponent<PlayerStatsHolder>().healingTaco = GameObject.Find("Player").gameObject.GetComponent<PlayerStatsHolder>().healingTaco + 3;
    }

    public void QuestTrigger4()
    {
        GameObject.Find("Player").GetComponent<PlayerStatsHolder>().InfoText3.GetComponentInChildren<Text>().text = "Only one left Juenos. He will wait you in mines south from here";
        GameObject.Find("Player").gameObject.GetComponent<PlayerStatsHolder>().experience = GameObject.Find("Player").gameObject.GetComponent<PlayerStatsHolder>().experience + 100;
        GameObject.Find("Player").gameObject.GetComponent<PlayerStatsHolder>().healingTaco = GameObject.Find("Player").gameObject.GetComponent<PlayerStatsHolder>().healingTaco + 3;
    }

    public void QuestTrigger5()
    {
        gameObject.GetComponent<PlayerStatsHolder>().Win();   
    }
}
