using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMoveAlpaca : MonoBehaviour {

	// Use this for initialization
	
	public RectTransform pacaPicRectTransform;
	public GameObject shadow;
	private bool moveIt;
	private float moveSpeed;
	private float stop_y = Screen.height * 0.05f;

	void Start () {	
		moveSpeed = 110f;
	}
	
	// Update is called once per frame
	void Update () {
		if(moveIt){
			Vector3 temp = pacaPicRectTransform.anchoredPosition; 
			if(Input.GetMouseButtonDown(0)){
				temp.y = stop_y;
				pacaPicRectTransform.anchoredPosition = temp;
				moveIt = false;
			}
			else{
				temp.y += moveSpeed*Time.deltaTime;
				pacaPicRectTransform.anchoredPosition = temp;
				if(pacaPicRectTransform.anchoredPosition.y >= stop_y){
					temp.y = stop_y;
					pacaPicRectTransform.anchoredPosition = temp;
					moveIt = false;
				}
			}		
		}
	}

	public void setMoveIt(bool move){ 
		// Debug.Log("set move it");
		this.moveIt = move;
		shadow.GetComponent<FadeImage>().FadeOut();
	}
}
