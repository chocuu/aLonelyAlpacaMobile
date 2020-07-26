using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockButt : MonoBehaviour
{

	public Image buttImg;
    public Image buttImgBg;
    private Color orange;
    private Color lightOrange;
    private Color lightWhite; 
    private bool pickUp;
    private float timer = 0f;
    private float timer_duration= 0.5f;

    // Start is called before the first frame update
    void Start()
    {
    	if(GameObject.FindGameObjectsWithTag("WORLD").Length > 0) {
            WorldScript world = GameObject.FindGameObjectsWithTag("WORLD")[0].GetComponent<WorldScript>();
            world.AddBlockButt(this);
        }
        SetNoBlock();
        orange = new Color(1, 0.8666f, 0.4f, 1);
        lightOrange = new Color(1, 0.8666f, 0.4f, 0.5f);
        lightWhite = new Color(1f, 1f, 1f, 0.6f);
        pickUp=false;
    }

    public void SetPickUp() {
        pickUp=true;
        buttImgBg.color = orange;
        gameObject.SetActive(true);
    }

    public void SetDrop() {
        pickUp=false;
        buttImgBg.color = lightWhite;
        gameObject.SetActive(true);
    }

    public void SetNoBlock() {
        pickUp=false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (pickUp && timer < timer_duration) {
            timer += Time.deltaTime;
            buttImgBg.color = orange;
        } else if (pickUp && timer < timer_duration*1.5f) {
            timer += Time.deltaTime;
            buttImgBg.color = lightOrange;
        } else if (pickUp) {
            timer=0;
        }
    }
}
