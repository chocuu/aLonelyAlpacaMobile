using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
  public void startGame()
  {
    GameObject gmobjct = GameObject.FindWithTag("GameManager");
    Destroy(GameObject.Find("MusicTime"));
    SceneManager.LoadScene("B0.5 - Intro", LoadSceneMode.Single);
  }

  public void resumeGame()
  {
  	GameObject gmobjct = GameObject.FindWithTag("GameManager");
    Destroy(GameObject.Find("MusicTime"));
    int level = PlayerPrefs.GetInt("LevelPassed") + 1;
    Debug.Log("level resume: " + level);
    if(level >= 28 || level < 0)
		SceneManager.LoadScene("B0.5 - Intro", LoadSceneMode.Single);
	else 
		SceneManager.LoadScene("B" + (PlayerPrefs.GetInt("LevelPassed") + 1), LoadSceneMode.Single);
  }
}
