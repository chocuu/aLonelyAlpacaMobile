using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour {

	/** Object of next cut scene */
	public GameObject nextScene;
	/** Image of next cut scene */
	private Image nextSceneImage;
	/** Image of the current cut scene */
	private Image curScene;
	/** FadeImage Script for this Scene */
	private FadeImage FIScript;
	/** The scene to go to once the cutscene has finished */
	public string endOfSceneLocation;
	/** Amount of time the current scene should stay for before
	    auto switching to the next */
	public float currSceneTime;

	/** Optional: true if want no fade out */
	public bool skipFade;
	/** Optional: true if want space to speed up the current image*/
	public bool spaceToSpeedUp = true;

	/** Accumulator of time passed between frames */
	private float timeAccum;
	/** True if actively fading Out */
	private bool fadeActive = false;
	/** True if cutscene image has been up/enabled */
	private bool wasEnabled = false;
	/** True if scene is finished, ensures scene doesn't run anymore */
	private bool endScene = false;

	private IntroMoveAlpaca rt;

	// Use this for initialization
	void Start () {
		curScene = GetComponent<Image>();
		if(nextScene != null)
			nextSceneImage = nextScene.GetComponent<Image>();
		rt = GetComponent<IntroMoveAlpaca>();
		FIScript = GetComponent<FadeImage>();
	}
	// Update is called once per frame
	void Update () {
		if(curScene.enabled){
			wasEnabled = true;
			if(!fadeActive){
				timeAccum += Time.deltaTime;
				if(timeAccum >= currSceneTime){
					if(skipFade)
						curScene.enabled = false;
					FIScript.FadeOut();
					fadeActive = true;
					if(rt != null)
						rt.setMoveIt(true);
					if(nextSceneImage!=null)
						nextSceneImage.enabled = true;
				}
                else if(spaceToSpeedUp && Input.GetMouseButtonDown(0)){
					FIScript.fadeRate = 10.0f;
					timeAccum = currSceneTime+1;
					if(SceneManager.GetActiveScene().name == "Splash Screen")
					{
						GameObject.Find("MangoSound").GetComponent<AudioSource>().volume = 0.0f;
					}
				}
			}
            else if(spaceToSpeedUp && Input.GetMouseButtonDown(0))
			{
				FIScript.fadeRate = 10.0f;
				if(SceneManager.GetActiveScene().name == "Splash Screen")
				{
					GameObject.Find("MangoSound").GetComponent<AudioSource>().volume = 0.0f;
				}
			}
		}
		else if(!endScene && wasEnabled){
			endScene = true;
			if(nextScene == null && endOfSceneLocation!=null){
				SceneManager.LoadScene(endOfSceneLocation, LoadSceneMode.Single);
			}
		}
	}
}
