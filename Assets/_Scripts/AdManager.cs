using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour {



    [Header("Banner ID")]
    public string BannerId;
    private BannerView bannerViewVar;

    [Header("Interstitial ID")]
    public string InterstitialId;
    public int showInterstitialEveryXGameover;
    private InterstitialAd interstitial;



    public static int gameoverCounter = 0;

    private int itemIDset;
    private int reward;

    private int counter;
	private bool needsLoadInterstitial;

    RewardBasedVideoAd rewardVideo;
                
	// Use this for initialization
	void Start () {

        if (showInterstitialEveryXGameover == 0)
            showInterstitialEveryXGameover = 1;
	
		needsLoadInterstitial = true;
    
		counter = 0;

        rewardVideo = RewardBasedVideoAd.Instance;

        //Banner Management
        bannerViewVar = new BannerView(BannerId, AdSize.Banner, AdPosition.Bottom);
        AdRequest requestBanner = new AdRequest.Builder().Build();
        bannerViewVar.LoadAd(requestBanner);
        bannerViewVar.Show();

        //Init the Interstitial
        interstitial = new InterstitialAd(InterstitialId);

        //Show an interstitial every 3 minuts
        //InvokeRepeating("ShowInterstitial", 60, 180);
    }

	public void LoadInterstitial()
	{
		interstitial = new InterstitialAd(InterstitialId);
		AdRequest request = new AdRequest.Builder().Build();
		interstitial.LoadAd(request);

	}

	public void ShowInterstitial()
	{
		if (interstitial.IsLoaded ()) {
			interstitial.Show ();
			needsLoadInterstitial = true;
		} else {
			AdRequest request = new AdRequest.Builder().Build();
			interstitial.LoadAd(request);
		}
	}
	
	// Update is called once per frame
	void Update () {

        if (gameoverCounter == showInterstitialEveryXGameover){
            ShowInterstitial();
            gameoverCounter = 0;
        }

		if (needsLoadInterstitial) {
			LoadInterstitial ();
			needsLoadInterstitial = false;
		}

	}

    public void ShowBanner()
    {
        bannerViewVar.Show();
    }

    public void HideBanner()
    {
        bannerViewVar.Hide();
    }
}
