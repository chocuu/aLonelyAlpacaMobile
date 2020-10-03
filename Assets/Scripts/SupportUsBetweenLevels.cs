using UnityEngine;
using UnityEngine.UI;

/** THIS SCRIPT SHOULD BE USED IN TANDEM WITH THE ORIGINAL SupportUsController.cs
    THIS SCRIPT ONLY ADDS ADDITIONAL FEATURES WHEN THE DONATE FLASHER APPEARS IN-BETWEEN LEVELS. **/
public class SupportUsBetweenLevels : MonoBehaviour
{
    // Set in editor
    [SerializeField] private Image supportButtTextBubbleImage;
    [SerializeField] private RectTransform supportButtTextBubbleRT;
    [SerializeField] private Text supportButtTextBubbleText;
    [SerializeField] private float imageFlashPeriod; //the period of an on-off flash for the clickMeText
    
    // Constants
    [Range(0, 1f)] private const float probabilityOfShowing = 0.4f; // The probability that the donateButton will appear on the level complete screen.
    private static readonly string[] possibleDonateMessages = new string[]{
                                                                "Click me if you enjoy the game!",
                                                                "Tap me to support us!",
                                                                "tap me or the alpaca will die",
                                                                "Tickle my nose!",
                                                                "Don't NOT poke me! "
                                                                };
    
    // Internal
    private bool animateTextBubble;
    private float imageFlashTimer;
    private bool buttonWillBeShown; // whether or not the donateButton will be shown on the level select screen

    private void Awake() 
    {
        buttonWillBeShown = (Random.Range(0,100) < probabilityOfShowing*100); // dice roll for whether or not donateButton will be shown 
        animateTextBubble = buttonWillBeShown;
    }


    private void Start() 
    {
        if(buttonWillBeShown){
            supportButtTextBubbleText.text = 
                    possibleDonateMessages[Random.Range(0, possibleDonateMessages.Length)];
            // supportButtTextBubbleRT.sizeDelta = new Vector2(supportButtTextBubbleText.preferredWidth, supportButtTextBubbleText.preferredHeight);
        }    

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
        if(animateTextBubble) {
            // toggle the image every half period
            imageFlashTimer += Time.deltaTime;
            if(imageFlashTimer >=imageFlashPeriod * 0.5f){ 
                imageFlashTimer = 0f;
                supportButtTextBubbleImage.enabled = !supportButtTextBubbleImage.enabled;
                supportButtTextBubbleText.enabled = !supportButtTextBubbleText.enabled;

            }
        }
    }


    //Called by the Support Us button
    public void ClickSupportUsBetweenLevels()
    {
        // stop showing the text bubble once the donate button has been clicked
        supportButtTextBubbleImage.enabled = false;
        supportButtTextBubbleText.enabled = false;
        animateTextBubble = false;
    }
}
