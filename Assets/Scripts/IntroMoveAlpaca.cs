using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMoveAlpaca : MonoBehaviour {

	// Use this for initialization
	
	public RectTransform rt;
	public GameObject shadow;
	private bool moveIt;
	private float moveSpeed;
	private float stop_y = Screen.height * 0.6f;

	void Start () {	
		moveSpeed = 160f;
	}
	
	// Update is called once per frame
	void Update () {
		if(moveIt){
			// Debug.Log(rt.position.y);
			Vector3 temp = rt.position; 
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
				temp.y = stop_y;
				rt.position = temp;
				moveIt = false;
			}
			else{
				temp.y += moveSpeed*Time.deltaTime;
				rt.position = temp;
				if(rt.position.y >= stop_y){
					temp.y = stop_y;
					rt.position = temp;
					moveIt = false;
			}
		}		
	}
}

	public void setMoveIt(bool move){ 
		Debug.Log("set move it");
		this.moveIt = move;
		shadow.GetComponent<FadeImage>().FadeOut();
	}
}
