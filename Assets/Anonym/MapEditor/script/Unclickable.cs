using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Used to change the attached block's sprite / tint.
 */
public class Unclickable : MonoBehaviour {
    SpriteRenderer sr;
    Color normalColor;
    Color canBeDroppedOnColor;
    Color previewColor;
    
    Sprite normal;
    Sprite wSprite;
    Sprite aSprite;
    Sprite sSprite;
    Sprite dSprite;

	// Use this for initialization
	void Start () {
		sr = GetComponentInChildren<SpriteRenderer>();
        normalColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        canBeDroppedOnColor = new Color(0.85f, 0.85f, 0.85f, 1.0f);
        previewColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        
        normal = Resources.Load<Sprite>("Sprites/normal_unclick");
        wSprite = Resources.Load<Sprite>("Sprites/W_Unclick");
        aSprite = Resources.Load<Sprite>("Sprites/A_Unclick");
        sSprite = Resources.Load<Sprite>("Sprites/S_Unclick");
        dSprite = Resources.Load<Sprite>("Sprites/D_Unclick");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void setNormalColor() {
        sr.color = normalColor;
    }
    
    public void setCanBeDroppedOnColor() {
        sr.color = canBeDroppedOnColor;
    }
    
    public void setNormalSprite() {
        sr.sprite = normal;
    }

    public void setDropping() {
        sr.color = previewColor;
    }
    
    public void setWASDsprite(int facingVal) {
        if (facingVal == 0) {
            sr.sprite = wSprite;
        } else if (facingVal == 1) {
            sr.sprite = sSprite;
        } else if (facingVal == 2) {
            sr.sprite = dSprite;
        } else if (facingVal == 3) {
            sr.sprite = aSprite;
        }
    }
}
