using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeLevelBanner : MonoBehaviour, IPointerEnterHandler {
	public Image levelBanner;
	public Image levelImagePreview;
	int defaultLevelNumber;
	int levelPassed;

	// Use this for initialization
	void Start () {
		levelPassed = GameObject.Find("LevelMenuController").GetComponent<LevelMenuControllerScript>().levelPassed;
		if(levelPassed < 28) defaultLevelNumber = levelPassed + 1;
		if(levelPassed == 28) defaultLevelNumber = levelPassed;
		levelBanner = GameObject.Find("LevelBanner").GetComponent<Image>();
		levelBanner.sprite = GameObject.Find("banner" + defaultLevelNumber).GetComponent<SpriteRenderer>().sprite;

		levelImagePreview = GameObject.Find("ImagePreview").GetComponent<Image>();
		levelImagePreview.sprite = GameObject.Find("level" + defaultLevelNumber).GetComponent<SpriteRenderer>().sprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter(PointerEventData eventData)
     {
        string getLevelNumber = Regex.Match(gameObject.name, @"\d+").Value;
		if (defaultLevelNumber < int.Parse(getLevelNumber)){
			levelBanner.sprite = GameObject.Find("banner" + getLevelNumber).GetComponent<SpriteRenderer>().sprite;
			levelImagePreview.sprite = GameObject.Find("lockedLevel").GetComponent<SpriteRenderer>().sprite;
		}
		else {
			levelBanner.sprite = GameObject.Find("banner" + getLevelNumber).GetComponent<SpriteRenderer>().sprite;
			levelImagePreview.sprite = GameObject.Find("level" + getLevelNumber).GetComponent<SpriteRenderer>().sprite;
		}
     }
}
