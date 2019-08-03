using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelMenuControllerScript : MonoBehaviour
{

  public Button B1Button, B2Button, B3Button, B4Button, B5Button, B6Button,
          B7Button, B8Button, B9Button, B10Button, B11Button, B12Button,
          B13Button, B14Button, B15Button, B16Button, B17Button, B18Button,
          B19Button, B20Button, B21Button, B22Button, B23Button, B24Button,
          B25Button, B26Button;
  public int levelPassed;
  public int moveAlpacaAround;

  public Sprite alpacaLeft;
  public Sprite alpacaRight;

  private Image levelBanner;
  private Image levelImagePreview;

  // Use this for initialization
  void Start()
  {
    levelPassed = 26;//PlayerPrefs.GetInt("LevelPassed");
    if(levelPassed > 26 || levelPassed < 0)
        levelPassed = 0;
    moveAlpacaAround = levelPassed;

    levelBanner = GameObject.Find("LevelBanner").GetComponent<Image>();
    levelImagePreview = GameObject.Find("ImagePreview").GetComponent<Image>();

    B1Button.interactable = true;

    switch (levelPassed)
    {  
      case 1:
        B2Button.interactable = true;
        break;
      case 2:
        B2Button.interactable = true;
        B3Button.interactable = true;
        break;
      case 3:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        break;
      case 4:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        break;
      case 5:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        break;
      case 6:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        B7Button.interactable = true;
        break;
      case 7:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        B7Button.interactable = true;
        B8Button.interactable = true;
        break;
      case 8:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        B7Button.interactable = true;
        B8Button.interactable = true;
        B9Button.interactable = true;
        break;
      case 9:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        B7Button.interactable = true;
        B8Button.interactable = true;
        B9Button.interactable = true;
        B10Button.interactable = true;
        break;
      case 10:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        B7Button.interactable = true;
        B8Button.interactable = true;
        B9Button.interactable = true;
        B10Button.interactable = true;
        B11Button.interactable = true;
        break;
      case 11:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        B7Button.interactable = true;
        B8Button.interactable = true;
        B9Button.interactable = true;
        B10Button.interactable = true;
        B11Button.interactable = true;
        B12Button.interactable = true;
        break;
      case 12:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        B7Button.interactable = true;
        B8Button.interactable = true;
        B9Button.interactable = true;
        B10Button.interactable = true;
        B11Button.interactable = true;
        B12Button.interactable = true;
        B13Button.interactable = true;
        break;
      case 13:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        B7Button.interactable = true;
        B8Button.interactable = true;
        B9Button.interactable = true;
        B10Button.interactable = true;
        B11Button.interactable = true;
        B12Button.interactable = true;
        B13Button.interactable = true;
        B14Button.interactable = true;
        break;
      case 14:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        B7Button.interactable = true;
        B8Button.interactable = true;
        B9Button.interactable = true;
        B10Button.interactable = true;
        B11Button.interactable = true;
        B12Button.interactable = true;
        B13Button.interactable = true;
        B14Button.interactable = true;
        B15Button.interactable = true;
        break;
      case 15:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        B7Button.interactable = true;
        B8Button.interactable = true;
        B9Button.interactable = true;
        B10Button.interactable = true;
        B11Button.interactable = true;
        B12Button.interactable = true;
        B13Button.interactable = true;
        B14Button.interactable = true;
        B15Button.interactable = true;
        B16Button.interactable = true;
        break;
      case 16:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        B7Button.interactable = true;
        B8Button.interactable = true;
        B9Button.interactable = true;
        B10Button.interactable = true;
        B11Button.interactable = true;
        B12Button.interactable = true;
        B13Button.interactable = true;
        B14Button.interactable = true;
        B15Button.interactable = true;
        B16Button.interactable = true;
        B17Button.interactable = true;
        break;
      case 17:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        B7Button.interactable = true;
        B8Button.interactable = true;
        B9Button.interactable = true;
        B10Button.interactable = true;
        B11Button.interactable = true;
        B12Button.interactable = true;
        B13Button.interactable = true;
        B14Button.interactable = true;
        B15Button.interactable = true;
        B16Button.interactable = true;
        B17Button.interactable = true;
        B18Button.interactable = true;
        break;
      case 18:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        B7Button.interactable = true;
        B8Button.interactable = true;
        B9Button.interactable = true;
        B10Button.interactable = true;
        B11Button.interactable = true;
        B12Button.interactable = true;
        B13Button.interactable = true;
        B14Button.interactable = true;
        B15Button.interactable = true;
        B16Button.interactable = true;
        B17Button.interactable = true;
        B18Button.interactable = true;
        B19Button.interactable = true;
        break;
      case 19:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        B7Button.interactable = true;
        B8Button.interactable = true;
        B9Button.interactable = true;
        B10Button.interactable = true;
        B11Button.interactable = true;
        B12Button.interactable = true;
        B13Button.interactable = true;
        B14Button.interactable = true;
        B15Button.interactable = true;
        B16Button.interactable = true;
        B17Button.interactable = true;
        B18Button.interactable = true;
        B19Button.interactable = true;
        B20Button.interactable = true;
        break;
      case 20:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        B7Button.interactable = true;
        B8Button.interactable = true;
        B9Button.interactable = true;
        B10Button.interactable = true;
        B11Button.interactable = true;
        B12Button.interactable = true;
        B13Button.interactable = true;
        B14Button.interactable = true;
        B15Button.interactable = true;
        B16Button.interactable = true;
        B17Button.interactable = true;
        B18Button.interactable = true;
        B19Button.interactable = true;
        B20Button.interactable = true;
        B21Button.interactable = true;
        break;
      case 21:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        B7Button.interactable = true;
        B8Button.interactable = true;
        B9Button.interactable = true;
        B10Button.interactable = true;
        B11Button.interactable = true;
        B12Button.interactable = true;
        B13Button.interactable = true;
        B14Button.interactable = true;
        B15Button.interactable = true;
        B16Button.interactable = true;
        B17Button.interactable = true;
        B18Button.interactable = true;
        B19Button.interactable = true;
        B20Button.interactable = true;
        B21Button.interactable = true;
        B22Button.interactable = true;
        break;
      case 22:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        B7Button.interactable = true;
        B8Button.interactable = true;
        B9Button.interactable = true;
        B10Button.interactable = true;
        B11Button.interactable = true;
        B12Button.interactable = true;
        B13Button.interactable = true;
        B14Button.interactable = true;
        B15Button.interactable = true;
        B16Button.interactable = true;
        B17Button.interactable = true;
        B18Button.interactable = true;
        B19Button.interactable = true;
        B20Button.interactable = true;
        B21Button.interactable = true;
        B22Button.interactable = true;
        B23Button.interactable = true;
        break;
      case 23:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        B7Button.interactable = true;
        B8Button.interactable = true;
        B9Button.interactable = true;
        B10Button.interactable = true;
        B11Button.interactable = true;
        B12Button.interactable = true;
        B13Button.interactable = true;
        B14Button.interactable = true;
        B15Button.interactable = true;
        B16Button.interactable = true;
        B17Button.interactable = true;
        B18Button.interactable = true;
        B19Button.interactable = true;
        B20Button.interactable = true;
        B21Button.interactable = true;
        B22Button.interactable = true;
        B23Button.interactable = true;
        B24Button.interactable = true;
        break;
      case 24:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        B7Button.interactable = true;
        B8Button.interactable = true;
        B9Button.interactable = true;
        B10Button.interactable = true;
        B11Button.interactable = true;
        B12Button.interactable = true;
        B13Button.interactable = true;
        B14Button.interactable = true;
        B15Button.interactable = true;
        B16Button.interactable = true;
        B17Button.interactable = true;
        B18Button.interactable = true;
        B19Button.interactable = true;
        B20Button.interactable = true;
        B21Button.interactable = true;
        B22Button.interactable = true;
        B23Button.interactable = true;
        B24Button.interactable = true;
        B25Button.interactable = true;
        break;
      case 25:
      case 26:
        B2Button.interactable = true;
        B3Button.interactable = true;
        B4Button.interactable = true;
        B5Button.interactable = true;
        B6Button.interactable = true;
        B7Button.interactable = true;
        B8Button.interactable = true;
        B9Button.interactable = true;
        B10Button.interactable = true;
        B11Button.interactable = true;
        B12Button.interactable = true;
        B13Button.interactable = true;
        B14Button.interactable = true;
        B15Button.interactable = true;
        B16Button.interactable = true;
        B17Button.interactable = true;
        B18Button.interactable = true;
        B19Button.interactable = true;
        B20Button.interactable = true;
        B21Button.interactable = true;
        B22Button.interactable = true;
        B23Button.interactable = true;
        B24Button.interactable = true;
        B25Button.interactable = true;
        B26Button.interactable = true;
        break;
    }

    // Debug.Log("levelPassed: " + levelPassed);
    GameObject previousLevel = GameObject.Find("GameObject");
    if (previousLevel != null) {
        levelPassed = int.Parse(Regex.Match(previousLevel.GetComponent<currentLevelName>().currentLevelNameString, @"\d+").Value);
    }
    positionAlpacaOnRecentLevel(levelPassed - 1);
  }

  public void levelToLoad(int level)
  {
    SceneManager.LoadScene("B" + level, LoadSceneMode.Single);
  }

  public void positionAlpacaOnRecentLevel(int mostRecentLevel)
  {
    string findThisButton;

    // Change the conditional values to match whatever the last level is
    findThisButton = "B" + (mostRecentLevel + 1).ToString() + "Button";
    if (mostRecentLevel == 26) findThisButton = "B" + (mostRecentLevel).ToString() + "Button";

    GameObject mostRecentStar = GameObject.Find(findThisButton);
    // Debug.Log(mostRecentStar);
    Vector3 mostRecentStarPosition = mostRecentStar.GetComponent<RectTransform>().position;

    GameObject alpacaSprite = GameObject.Find("AlpacaSprite");
    alpacaSprite.GetComponent<RectTransform>().position = mostRecentStarPosition + new Vector3(0, 40, 0);
    if ((mostRecentLevel >= 0 && mostRecentLevel <= 6)
    || (mostRecentLevel >= 14 && mostRecentLevel <= 18)
    || mostRecentLevel == 25)
        alpacaSprite.GetComponent<Image>().sprite = alpacaLeft;
    else
        alpacaSprite.GetComponent<Image>().sprite = alpacaRight;

  }

  // Update is called once per frame
  void Update()
  {
    // if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.UpArrow)) && moveAlpacaAround > 0)
    // {
    //   moveAlpacaAround = moveAlpacaAround - 1;
    //   positionAlpacaOnRecentLevel(moveAlpacaAround);
    //   if (moveAlpacaAround > levelPassed)
    //   {
    //     levelBanner.sprite = GameObject.Find("banner" + (moveAlpacaAround + 1)).GetComponent<SpriteRenderer>().sprite;
    //     levelImagePreview.sprite = GameObject.Find("lockedLevel").GetComponent<SpriteRenderer>().sprite;
    //   }
    //   else
    //   {
    //     Debug.Log("movealpacaaround is: " + moveAlpacaAround);
    //     levelBanner.sprite = GameObject.Find("banner" + (moveAlpacaAround + 1)).GetComponent<SpriteRenderer>().sprite;
    //     levelImagePreview.sprite = GameObject.Find("level" + (moveAlpacaAround + 1)).GetComponent<SpriteRenderer>().sprite;
    //   }
    // }
    // if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.DownArrow)) && moveAlpacaAround < 26)
    // {
    //   moveAlpacaAround = moveAlpacaAround + 1;
    //   positionAlpacaOnRecentLevel(moveAlpacaAround);
    //   if (moveAlpacaAround > levelPassed)
    //   {
    //     levelBanner.sprite = GameObject.Find("banner" + (moveAlpacaAround + 1)).GetComponent<SpriteRenderer>().sprite;
    //     levelImagePreview.sprite = GameObject.Find("lockedLevel").GetComponent<SpriteRenderer>().sprite;
    //   }
    //   else
    //   {
    //     levelBanner.sprite = GameObject.Find("banner" + (moveAlpacaAround + 1)).GetComponent<SpriteRenderer>().sprite;
    //     levelImagePreview.sprite = GameObject.Find("level" + (moveAlpacaAround + 1)).GetComponent<SpriteRenderer>().sprite;
    //   }
    // }

    // if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
    // && ((moveAlpacaAround) <= levelPassed))
    // {
    //   SceneManager.LoadScene("B" + (moveAlpacaAround + 1), LoadSceneMode.Single);
    // }

  }
}
