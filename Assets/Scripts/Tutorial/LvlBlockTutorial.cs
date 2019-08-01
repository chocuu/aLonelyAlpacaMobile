using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/**
 * Tutorial script for first moveable block level. Instructs
 * the player to approach the block, hold to pick up, and when 
 * reaching the edge to hold to drop.
 */
public class LvlBlockTutorial : MonoBehaviour
{

	public GameObject arrowRight;
	public GameObject approachShrub;
	public Image holdLeft;
	public Image holdRight;
    public Image dropLeft;
    public Image dropRight;
	public Image quadrant0;
	public Image quadrant1;
    public Image quadrant2;
    public Image quadrant3;
	public Alpaca alpaca;
	int step = 0;

	Vector3 alpacaApproachLeft = new Vector3(-1, 0, 0);
	Vector3 alpacaApproachRight = new Vector3(0, 0, 1);

    // Start is called before the first frame update
    void Start()
    {
        holdLeft.enabled = holdRight.enabled = false;
        dropLeft.enabled = dropRight.enabled = false;
        quadrant0.enabled = quadrant1.enabled = quadrant2.enabled = quadrant3.enabled = false;

        holdLeft.rectTransform.position = new Vector3(Screen.width * 0.25f , Screen.height * 0.75f);
        holdRight.rectTransform.position = new Vector3(Screen.width * 0.75f, Screen.height * 0.75f);
        dropLeft.rectTransform.position = new Vector3(Screen.width * 0.25f , Screen.height * 0.25f);
        dropRight.rectTransform.position = new Vector3(Screen.width * 0.75f, Screen.height * 0.25f);
    }

    bool equals(Vector3 a, Vector3 b) {
		return Math.Round(a.x - b.x)  == 0 && Math.Round(a.z - b.z) == 0;
    }

    bool onEdgeLeft(Vector3 a) {
        return Math.Round(a.y) == 1 && Math.Round(a.z) == -1;
    }

    bool onEdgeRight(Vector3 a) {
        return Math.Round(a.y) == 1 && Math.Round(a.x) == 1 && Math.Round(a.z) == 0;
    }

    // Update is called once per frame
    void Update()
    {
    	Vector3 alpacaPos = alpaca.GetCurrAlpacaLocation();
        switch(step) {
        	case 0: //approach shrub
        		if(equals(alpacaPos, alpacaApproachLeft) || equals(alpacaPos, alpacaApproachRight)) {
        			step += 1;
					arrowRight.active = false;
        		}
        		break;
        	case 1: // hold to pick up
        		if(equals(alpacaPos, alpacaApproachLeft) || equals(alpacaPos, alpacaApproachRight)) {
        			approachShrub.active = false;
					arrowRight.active = false;
					if(equals(alpacaPos, alpacaApproachLeft))
						holdRight.enabled = quadrant1.enabled = true;
					if(equals(alpacaPos, alpacaApproachRight))
						holdLeft.enabled = quadrant0.enabled = true;
        		} else if(!alpaca.HasBlock()) {
        			approachShrub.active = true;
					arrowRight.active = true;
					holdLeft.enabled = quadrant0.enabled = false;
					holdRight.enabled = quadrant1.enabled = false;
        		}
        		if(alpaca.HasBlock()) {
        			step += 1;
					holdLeft.enabled = quadrant0.enabled = false;
					holdRight.enabled = quadrant1.enabled = false;
        		}
        		break;
            case 2:
                if(alpaca.HasBlock()) {
                    quadrant2.enabled = quadrant3.enabled = false;
                    dropLeft.enabled = dropRight.enabled = false;
                    if(onEdgeLeft(alpacaPos))
                        quadrant3.enabled = dropLeft.enabled = true;
                    if(onEdgeRight(alpacaPos))
                        quadrant2.enabled = dropRight.enabled = true;
                } 
                else {
                    quadrant2.enabled = quadrant3.enabled = false;
                    dropLeft.enabled = dropRight.enabled = false;
                }
                break;
        	default:
        		break;
        }
    }
}
