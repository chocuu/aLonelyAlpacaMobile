using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoHome : MonoBehaviour
{
  private const int numLevels = 26;
  public string menuLevel = "B0 - Menu";
  public string levelSelect = "Level Select Menu Mobile";
  public GameObject confirmScreen;
  GameObject currentLevel;
  currentLevelName currentLevelScript;
  GameObject previousLevel;
  currentLevelName previousLevelScript;
  bool confirm_selection = false; // false = goToLevelSelect, true = goHome 

  public void goHome() {
    SceneManager.LoadScene(menuLevel, LoadSceneMode.Single);
  }

  public void restart() {
    confirm_selection=true;
    confirmScreen.SetActive(true);
  }

  public void goToLevelSelect() {
    confirm_selection=false;
    confirmScreen.SetActive(true);
  }

  public void goToLevelSelectFromMainMenu() {
    // Set previous screen to main menu 
    currentLevelScript = GameObject.Find("GameObject").GetComponent<currentLevelName>();
    currentLevelScript.currentLevelNameString = SceneManager.GetActiveScene().name;
    SceneManager.LoadSceneAsync(levelSelect, LoadSceneMode.Single);
  }

  // For Level Complete Screen, when don't need confirm screen
  public void restart_Bypass() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    return;
  }
  public void goToLevelSelect_Bypass() {
    SceneManager.LoadSceneAsync(levelSelect, LoadSceneMode.Single);
    return;
  }

  public void confirm() {
    if(confirm_selection) {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    } else {
      SceneManager.LoadSceneAsync(levelSelect, LoadSceneMode.Single);
    }
    confirmScreen.SetActive(false);
  }

  public void closeConfirm() {
    confirmScreen.SetActive(false);
  }

  private void goLastPassedLevel() {
    int level = PlayerPrefs.GetInt("LevelPassed") + 1;
    if(level > numLevels || level <= 1) { // out of bounds level
      Destroy(GameObject.Find("MusicTime"));
      SceneManager.LoadScene("B0.5 - Intro", LoadSceneMode.Single);
    }
    else if(level == numLevels) { // last level
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
