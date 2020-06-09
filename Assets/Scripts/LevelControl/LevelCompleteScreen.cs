using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompleteScreen : MonoBehaviour
{
	private const int numberOfLevels = 26;
	public Sprite[] levelBannersArray = new Sprite[numberOfLevels];
	public GameObject[] uiElementsArray = new GameObject[5];
	// starsArray contains sprites for: Empty Star, Half-Star, Full Star, Star w/ face
	public Sprite[] starSpritesArray = new Sprite[4]; 
	// starsArray is the 3 references to the stars' images (left to right). Initialized to empty.
	public Image [] starsArray = new Image[3];
	public Text currentScore;
	public Text bestScore;
	public Image levelBanner;
	int level;
	ScoreboardController scoreboardController;

	// Called when level is completed, opens level complete screen
	public void CompletedLevel(int lev, ScoreboardController sc) {
		level = lev;
		scoreboardController = sc;
		levelBanner.sprite = levelBannersArray[lev-1];
		scoreboardController.processFinalScore(level);
		currentScore.text = scoreboardController.getNumMovesMade() 
							+ "\n" + scoreboardController.getTotalTime();
		bestScore.text = "| " + scoreboardController.getBestNumMovesMade()
						 + "\n| " + scoreboardController.getBestTotalTime();
		drawStars(scoreboardController.getScore());
		// Update Farther Reached Level stat
		if(PlayerPrefs.GetInt("LevelPassed") < level) {
			PlayerPrefs.SetInt("LevelPassed", level);
		}
		// Disable other UI elements
		foreach(GameObject o in uiElementsArray) {
			if(o != null) o.SetActive(false);
		}
	}

	// score is 1 - 6 (1=0.5 stars, 6=3 stars)
	// starSprites: 0=empty, 1=half, 2=full, 3=full with face
	private void drawStars(int score) {
		Debug.Log("SCORE IS: " + score);
		if(score > 2) // score > 1 star
			starsArray[0].sprite = starSpritesArray[2];
		if(score > 4) // score > 2 star
			starsArray[1].sprite = starSpritesArray[2];
		if(score == 3) // 1.5 star
			starsArray[1].sprite = starSpritesArray[1];
		if(score == 5) // 2.5 star
			starsArray[2].sprite = starSpritesArray[1];
		if(score == 6) { // 3 star
			starsArray[1].sprite = starSpritesArray[3];
			starsArray[2].sprite = starSpritesArray[2];
		}
	}

	/* Called by next level button in level complete screen
	**/
	public void GoNextLevel() {
		// Check if we're on the final level -- if yes load credits sequence instead.
		FinalWinBlockController final = gameObject.GetComponent<FinalWinBlockController>();
		if(final != null) final.BeatFinalLevel();

		// Load next level
		if(level != numberOfLevels){
			Debug.Log("T: " + PlayerPrefs.GetFloat("Level" + level+ "BestTime"));
			Debug.Log("N: " + PlayerPrefs.GetInt("Level" + level + "BestNumMovesMade"));
			Debug.Log("S: " + PlayerPrefs.GetInt("Level" + level + "BestScore"));
			SceneManager.LoadSceneAsync("B" + (level+1), LoadSceneMode.Single);
		}
	}
}
