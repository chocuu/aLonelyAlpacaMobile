using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/**
 * Script for first and second level with control scheme. Shows
 * "tap to move" 
 * 
 * This is a lot of code reuse form other scripts idk if this is good style LOL
 * From World Controller & Show Level Banner Controller
 */
public class Lvl1Tutorial : MonoBehaviour
{
	public Image tutImage;
	bool didFade = false;

    private void Awake()
    {
        if(tutImage != null) {
            tutImage.color = new Color(1, 1, 1, 0);
            StartCoroutine(fadeIn(true));
        }
    }


    void Update()
    {
        if(!didFade && ClickedNow()) { // Begin fading upon click
            StartCoroutine(fadeOut(true));
    	}
    }


    /** Fade in the Tutorial image*/
    IEnumerator fadeIn(bool fadeAway) {
		yield return new WaitForSeconds(0.2f);
		if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 0; i <= 1; i += Time.deltaTime * 3)
            {
                if(!didFade) // set color with i as alpha
                    tutImage.color = new Color(1, 1, 1, i);
                yield return null;
            }
            tutImage.color = new Color(1, 1, 1, 1);
        }
	}


    /** Fade out the Tutorial image*/
    IEnumerator fadeOut(bool fadeAway) {
    	didFade = true;
		yield return new WaitForSeconds(1f);
		if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                if(tutImage != null)
                    tutImage.color = new Color(1, 1, 1, i);
                yield return null;
            }
            if(tutImage != null)
                tutImage.color = new Color(1, 1, 1, 0);
        }
	}


    ///////////// HELPER FUNCTIONS ////////////////
    /**
	 * Returns true iff there was a click during this update.
	 */
	bool ClickedNow() {
		// check if is on ui button (this version works for mobile too)
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		
		return Input.GetMouseButton(0) && !(results.Count > 0);	
	}


    // Used to determine which quadrant is clicked
    int padding = Screen.height / 12;
    int middle_x = Screen.width / 2;
    int middle_y = Screen.height / 2;

    /**
     * Returns which quadrant the click this update was on.
     *  -----------
     * |  0  |  1  |
     * |-----------
     * |  3  |  2  |
     *  -----------
     *
     * Returns -1 if too close to the boundary
     */
    int ClickedWhere(Vector3 clickPos) {
        if(clickPos.x < middle_x - padding) {
            if (clickPos.y < middle_y - padding) return 3;
            else if(clickPos.y > middle_y + padding) return 0;
        } else if(clickPos.x > middle_x + padding) {
            if (clickPos.y < middle_y - padding) return 2;
            else if(clickPos.y > middle_y + padding) return 1;
        }
        return -1;
    }
}
