using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoHome : MonoBehaviour
{
  public string menuLevel = "B0 - Menu";
  public string levelSelect = "Level Select Menu Mobile";
  GameObject currentLevel;
  currentLevelName currentLevelScript;
  GameObject previousLevel;
  currentLevelName previousLevelScript;

  public void goHome() {
    SceneManager.LoadScene(menuLevel, LoadSceneMode.Single);
  }

  public void goToLevelSelect() {
    currentLevel = GameObject.Find("GameObject");
    currentLevelScript = currentLevel.GetComponent<currentLevelName>();
    currentLevelScript.currentLevelNameString = SceneManager.GetActiveScene().name;
    SceneManager.LoadSceneAsync(levelSelect, LoadSceneMode.Single);

  }

  private void goLastPassedLevel() {
    int level = PlayerPrefs.GetInt("LevelPassed") + 1;
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

  public void resumeGame()
  {
    string prevLvl = PlayerPrefs.GetString("lastLoadedScene");
    if(Application.CanStreamedLevelBeLoaded(prevLvl)
      && !prevLvl.Equals(menuLevel) 
      && !prevLvl.Equals(levelSelect))
        SceneManager.LoadScene(prevLvl, LoadSceneMode.Single);
    else 
      goLastPassedLevel();
  }

  public void goBackToPreviousLevel() {
    previousLevel = GameObject.Find("GameObject");
    if (previousLevel == null) goHome();
    else {
      previousLevelScript = previousLevel.GetComponent<currentLevelName>();
      SceneManager.LoadScene(previousLevelScript.currentLevelNameString, LoadSceneMode.Single);
    }
  }
}
