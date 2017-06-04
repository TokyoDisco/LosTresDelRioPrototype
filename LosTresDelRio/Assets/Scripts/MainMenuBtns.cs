using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBtns : MonoBehaviour {
    public GameObject SoundText;
    public AudioSource plum;
    public GameObject Camera1;
    public GameObject Camera2;
    public GameObject ControlScheme;
    public GameObject HistoryText;

	public void SoundOff()
    {
        plum.Play();
        if(AudioListener.volume == 1.0)
        {
            AudioListener.volume = 0;
            SoundText.GetComponentInChildren<Text>().text = "Sound on";
        }
        else
        {
            AudioListener.volume = 1.0f;
            SoundText.GetComponentInChildren<Text>().text = "Sound off";
        }
    }

    public void CameraChange()
    {
       
        Camera1.SetActive(false);
        Camera2.SetActive(true);
        StartCoroutine(WaitForRead());
    }

    public IEnumerator WaitForRead()
    {
        yield return new WaitForSeconds(3f);
        HistoryText.SetActive(false);
        ControlScheme.SetActive(true);
        StartCoroutine(WaitForControl());
    }



    public void GameQuit()
    {
        //  plum.Play();
        Application.Quit();
    }

    public void LosBarosLoad()
    {
        plum.Play();
        SceneManager.LoadSceneAsync("LosBaros");
        SceneManager.UnloadSceneAsync("MainMenu");
    }

    public IEnumerator WaitForControl()
    {
        yield return new WaitForSeconds(5f);
        LosBarosLoad();
    }

    public void Restart()
    {
        SceneManager.LoadScene("LosBaros");
        SceneManager.UnloadSceneAsync("LosBaros");
        
    }


}
