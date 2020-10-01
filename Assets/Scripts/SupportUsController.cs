using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class SupportUsController : MonoBehaviour, IUnityAdsListener
{
    /** Store information */
    private string appleID = "3203751";
    private string googleID = "3203750";
    private string videoAdPlacementID = "rewardedVideo";
    private string appID = "com.MangoSnoopers.ALonelyAlpaca";
    [SerializeField] private bool testMode = true;

    /* UI Stuff */
    [SerializeField] GameObject supportUsMenu;
    [SerializeField] GameObject supportOptionsMenu;
    [SerializeField] GameObject donateMenu;
    [SerializeField] GameObject thanksImage;
    
    /* Support Button Position */
    public RectTransform supportButtTransform;
    /* start and end positions of the support buttons */
    public Vector3 unselectedPosition = new Vector3(-50.0f, -37.0f, 0);
    public Vector3 selectedPosition = new Vector3(-50.0f, -55.0f, 0);

    /* state of whether button is coming down or going up */
    private bool comingDown;
    private bool goingUp;
    private float lerp_timer;


    void Start()
    {
    // UI
    supportUsMenu.SetActive(false);
    supportOptionsMenu.SetActive(false);
    donateMenu.SetActive(false);

    // Monetization
        Advertisement.AddListener(this);
#if UNITY_ANDROID
        Advertisement.Initialize(googleID, testMode);
#elif UNITY_IPHONE
        Advertisement.Initialize(appleID, testMode);
#endif
    }


    private void Update() 
    {
        ResolveSupportButton();
    }


/* ===================================================================================================== */
#region  UI Control

    /**
     * Used for the button entering the support screen,
     * and both back buttons in the support screens (on options and donate page)
     */
    public void toggleMenu()
    {
        lerp_timer = 0;
        if (!supportUsMenu.activeSelf) // no support screen open -> open options
        {
            supportUsMenu.SetActive(true);
            supportOptionsMenu.SetActive(true);
            donateMenu.SetActive(false);
            thanksImage.SetActive(false);
            comingDown = true;
            goingUp = false;
        } else if(donateMenu.activeSelf) // donate screen open -> go to options
        {
            supportOptionsMenu.SetActive(true);
            donateMenu.SetActive(false);
            thanksImage.SetActive(false);
        } else // support options open -> close support screen
        {
            supportUsMenu.SetActive(false);
            supportOptionsMenu.SetActive(false);
            donateMenu.SetActive(false);
            thanksImage.SetActive(false);
            comingDown = false;
            goingUp = true;
        }
    }


    /** Brings the support button up or down if needed */
    void ResolveSupportButton () {
        if(comingDown) {
            lerp_timer += 2 * Time.deltaTime;
			supportButtTransform.anchoredPosition = Vector3.Lerp(supportButtTransform.anchoredPosition, selectedPosition, lerp_timer);
        }
        else if(goingUp) {
            lerp_timer += 2 * Time.deltaTime;
			supportButtTransform.anchoredPosition = Vector3.Lerp(supportButtTransform.anchoredPosition, unselectedPosition, lerp_timer);
        }
        else {
            lerp_timer = 0;
        }
    }


    /**  Used togo from support options page -> donate page (*/
    public void goDonatePage()
    {
        supportOptionsMenu.SetActive(false);
        donateMenu.SetActive(true);
    }


    private void DisplayThanksImage()
    {
        supportOptionsMenu.SetActive(false);
        thanksImage.SetActive(true);
    }


#endregion // UI Cotrol
/* ===================================================================================================== */
#region Monetization Control

    /* ================================================================================================= */
    #region  Advertisements 


    public void ShowAd()
    {
        if (Advertisement.IsReady(videoAdPlacementID)) {
            Advertisement.Show(videoAdPlacementID);
        }
        else {
            Debug.Log("Rewarded video not ready. Try again later!");
        }        
    }


    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) 
    {
        if (showResult == ShowResult.Finished) {
            DisplayThanksImage();
        } 
        else if (showResult == ShowResult.Skipped) {
            // Do not reward the user for skipping the ad.
        } 
        else if (showResult == ShowResult.Failed) {
            Debug.LogWarning ("The ad did not finish due to an error.");
        }
    }


    public void OnUnityAdsReady (string placementId) {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == videoAdPlacementID) {
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
        }
    }


    public void OnUnityAdsDidError (string message) {
        // Log the error.
    }


    public void OnUnityAdsDidStart (string placementId) {
        // Optional actions to take when the end-users triggers an ad.
    } 


    #endregion //Advertisements
    /* ================================================================================================= */
    #region Reviews


    public void OpenReviewPage() 
    {
#if UNITY_ANDROID
        Application.OpenURL("market://details?id=" + appID);
#elif UNITY_IPHONE
        Application.OpenURL("itms-apps://itunes.apple.com/app/id" + appID);
#else
        Debug.Log("NOT ON MOBILE DEVICE");
#endif
    }


    #endregion // Reviews
    /* ================================================================================================= */
    #region In App Purchases


    /* Donation Methods */
    public void DonatePressed1Dollar() 
    {
        IAPManager.Instance.PurchaseDonation1Dollar();
        DisplayThanksImage();
    }

    public void DonatePressed3Dollar() 
    {
        IAPManager.Instance.PurchaseDonation3Dollar();
        DisplayThanksImage();
    }

    public void DonatePressed5Dollar() 
    {
        IAPManager.Instance.PurchaseDonation5Dollar();
        DisplayThanksImage();
    }


    #endregion // In App Purchases
    /* ================================================================================================= */


#endregion // Monetization Control
/* ===================================================================================================== */
}
