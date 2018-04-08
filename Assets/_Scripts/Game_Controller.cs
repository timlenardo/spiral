using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AppAdvisory.SharingSystem;

namespace EndlessManager
{

    public class Game_Controller : MonoBehaviour
    {

        public enum GameState { MENU, GAME, GAMEOVER, PAUSE, TUTORIAL, SHOP }
        public static GameState gameState;

        public bool DeletePrefs;
        public bool SkipTutorial;

        [Header(" Canvas Groups ")]
        public CanvasGroup MENU;
        public CanvasGroup GAME;
        public CanvasGroup GAMEOVER;
        public CanvasGroup PAUSE;
        public CanvasGroup SHOP;

        [Header(" Score Management ")]
        public static int SCORE;
        public static int BESTSCORE;
        public static int COINS;
		public static int LEVEL;
        int previousScore = -1;
        int previousCoins = -1;

        [Header(" Texts ")]
        public Text ScoreText;
        public Text BestScoreText;
        public Text GameoverText;
		public Text PercentCompleteText;
        public Text CoinsMenuText;
        public Text CoinsGameText;
        public Text CoinsShop;
        public Text PauseBestText;
        public Text MenuScoreText;

        [Header(" Rate Game Links ")]
        public string iOSRateLink;
        public string androidRateLink;
        public string facebookLink;

        [Header(" Triggers ")]
        public static bool saveTrigger;
        public static bool gameoverTrigger;
        public static bool playGameoverSound;
        // Used during the black image fade to avoid some issues
        bool canClick = true;

        [Header(" Black Image Fade ")]
        public Image fadeImage;

        [Header(" Sounds ")]
        public AudioSource gameoverSound;

        [Header(" Camera ")]
        Camera mainCamera;

        [Header(" Share ")]
        //public Animator VSSHAREAnimator;
        bool windowed;

        [Header(" Daily Rewards ")]
        public static bool playedOneGame;

        [Header(" Shop Management ")]
        public GameObject shopPrefab;

        // Use this for initialization
        void Start()
        {
            if (SkipTutorial)
                PlayerPrefs.SetInt("FIRSTTIME", 1);

            //Load the bestscore
            LoadBestScore();

            //Load the coins
            LoadCoins();

            //Set Menu
            //SetMenu();

            //Check if this is the first launch to launch the tutorial or not
            SetMenu();

            // Get the main camera
            mainCamera = Camera.main;
        }


        // Update is called once per frame
        void Update()
        {
            if (DeletePrefs)
                PlayerPrefs.DeleteAll();

            if (previousScore != SCORE)
            {
                UpdateTexts();

                if (SCORE > BESTSCORE)
                    BESTSCORE = SCORE;

                previousScore = SCORE;
            }



            if (previousCoins != COINS)
            {
                UpdateTexts();
            }


            if (gameoverTrigger)
            {
                SetGameover();
                gameoverTrigger = false;
            }



            if (saveTrigger)
            {
                SaveCoins();
                saveTrigger = false;
            }


            if (playGameoverSound)
            {
                PlayGameoverSound();
                playGameoverSound = false;
            }


        }


        public void SetMenu()
        {
           // if (canClick)
            //{
                //Set the game state
                gameState = GameState.MENU;

                //Manage Canvas groups
                EnableCG(MENU);
                DisableCG(GAME);
                DisableCG(GAMEOVER);
                DisableCG(PAUSE);
                DisableCG(SHOP);
                shopPrefab.SetActive(false);


                Time.timeScale = 1;
            //}
        }

        public void SetMenuFromPause()
        {
//            if (canClick)
//            {
                Time.timeScale = 1;

                //Set the gameover first
                SetGameover();

                //then set the menu
                SetMenu();
//            }
        }

        public void SetGame()
        {
            if (canClick)
            {
                //Set the game state
                gameState = GameState.GAME;


                //Manage Canvas groups
                EnableCG(GAME);
                DisableCG(MENU);
                DisableCG(GAMEOVER);
                DisableCG(PAUSE);


                //Reset the score
                SCORE = 0;

                //Set the time scale to 1
                Time.timeScale = 1;
            }


        }

        public void SetPause()
        {
            if (canClick)
            {
                //Set the game state
                gameState = GameState.PAUSE;

                //Manage Canvas groups
                EnableCG(PAUSE);

                SaveBestScore();
                UpdateTexts();

                //Stop time
                Time.timeScale = 0;
            }
        }

        public void SetGameFromPause()
        {
            if (canClick)
            {
                //Set the game state
                gameState = GameState.GAME;

                //Manage Canvas groups
                DisableCG(PAUSE);

                //Set time
                Time.timeScale = 1;
            }
        }

        public void SetGameover()
        {
            if (canClick)
            {
                //Set the game state
                gameState = GameState.GAMEOVER;

                // Increase the gameover counter
                AdManager.gameoverCounter++;




                //Save the best score
                SaveBestScore();
                SaveCoins();

                // Start the gameover coroutine
                StartCoroutine("GameoverCoroutine");

            }

        }


        IEnumerator GameoverCoroutine()
        {
            canClick = false;


//			Color c = fadeImage.color;
//			c.a += 0.5f;
//			GAME.alpha = 1 - c.a;
//			fadeImage.color = c;

//			PercentCompleteText.text = "Wololo";

			DisableCG(GAME);
			EnableCG(GAMEOVER);
			DisableCG(PAUSE);
			DisableCG(MENU);
				
            // Manage the daily rewards
            if (!playedOneGame) {
                playedOneGame = true;
                //DailyRewardsInterface.forceShowRewardPanel = true;
            }
				

            canClick = true;
            yield return null;
        }

		IEnumerator PlayAgainCoroutine()
		{
			SetMenu();
            Game_Controller.SCORE = 0;

            // Reset the player
            Player_Controller.resetTrigger = true;
            Camera_Behavior.resetCamera = true;

            // Reset the cheeses
            Cheese_Spawner.resetTrigger = true;
			yield return null;
		}

			




        public void SetShop()
        {
            if (canClick)
            {
                // Start the shop coroutine
                StartCoroutine("GoToShopCoroutine");
            }

        }

        public void SetMenuFromShop()
        {
            if (canClick)
            {
                // Start the shop coroutine
                StartCoroutine("BackToMenuFromShopCoroutine");
            }
        }


        IEnumerator GoToShopCoroutine()
        {
            // Enable the shop
            //ShopCircle.SetActive(true);

            canClick = false;

            float step = 0.04f;
            float timeStep = 0.005f;

            // Fade in and out the black screen
            while (fadeImage.color.a < 1)
            {
                Color c = fadeImage.color;
                c.a += step;
                fadeImage.color = c;

                yield return new WaitForSeconds(timeStep);
            }

            //Set the game state
            gameState = GameState.SHOP;

            // Show the shop
            shopPrefab.SetActive(true);

            //Manage Canvas groups
            //EnableCG(SHOP);
            DisableCG(GAME);
            DisableCG(GAMEOVER);
            DisableCG(PAUSE);
            //DisableCG(TUTORIAL);
            DisableCG(MENU);

            //mainCamera.transform.localEulerAngles = new Vector3(0, 180, 0);

            // Fade in and out the black screen
            while (fadeImage.color.a > 0)
            {
                Color c = fadeImage.color;
                c.a -= step;
                fadeImage.color = c;

                yield return new WaitForSeconds(timeStep);
            }


            canClick = true;

            yield return null;
        }



        IEnumerator BackToMenuFromShopCoroutine()
        {

            canClick = false;

            float step = 0.04f;
            float timeStep = 0.005f;

            // Fade in and out the black screen
            while (fadeImage.color.a < 1)
            {
                Color c = fadeImage.color;
                c.a += step;
                fadeImage.color = c;

                yield return new WaitForSeconds(timeStep);
            }

            //Set the game state
            gameState = GameState.MENU;

            // Hide the shop
            shopPrefab.SetActive(false);

            //Manage Canvas groups
            EnableCG(MENU);
            DisableCG(GAME);
            DisableCG(GAMEOVER);
            DisableCG(PAUSE);
            DisableCG(SHOP);

            //mainCamera.transform.localEulerAngles = new Vector3(0, 0, 0);

            // Fade in and out the black screen
            while (fadeImage.color.a > 0)
            {
                Color c = fadeImage.color;
                c.a -= step;
                fadeImage.color = c;

                yield return new WaitForSeconds(timeStep);
            }

            // Disable the shop
            //ShopCircle.SetActive(false);

            canClick = true;

            yield return null;
        }


        void UpdateTexts()
        {

            //Update text
            ScoreText.text = "" + SCORE;
            BestScoreText.text = "best: " + BESTSCORE;
            GameoverText.text = ScoreText.text;

            if (PauseBestText != null)
                PauseBestText.text = BestScoreText.text;

            if (MenuScoreText != null)
            {

                if (SCORE > 0)
                {
                    if (!MenuScoreText.gameObject.activeSelf)
                        MenuScoreText.gameObject.SetActive(true);

                    MenuScoreText.text = (SCORE - 1).ToString();

                }
                else
                {
                    if (MenuScoreText.gameObject.activeSelf)
                        MenuScoreText.gameObject.SetActive(false);

                    MenuScoreText.text = "";
                }
            }


            CoinsGameText.text = "" + COINS;
            CoinsMenuText.text = "" + COINS;
            CoinsShop.text = "" + COINS;

        }


        void SaveBestScore()
        {
            PlayerPrefs.SetInt("BESTSCORE", BESTSCORE);
        }

        void LoadBestScore()
        {
            BESTSCORE = PlayerPrefs.GetInt("BESTSCORE");
        }

        void SaveCoins()
        {
            PlayerPrefs.SetInt("COINS", COINS);
        }

        void LoadCoins()
        {
            COINS = PlayerPrefs.GetInt("COINS");
        }

		void SaveLevel()
		{
			PlayerPrefs.SetInt("LEVEL", LEVEL);
		}

		void LoadLevel()
		{
			LEVEL = PlayerPrefs.GetInt("LEVEL");
		}


        void EnableCG(CanvasGroup cg)
        {
            cg.alpha = 1;
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }

        void DisableCG(CanvasGroup cg)
        {
            cg.alpha = 0;
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }



        public void RateGame()
        {
#if UNITY_ANDROID
        Application.OpenURL(androidRateLink);
#endif

#if UNITY_IOS
        Application.OpenURL(iOSRateLink);
#endif
        }
			
        public void OpenFacebookLink()
        {
            Application.OpenURL(facebookLink);
        }


        void PlayGameoverSound()
        {
            if (gameoverSound != null)
                gameoverSound.Play();
        }


        public void OpenLeaderboard()
        {
            //LeaderboardManager.ShowLeaderboardUI();   
        }

        void SaveScoreToLeaderboard()
        {
            //LeaderboardManager.ReportScore(SCORE);
        }
    }
}