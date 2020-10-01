using UnityEngine;
using UnityEngine.UI;

/** THIS SCRIPT SHOULD BE USED IN TANDEM WITH THE ORIGINAL SupportUsController.cs
    THIS SCRIPT ONLY ADDS ADDITIONAL FEATURES WHEN THE DONATE FLASHER APPEARS IN-BETWEEN LEVELS. **/
public class SupportUsBetweenLevels : MonoBehaviour
{
    // Set in editor
    [SerializeField] private Image clickMeTextImage;
    [SerializeField] private float imageFlashPeriod; //the period of an on-off flash for the clickMeText
    
    private static float probabilityOfShowing = 0.4f; // The probability that the donateButton will appear on the level complete screen.
    // Internal
    private float imageFlashTimer;
    private bool buttonWillBeShown; // whether or not the donateButton will be shown on the level select screen

    private void Awake() 
    {
        buttonWillBeShown = (Random.Range(0,100) < probabilityOfShowing); // dice roll for whether or not donateButton will be shown 
    }

    private void OnEnable() 
    {
        if(!buttonWillBeShown){
            gameObject.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        imageFlashTimer += Time.deltaTime;
        if(imageFlashTimer >=imageFlashPeriod * 0.5f){ // toggle the image every half period
            imageFlashTimer = 0f;
            Color temp = clickMeTextImage.color;
            temp.a = (temp.a == 0) ? 1f : 0f; 
            clickMeTextImage.color = temp;
        }
    }


    //Called by the Support Us button
    public void ClickSupportUsBetweenLevels()
    {
        clickMeTextImage.enabled = false;
    }
}
