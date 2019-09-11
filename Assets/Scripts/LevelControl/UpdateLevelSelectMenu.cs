using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class UpdateLevelSelectMenu : MonoBehaviour
{   
    // Number of Levels
    public const int numberOfLevels = 26; 
    // Selector Gameobject dragged in from hierarchy
    public GameObject alpacaCursor;
    public Animator alpacaCursorAnim;
    // UI elements from canvas that need to be updated
    public GameObject ImagePreviewUI;
    public GameObject LevelBannerUI;
    // Collection of image previews
    public Sprite[] imagePreviewsArray = new Sprite[numberOfLevels];
    // Collection of level banners
    public Sprite[] levelBannersArray = new Sprite[numberOfLevels];
        // Gameobject of "Next Level" button
    public GameObject nextLevelButtArrow;
        // Gameobject of "Prex Level" button
    public GameObject prevLevelButtArrow; 
    // Reference to the transform of the alpaca selector used to change its position
    private static Transform alpacaPos;
    // Reference to the spriterenderer of the alpaca selector used to flip its sprite at certain levels
    private static SpriteRenderer alpacaSR;
    // Reference to the image component of the ImagePreview gameobject
    private Image ImagePreviewImage;
    // Reference to the Image component of the LevelBanner gameobject
    private Image LevelBannerImage;
    // Reference to the Image component of the nextLevelButtArrow gameobject
    private Image NextLevelImage;
    // Reference to the Image component of the prevLevelButtArrow gameobject
    private Image PrevLevelImage;
    // Level that the alpaca is on / that is being selected
    static int currentLevel = 0; 

    // Positions on the map that alpaca moves to
    private Vector3[] levelPositions = new [] {
                                            new Vector3(-280f, -14f, 93f),
                                            new Vector3(-302.5f, -48f, 93f),
                                            new Vector3(-314f, -82f, 93f),
                                            new Vector3(-332.5f, -103f, 93f),
                                            new Vector3(-358f, -114f, 93f),
                                            new Vector3(-395f, -117f, 93f),
                                            new Vector3(-405f, -132.5f, 93f),
                                            new Vector3(-369.5f, -128f, 93f),
                                            new Vector3(-336.5f, -131.5f, 93f),
                                            new Vector3(-302f, -126.5f, 93f),
                                            new Vector3(-278.5f, -88.5f, 93f),
                                            new Vector3(-247f, -68.5f, 93f),
                                            new Vector3(-223f, -81f, 93f),
                                            new Vector3(-197f, -109f, 93f),
                                            new Vector3(-239f, -108f, 93f),
                                            //new Vector3(-262f, -124f, 93f),
                                            new Vector3(-288.5f, -147.5f, 93f),
                                            new Vector3(-312.5f, -161f, 93f),
                                            new Vector3(-338f, -161.5f, 93f),
                                            new Vector3(-369f, -161.5f, 93f),
                                            new Vector3(-402.5f, -154.5f, 93f),
                                            new Vector3(-379f, -175f, 93f),
                                            //new Vector3(-335f, -178f, 93f),
                                            new Vector3(-300f, -175f, 93f),
                                            new Vector3(-258.5f, -161f, 93f),
                                            new Vector3(-224f, -147f, 93f),
                                            new Vector3(-187f, -157f, 93f),
                                            new Vector3(-215f, -180f, 93f),
                                            };
    
    void Start() {
        // Put selector at the player's most recent level if they enter level selct from menu
        // and put them at the level the paused from if they enter level select from a level
        GameObject previousLevel = GameObject.Find("GameObject");
        if (previousLevel != null) {
            currentLevel = int.Parse(Regex.Match(previousLevel.GetComponent<currentLevelName>().currentLevelNameString, @"\d+").Value);
            currentLevel = (currentLevel == 0) ? int.Parse(PlayerPrefs.GetString("lastLoadedScene").Substring(1))-1: (currentLevel - 1);
        }

        if(currentLevel < 0 || currentLevel >= numberOfLevels) 
            currentLevel = 0;

        // Load components
        alpacaPos = alpacaCursor.GetComponent<Transform>();
        alpacaSR = alpacaCursor.GetComponent<SpriteRenderer>();
        ImagePreviewImage = ImagePreviewUI.GetComponent<Image>();
        LevelBannerImage = LevelBannerUI.GetComponent<Image>();
        NextLevelImage = nextLevelButtArrow.GetComponent<Image>();
        PrevLevelImage = prevLevelButtArrow.GetComponent<Image>();
        
        // Make level arrows disappear if starting at certain levels
        if(currentLevel == 0)
            PrevLevelImage.enabled = false;
        else if(currentLevel == numberOfLevels-1)
            NextLevelImage.enabled = false; 
        // Make next level arrow disappear if player hasn't made it that far yet
        if(currentLevel == PlayerPrefs.GetInt("LevelPassed")) NextLevelImage.enabled = false;

        // Start at the right place
        alpacaPos.position = levelPositions[currentLevel];
        ImagePreviewImage.sprite = imagePreviewsArray[currentLevel];
        LevelBannerImage.sprite = levelBannersArray[currentLevel];
        
        // Start with right flipping (HARDCODED VALUES)
        if((currentLevel>=6 && currentLevel <= 12) || 
                (currentLevel >= 19 && currentLevel <= 23))
            flipAlpacaSprite();
    }

    private void flipAlpacaSprite(){
        alpacaCursorAnim.SetBool("is_facing_left", !(alpacaCursorAnim.GetBool("is_facing_left")));
    }

    public void NextLevelClicked() {
        // Update alpaca sprite position
        if(currentLevel < (numberOfLevels - 1)){
            currentLevel++;
            alpacaPos.position = levelPositions[currentLevel];
        }
        
        // Flip alpaca sprite at certain levels (HARDCODED VALUES)
        if(currentLevel == 6 || currentLevel == 13 || currentLevel == 20 || currentLevel == 24)
            flipAlpacaSprite();
        
        // Make level arrows appear/disapper at certain level
        if(currentLevel == 1) PrevLevelImage.enabled = true;
        else if(currentLevel == numberOfLevels-1) NextLevelImage.enabled = false;
        // Make next level arrow disappear if player hasn't made it that far yet
        if(currentLevel == PlayerPrefs.GetInt("LevelPassed")) NextLevelImage.enabled = false;

        // Update image preview and level banner
        ImagePreviewImage.sprite = imagePreviewsArray[currentLevel];
        LevelBannerImage.sprite = levelBannersArray[currentLevel];
    }

    public void PrevLevelClicked() {
        // Update alpaca sprite position
        if(currentLevel > 0 && currentLevel < numberOfLevels){
            currentLevel--;
            alpacaPos.position = levelPositions[currentLevel];
        }
        
        // Flip alpaca sprite at certain levels (HARDCODED VALUES)
        if(currentLevel == 5 || currentLevel == 12 || currentLevel == 19 || currentLevel == 23)
            flipAlpacaSprite();
        
        // Make level arrows appear/disapper at certain level
        if(currentLevel == 0) PrevLevelImage.enabled = false;
        else if(currentLevel == numberOfLevels-2) NextLevelImage.enabled = true;
        if (currentLevel == (PlayerPrefs.GetInt("LevelPassed") - 1)) NextLevelImage.enabled = true; 
        // Update image preview and level banner
        ImagePreviewImage.sprite = imagePreviewsArray[currentLevel];
        LevelBannerImage.sprite = levelBannersArray[currentLevel];
    }

    public void CurrLevelClicked() {
        SceneManager.LoadScene("B" + (currentLevel+1), LoadSceneMode.Single);
    }

}
