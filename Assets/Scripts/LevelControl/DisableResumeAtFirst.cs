﻿using UnityEngine;
using UnityEngine.UI;

public class DisableResumeAtFirst : MonoBehaviour
{

	[SerializeField] GameObject resumeButton;
    [SerializeField] RectTransform startButton;
    [SerializeField] GameObject lvlButton;


    // Start is called before the first frame update
    void Start()
    {
        int level = PlayerPrefs.GetInt("LevelPassed") + 1;
        if(level > GoHome.numLevels + 1 || level <= 1) { // out of bounds level
			resumeButton.SetActive(false);
            startButton.anchoredPosition = new Vector3(0, 40, 0);
            lvlButton.SetActive(false);
    	}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
