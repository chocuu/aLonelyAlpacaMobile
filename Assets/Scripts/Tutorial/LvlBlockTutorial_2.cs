using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/**
 * Tutorial script used for Yuji's level (curr. level 7). Displays
 * what direction to hold to drop block off of cliff. People were 
 * having difficulty between holding in facing direction instead of 
 * clicking where they wanted to drop.
 */
public class LvlBlockTutorial_2 : MonoBehaviour
{
    public Image dropRight;
	public Image quadrant1;
    public Image quadrant2;
	public Alpaca alpaca;
    Vector3 alpacaDrop0 = new Vector3(1, 1, -4);
	Vector3 alpacaDrop1 = new Vector3(1, 1, -5);
	Vector3 alpacaDrop2 = new Vector3(2, -2, -4);
	Vector3 alpacaDrop3 = new Vector3(2, -5, -2);

    // Start is called before the first frame update
    void Start()
    {
        quadrant1.enabled = false;
        quadrant2.enabled = false;
    }

    bool Equals(Vector3 a, Vector3 b) {
		return Math.Round(a.x - b.x)  == 0 && Math.Round(a.z - b.z) == 0 && Math.Round(a.y - b.y) == 0;
    }

    // Update is called once per frame
    void Update()
    {
    	Vector3 alpacaPos = alpaca.GetCurrAlpacaLocation();
        if(alpaca.HasBlock()){
            if( Equals(alpacaDrop0, alpacaPos) || Equals(alpacaDrop1, alpacaPos)) 
                quadrant2.enabled = true;
            else if( Equals(alpacaDrop2, alpacaPos) || Equals(alpacaDrop3, alpacaPos))
                quadrant1.enabled = true;
            else
                quadrant1.enabled = quadrant2.enabled = false;
        }
        else 
            quadrant1.enabled = quadrant2.enabled = false;
    }
}
