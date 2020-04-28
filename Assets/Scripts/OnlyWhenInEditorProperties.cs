using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyWhenInEditorProperties : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
    #if UNITY_EDITOR
		// PlayerPrefs.DeleteAll();
		PlayerPrefs.SetInt("LevelPassed", 26);
    #endif 
    }
}
