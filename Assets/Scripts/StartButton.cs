using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{

  public void startGame()
  {
    Destroy(GameObject.Find("MusicTime"));
    SceneManager.LoadScene("B0.5 - Intro", LoadSceneMode.Single);
  }

  public void resumeGame()
  {
    int level = PlayerPrefs.GetInt("LevelPassed") + 1;
    Debug.Log("level resume: " + level);
    if(level > 28 || level <= 1) { // out of bounds level
    	Destroy(GameObject.Find("MusicTime"));
		SceneManager.LoadScene("B0.5 - Intro", LoadSceneMode.Single);
    }
    else if(level == 28) { // last level
    	SceneManager.LoadScene("B" + (PlayerPrefs.GetInt("LevelPassed")), LoadSceneMode.Single);
    }
	else {
		SceneManager.LoadScene("B" + (PlayerPrefs.GetInt("LevelPassed") + 1), LoadSceneMode.Single);
	}
  }
}
