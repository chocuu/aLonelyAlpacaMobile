using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Text.RegularExpressions;

public class ShowLevelBannerInCanvas : MonoBehaviour {
	Image uiImage;
	String sceneName;
	public Sprite[] LevelBannerList;

	// Use this for initialization
	void Start () {
		//Fetch the Image from the GameObject
        uiImage = GetComponent<Image>();

		//Get Level Number
		sceneName = SceneManager.GetActiveScene().name;
		Regex getNumber = new Regex(@"\d+$");
        var levelNumber = Int32.Parse(getNumber.Match(sceneName).ToString());	
        if(levelNumber == 0) levelNumber = 1;
		uiImage.overrideSprite = LevelBannerList[levelNumber - 1];

		//Begin Fading
		StartCoroutine(fadeOut(true));
	}

	IEnumerator fadeOut(bool fadeAway) {
		yield return new WaitForSeconds(1);
		if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                uiImage.color = new Color(1, 1, 1, i);
                yield return null;
            }
            uiImage.color = new Color(1, 1, 1, 0);
        }
	}
}
