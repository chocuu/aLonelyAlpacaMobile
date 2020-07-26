using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/**
 * Tutorial script used for Yuji's level (curr. level 7). Displays
 * what direction to hold to drop block off of cliff. People were 
 * having difficulty between holding in facing direction instead of 
 * clicking where they wanted to drop.
 */
public class LvlBlockTutorial_2 : MonoBehaviour
{
    public GameObject quadrant_0, quadrant_1, quadrant_2, quadrant_3;
    public Image alp_0, alp_1, alp_2, alp_3;
    public Animator alpanim_0, alpanim_1, alpanim_2, alpanim_3;
    private GameObject[] quadrants;
    public GameObject world;//WorldScript world;
	public Alpaca alpaca;
    public GameObject zoomText;
    public GameObject zoomBg;
    public GameObject panText;
    public GameObject panBg;
    public Zoomer zoomer;
    public PanButtonController panner;
    Vector3 alpacaDrop0 = new Vector3(1, 1, -4);
	Vector3 alpacaDrop1 = new Vector3(1, 1, -5);
	Vector3 alpacaDrop2 = new Vector3(2, -2, -4);
    Vector3 alpacaDrop3 = new Vector3(2, -5, -2);
    Vector3 alpacaDrop4 = new Vector3(2, -8, -0);
    Vector3 alpacaDrop5 = new Vector3(2, -8, -1);
    
    // Start is called before the first frame update
    void Start()
    {
  //       if(GameObject.FindGameObjectsWithTag("WORLD").Length > 0) {
  //           world = GameObject.FindGameObjectsWithTag("WORLD")[0].GetComponent<WorldScript>();
  //       }

  //       quadrants = new GameObject[4]{quadrant_0, quadrant_1, quadrant_2, quadrant_3};

  //       // quadrant1.enabled = quadrant2.enabled = dropRight.enabled = false;

  //       Vector2 alp_dim = new Vector2(Screen.width * 0.125f, Screen.width * 0.125f);
  //       alp_0.rectTransform.sizeDelta = alp_dim;
  //       alp_1.rectTransform.sizeDelta = alp_dim;
  //       alp_3.rectTransform.sizeDelta = alp_dim;
  //       alp_2.rectTransform.sizeDelta = alp_dim;

  //       //resize the quadrants
  //       Vector2 quad_dim = new Vector2(Screen.width*0.5f, Screen.height*0.5f);
		// quadrants[0].GetComponent<RectTransform>().sizeDelta = quad_dim;
		// quadrants[1].GetComponent<RectTransform>().sizeDelta = quad_dim;
		// quadrants[2].GetComponent<RectTransform>().sizeDelta = quad_dim;
		// quadrants[3].GetComponent<RectTransform>().sizeDelta = quad_dim;

        panText.SetActive(false);
        panBg.SetActive(false);
    }

    bool Equals(Vector3 a, Vector3 b) {
		return Math.Round(a.x - b.x)  == 0 && Math.Round(a.z - b.z) == 0 && Math.Round(a.y - b.y) == 0;
    }

    /**
     * forward = true, play animations as original 
     * otherwise rewind the animation
     */
    void SetAnimForward(bool forward) {
        if(forward) {
            alpanim_0.SetFloat("speed", 1.0f); 
            alpanim_1.SetFloat("speed", 1.0f);
            alpanim_2.SetFloat("speed", 1.0f); 
            alpanim_3.SetFloat("speed", 1.0f);
        } else {
            alpanim_0.SetFloat("speed", -1.0f); 
            alpanim_1.SetFloat("speed", -1.0f);
            alpanim_2.SetFloat("speed", -1.0f); 
            alpanim_3.SetFloat("speed", -1.0f);
        }
    }

    void ClearQuadrants() {
        quadrants[0].SetActive(false); 
        quadrants[1].SetActive(false); 
        quadrants[2].SetActive(false);
        quadrants[3].SetActive(false); 
    }


    // bool hadBlock = false; 
    int step = 0;

    // Update is called once per frame
    void Update()
    {
        switch(step) {
            case 0: // click zoom butt
                world.SetActive(false);
                step++;
                break;
            case 1:
                if(zoomer.isZoomed()) {
                    zoomText.SetActive(false);
                    zoomBg.SetActive(false);
                    panBg.SetActive(true);
                    panText.SetActive(true);
                    step++;
                }
                break;
            case 2: // click pan butt
                if(panner.getIsPanning()) {
                    panText.SetActive(false);
                    panBg.SetActive(false);
                    world.SetActive(true);
                    step++;
                } else if(!zoomer.isZoomed()) {
                    zoomText.SetActive(true);
                    zoomBg.SetActive(true);
                    panText.SetActive(false);
                    panBg.SetActive(false);
                    step--;
                }
                break;
            default: 
                break;
        }
    	// Vector3 alpacaPos = alpaca.GetCurrAlpacaLocation();

     //    if(alpaca.HasBlock()){
     //        if(!hadBlock)
     //            ClearQuadrants();
     //        SetAnimForward(true);
     //        if( Equals(alpacaDrop0, alpacaPos) || Equals(alpacaDrop1, alpacaPos) ||
     //                    (Math.Round(4 - alpacaPos.x)  == 0 && Math.Round(-11 - alpacaPos.y) == 0)
     //                    || Equals(alpacaDrop4, alpacaPos) || Equals(alpacaDrop5, alpacaPos)) {
     //            quadrants[2].SetActive(true);
     //        }
     //        else if( Equals(alpacaDrop2, alpacaPos) || Equals(alpacaDrop3, alpacaPos)) {
     //            quadrants[1].SetActive(true);
     //        }
     //        else {
     //            quadrants[1].SetActive(false); 
     //            quadrants[2].SetActive(false);
     //        }
     //    }
     //    else {
     //        SetAnimForward(false);
     //        if(world.GetBlockAlpacaFacing() != null && world.GetBlockAlpacaFacing().b_type == Block.BlockType.MOVEABLE &&
     //            alpacaPos.y > -14) {
     //            quadrants[world.AlpClickedWhere()].SetActive(true);
     //        } else {
     //            ClearQuadrants();
     //        }
     //    }

     //    hadBlock = alpaca.HasBlock();
    }

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
}
