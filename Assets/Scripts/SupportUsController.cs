using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.UI;

public class SupportUsController : MonoBehaviour
{
    [SerializeField] GameObject supportUsMenu;
    [SerializeField] GameObject supportOptionsMenu;
    [SerializeField] GameObject donateMenu;
    [SerializeField] Image buttonIn;
    [SerializeField] Image buttonOut;

    string appleID = "3203751";
    string googleID = "3203750";
    string videoAdID = "rewardedVideo";

    // Start is called before the first frame update
    void Start()
    {
        // TODO: switch to false when live
        Monetization.Initialize(googleID, true);
        Monetization.Initialize(appleID, true);
        supportUsMenu.SetActive(false);
        supportOptionsMenu.SetActive(false);
        donateMenu.SetActive(false);
        buttonIn.enabled = true;
        buttonOut.enabled = false;
    }

    /**
     * Used for the button entering the support screen,
     * and both back buttons in the support screens (on options and donate page)
     */
    public void toggleMenu()
    {

        if (!supportUsMenu.activeSelf) // no support screen open -> open options
        {
            supportUsMenu.SetActive(true);
            supportOptionsMenu.SetActive(true);
            donateMenu.SetActive(false);
            buttonIn.enabled = false;
            buttonOut.enabled = true;
        } else if(donateMenu.activeSelf) // donate screen open -> go to options
        {
            supportOptionsMenu.SetActive(true);
            donateMenu.SetActive(false);
        } else // support options open -> close support screen
        {
            supportUsMenu.SetActive(false);
            supportOptionsMenu.SetActive(false);
            donateMenu.SetActive(false);
            buttonIn.enabled = true;
            buttonOut.enabled = false;
        }
    }

    /**
     * Used for support options page -> donate page
     */
    public void goDonatePage()
    {
        supportOptionsMenu.SetActive(false);
        donateMenu.SetActive(true);
    }

    public void showAd()
    {
        if (Monetization.IsReady(videoAdID))
        {
            ShowAdPlacementContent ad = null;
            ad = Monetization.GetPlacementContent(videoAdID) as ShowAdPlacementContent;

            if (ad != null)
            {
                ad.Show();
            }
        }
    }

    public void leaveReivew()
    {
        // TODO: take to play/app store
    }
}
