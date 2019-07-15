using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles background scaling. Should keep it fills the entire screen.
 * (Used by Zoomer script)
 */
public class ScaleBackground : MonoBehaviour
{
	public SpriteRenderer render;
	float width;
	float height;
	
    // Start is called before the first frame update
    void Start()
    {
    	width = render.sprite.bounds.size.x;
		height = render.sprite.bounds.size.y;
    	
    	Scale2(1/Camera.main.orthographicSize);
    }

	public void Scale2(float s) {
		if (render == null) return;

		width = render.sprite.bounds.size.x;
		height = render.sprite.bounds.size.y;

		var worldScreenWidth = Camera.main.orthographicSize * 8 + s*1.5f;
		var worldScreenHeight = (worldScreenWidth / Screen.width) * Screen.height;

		Vector3 scale = new Vector3((float) worldScreenWidth / width, (float) worldScreenWidth / width,1);
		transform.localScale = scale;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
