using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCollision : MonoBehaviour
{

  public string nextLevel;
  //public AudioSource winSound;
  public AudioSource winSound;

  int sceneIndex, levelPassed;

  // Use this for initialization
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
  }

  void OnControllerColliderHit(ControllerColliderHit hit)
  {
    if (hit.collider.tag == "Goal")
    {
      winSound.Play();
      sceneIndex = SceneManager.GetActiveScene().buildIndex;
      levelPassed = PlayerPrefs.GetInt("LevelPassed");
      //Debug.Log("sceneIndex: " + sceneIndex + ", levelPassed: " + levelPassed);
      if (levelPassed < sceneIndex)
      {
        //Debug.Log("levePassed < sceneIndex :^0");
        PlayerPrefs.SetInt("LevelPassed", sceneIndex);
      }
      SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
      //Debug.Log("Player moving on to level " + nextLevel);
    }
  }

}
