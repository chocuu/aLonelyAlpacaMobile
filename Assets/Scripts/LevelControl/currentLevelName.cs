using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currentLevelName : MonoBehaviour {

	public string currentLevelNameString;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		PlayerPrefs.DeleteAll();
#if UNITY_EDITOR
		PlayerPrefs.DeleteAll();
		// PlayerPrefs.SetInt("LevelPassed", 28);
#endif
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
