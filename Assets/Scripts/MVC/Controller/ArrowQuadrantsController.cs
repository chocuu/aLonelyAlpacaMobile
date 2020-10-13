using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ArrowQuadrantsController : MonoBehaviour
{
	// used to highlight four arrowQuadrants
    [SerializeField] private Image allArrowsImage;
	[SerializeField] private Image[] arrowQuadrants;


    private void Awake()
    {
		SetUseArrows(PlayerSettingsController.UseArrows);
    }


    void Update()
    {
        if(PlayerSettingsController.UseArrows) {
            if(ClickedNow()) { // click is happening
                HighlightQuadrant(ClickedWhere(Input.mousePosition));
            } 
            else {
                ClearHighlights();
            }
        }
    }


    /** Remove all highlights from screen */
	void ClearHighlights() {
		arrowQuadrants[0].enabled = false;
		arrowQuadrants[1].enabled = false;
		arrowQuadrants[2].enabled = false;
		arrowQuadrants[3].enabled = false;
    }


    /** Makes the current click position highlighted */
    void HighlightQuadrant(int clickedWhere) {
    	ClearHighlights();
        if(clickedWhere == -1) return;
    	arrowQuadrants[clickedWhere].enabled = true;
    }


    /** Called From PlayerSettingsController **/
    public void SetUseArrows(bool set)
    {
        Debug.Log("SetUseArrows() " + set);
        ClearHighlights();
        allArrowsImage.enabled = set;
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
