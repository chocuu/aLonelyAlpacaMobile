﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCollision : MonoBehaviour {

	public string nextLevel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnControllerColliderHit(ControllerColliderHit hit){
		if(hit.collider.tag == "Goal"){
			SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
			LoggingManager.instance.RecordLevelEnd(); //sends data to server that player won the level
			Debug.Log("Player moving on to level " + nextLevel);
		}
	}

}
