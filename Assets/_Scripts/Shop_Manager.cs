using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessManager
{

    public class Shop_Manager : MonoBehaviour
    {

        [Header(" Player ")]
        public SpriteRenderer Player;

        [Header(" Buttons ")]
        Button[] CharacterButtons;
        Button[] LockButtons;

        [Header(" Sprites ")]
        public Sprite[] CharacterSprites;

        [Header(" Selection Circle ")]
        public GameObject selectionCircle;

        // Use this for initialization
        void Start()
        {

            CharacterButtons = new Button[transform.childCount];
            LockButtons = new Button[transform.childCount];

            //Store the buttons to manipulate them
            StoreButtons();

            //Add the listeners to the buttons
            AddListeners();

            //Load the values to destroy the lock buttons
            LoadValues();

        }


        void StoreButtons()
        {

            //Start with the Character buttons
            for (int i = 0; i < transform.childCount; i++)
            {
                CharacterButtons[i] = (transform.GetChild(i).GetComponent<Button>() != null) ? transform.GetChild(i).GetComponent<Button>() : null;
            }


            //Then store the lock buttons
            for (int i = 0; i < transform.childCount; i++)
            {
                LockButtons[i] = (transform.GetChild(i).GetChild(0).GetComponent<Button>() != null) ? transform.GetChild(i).GetChild(0).GetComponent<Button>() : null;
            }


        }

        void AddListeners()
        {

            int k = 0;
            foreach (Button b in CharacterButtons)
            {
                int _k = k;

                b.onClick.AddListener(delegate { SetPlayerSprite(_k); });

                k++;
            }


            int j = 0;
            foreach (Button b in LockButtons)
            {
                int _j = j;

                b.onClick.AddListener(delegate { TryPurchaseCharacter(_j); });

                j++;
            }

        }


        void SetPlayerSprite(int index)
        {

            //Set the sprite 
            Player.sprite = CharacterSprites[index];

            selectionCircle.transform.SetParent(CharacterButtons[index].transform);
            selectionCircle.transform.localPosition = Vector2.zero;

        }

        void TryPurchaseCharacter(int index)
        {

            if (Game_Controller.COINS >= CharacterButtons[index].GetComponent<Character_Shop>().price)
            {

                //Destroy the Lock Button
                Destroy(LockButtons[index].gameObject);

                //Set the sprite of the character Button
                CharacterButtons[index].image.sprite = CharacterSprites[index];

                //Save the state of this object
                SaveButtonState(index);

                //Diminish the amount of coins
                Game_Controller.COINS -= CharacterButtons[index].GetComponent<Character_Shop>().price;
                Game_Controller.saveTrigger = true;


            }


        }



        void LoadValues()
        {

            SaveButtonState(0);

            for (int i = 0; i < CharacterButtons.Length; i++)
            {
                if (LoadButtonState(i) == 1)
                {

                    //Destroy the lock button
                    Destroy(LockButtons[i].gameObject);

                    //Set the sprite of the character Button
                    CharacterButtons[i].image.sprite = CharacterSprites[i];
                }
            }

        }


        void SaveButtonState(int index)
        {
            PlayerPrefs.SetInt("SHOP" + index, 1);
        }

        int LoadButtonState(int index)
        {
            return PlayerPrefs.GetInt("SHOP" + index);
        }
    }
}