using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class EndCreditsController : MonoBehaviour {

	private Image creditsImage;
	public GameObject firstScreenAfterCreds;
	private Image firstScreenAfterCredsImage;
	private CutsceneController firstScreenAfterCredsCutsceneController;

	public float endPauseTime;
	private FadeImage FIScript;
	private RectTransform rt;
	private float moveSpeed;
	private bool done;
	private bool started;
	private float timeAccum;
	//private bool speedyCreds;
	public float initial_speed;
	private const float initial_pos = -1005f;
	private const float final_position = 400f;
	private const float time_delay = 0f;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform>();
		FIScript = GetComponent<FadeImage>();
		creditsImage = GetComponent<Image>();
		firstScreenAfterCredsImage = firstScreenAfterCreds.GetComponent<Image>();
		firstScreenAfterCredsCutsceneController = firstScreenAfterCreds.GetComponent<CutsceneController>();
		moveSpeed = initial_speed;
	}
	
	// Update is called once per frame
	void Update () {
		if(!started){
			timeAccum += Time.deltaTime;
			if(timeAccum>=time_delay){
				started = true;
			}
		}
		else{
			if(!done){
				Vector3 temp = rt.anchoredPosition; 
				// if(Input.GetMouseButton(0)) moveSpeed = initial_speed*4;
				if(rt.anchoredPosition.y >= (final_position*0.8f)){
					moveSpeed-= 2.5f*Time.deltaTime;
					if(moveSpeed<=20) moveSpeed = 20;
				}
				else moveSpeed = initial_speed;

				temp.y += moveSpeed*Time.deltaTime;
				rt.anchoredPosition = temp;
				
				if(rt.anchoredPosition.y >= final_position){
					temp.y = final_position;
					rt.anchoredPosition = temp;
					done = true;
				}
			}
			else{
				StartCoroutine(FadeAfterTime(endPauseTime));
			}
			if(!creditsImage.enabled){
				firstScreenAfterCredsCutsceneController.enabled = true;
			}
		}
	}

	public bool isDone() { return done; }

	/* waits for time seconds, then fades out */
	IEnumerator FadeAfterTime(float time){
    	yield return new WaitForSeconds(time);
     	FIScript.FadeOut();
		firstScreenAfterCredsImage.enabled = true;
 	}

}
