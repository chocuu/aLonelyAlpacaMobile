using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Handles background scaling. Should keep it fills the entire screen.
 * (Used by Zoomer script)
 */
public class ScaleBackground : MonoBehaviour
{
	public SpriteRenderer render;
	public float magnitude = 8.0f;
	float width;
	float height;
	
    // Start is called before the first frame update
    void Start()
    {
    	width = render.sprite.bounds.size.x;
		height = render.sprite.bounds.size.y;
    	
		// if(SceneManager.GetActiveScene().name == "B0.5 - Intro")
		// 	ScaleIntroCutscene(1/Camera.main.orthographicSize);
    	// else
		Scale2(1/Camera.main.orthographicSize);
    }

	public void Scale2(float s) {
		if (render == null) return;

		width = render.sprite.bounds.size.x;
		height = render.sprite.bounds.size.y;

		var worldScreenWidth = Camera.main.orthographicSize * magnitude + s*1.5f;
		var worldScreenHeight = (worldScreenWidth / Screen.width) * Screen.height;

		Vector3 scale = new Vector3((float) worldScreenWidth / width, (float) worldScreenWidth / width,1);
		transform.localScale = scale;
	}

	/**
	 * Intro scene uses a different camera, so scaling is different
	 */
	// public void ScaleIntroCutscene(float s) {
	// 	if (render == null) return;

	// 	width = render.sprite.bounds.size.x;
	// 	height = render.sprite.bounds.size.y;

	// 	var worldScreenWidth = Camera.main.orthographicSize * magnitude + s*1.5f;
	// 	var worldScreenHeight = (worldScreenWidth / Screen.width) * Screen.height;

	// 	Vector3 scale = new Vector3((float) worldScreenWidth / width, (float) worldScreenWidth / width,1);
	// 	transform.localScale = scale;
	// }

    // Update is called once per frame
    void Update()
    {
        
    }
}
