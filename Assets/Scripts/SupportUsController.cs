using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.UI;

public class SupportUsController : MonoBehaviour
{
    [SerializeField] GameObject supportUsMenu;
    //[SerializeField] Button supportUsButton;

    string appleID = "3203751";
    string googleID = "3203750";
    string videoAdID = "rewardedVideo";

    // Start is called before the first frame update
    void Start()
    {
        // TODO: switch to false when live
        Monetization.Initialize(googleID, true);
        Monetization.Initialize(appleID, true);
    }

    public void toggleMenu()
    {

        if (!supportUsMenu.activeSelf)
        {
            supportUsMenu.SetActive(true);
        } else
        {
            supportUsMenu.SetActive(false);
        }
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
