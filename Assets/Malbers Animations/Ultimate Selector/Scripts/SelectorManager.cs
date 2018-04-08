﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;
using MalbersAnimations.Utilities;
using MalbersAnimations.Events;
using UnityEngine.Events;
using EndlessManager;

namespace MalbersAnimations.Selector
{
    /// <summary>
    /// Master Script to Control and Hold the Character Selector Scripts
    /// </summary>
    [HelpURL("https://docs.google.com/document/d/e/2PACX-1vTwj1r5z3KzslDAaI4bFPk6Un9GhQZHa6EIy39UGvpAlkcAuh3ttieQ3MSPwWauOR0tCMMpyzkFwtb0/pub#h.e1hns3wj26y0")]
    public class SelectorManager : MonoBehaviour
    {
        #region Variables 
        public GameObject OriginalItemSelected;                     //Original Object of the Selected Item
        public MItem ItemSelected;                                  //The item Selected

        public bool InstantiateItems = true;                        //Instantiate the original items on the scene
        public bool RemoveLast = false;

        private GameObject lastSpawnedItem;                         //the Last Spawned item
        private MItem lastItemAdded;                                //Store the last item added

        public Transform SpawnPoint;                                //Where do you want to spawn the Item.

        public GameObjectEvent OnSelected = new GameObjectEvent();
        public GameObjectEvent OnOpen = new GameObjectEvent();
        public GameObjectEvent OnClosed = new GameObjectEvent();

        private SelectorController controller;
        private SelectorEditor editor;
        private SelectorUI ui;

        /// <summary>
        /// Scriptable Object that contains the Save Data
        /// </summary>
        public SelectorData Data;

        /// <summary>
        /// Transform to animate the Open and Close Animations for the Selector
        /// </summary>
        public Transform Target;


        public TransformAnimation EnterAnimation;
        public TransformAnimation ExitAnimation;

        DeltaTransform LastTarget = new DeltaTransform();

        public InputRow Input = new InputRow("Cancel", KeyCode.Escape, InputButton.Down);

        public bool enableSelector = true;

        public GameObject player;

        #region Properties
        /// <summary>
        /// Store the Last Instantiate Item
        /// </summary>
        public GameObject Last_SpawnedItem
        {
            get { return lastSpawnedItem; }
            set { lastSpawnedItem = value; }
        }

        public SelectorController Controller
        {
            get
            {
                if (!controller) controller = GetComponentInChildren<SelectorController>();
                return controller;
            }
        }
        public SelectorEditor Editor
        {
            get
            {
                if (!editor) editor = GetComponentInChildren<SelectorEditor>();
                return editor;
            }
        }
        public SelectorUI UI
        {
            get
            {
                if (!ui) ui = GetComponentInChildren<SelectorUI>();
                return ui;
            }
        }

        /// <summary>
        /// Check if the Selector is Animating When Open or Close
        /// </summary>
        protected bool isAnimating = false;

        /// <summary>
        /// Hide/Show the Selector system
        /// </summary>
        public bool EnableSelector
        {
            get { return enableSelector; }
            set
            {
                if (enableSelector != value && !isAnimating)
                {
                    enableSelector = value;
                    StopAllCoroutines();

                    if (enableSelector)         //If the Selector is Open
                    {
                        StartCoroutine(PlayAnimation(EnterAnimation, true));
                        OnOpen.Invoke(Last_SpawnedItem);
                    }
                    else
                    {
                        StartCoroutine(PlayAnimation(ExitAnimation, false));
                        OnClosed.Invoke(Last_SpawnedItem);
                        if (Data) Data.UpdateData(this);            //Update Data when the selector closed
                    }
                }
            }
        }

        public MItem LastAddedItem
        {
            get { return lastItemAdded; }
            private set { lastItemAdded = value; }
        }

        #endregion
        #endregion

        //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        void Awake()
        {


            // Find the player
            player = GameObject.FindWithTag("Player");

            if (Controller || Editor || UI) { }         //Store all the Selector Components on variables

            if (Data)
            {
                Data.LoadDataPlayerPref();              //Checks if you're using PlayerPref and load the Data from there
                Data.UpdateItems(this);                 //If you are using Data Update it   

                // Update the data coins
                Data.Save.Coins = Game_Controller.COINS;

            }


            LastTarget.StoreTransform(Target);          //Store the Transform of the Target to Animate.     

            if (!EnableSelector)
            {
                if (Controller) Controller.gameObject.SetActive(false);
                if (UI) UI.gameObject.SetActive(false);
            }

            if (Controller.CurrentItem)
                UpdateSelectedItem(Controller.CurrentItem.gameObject);          //Update the Focused item on the Manager

            if (ItemSelected)
            {
                UI.UpdateSelectedItemUI(ItemSelected.gameObject);       //Updates the UI.
            }
        }

        //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        void Update()
        {
            if (Input.GetInput) ToggleSelector();

            if (Data.Save.Coins != Game_Controller.COINS)
                Data.Save.Coins = Game_Controller.COINS;
        }

        //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        /// <summary>
        /// Toggle the Selector On and Off
        /// </summary>
        public virtual void ToggleSelector()
        {
            EnableSelector = !EnableSelector;
        }

        //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        void OnDisable()
        {
            if (Controller) Controller.OnItemFocused.RemoveListener(UpdateSelectedItem);
        }

        //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        void OnEnable()
        {
            if (Data) Data.UpdateItems(this);

            if (Controller)
            {
                Controller.OnItemFocused.AddListener(UpdateSelectedItem);
                Controller.UpdateLockItems();
            }
        }

        //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        private void UpdateSelectedItem(GameObject item)
        {
            if (item == null) return;
            ItemSelected = item.GetComponent<MItem>();

            if (ItemSelected)
            {
                OriginalItemSelected = ItemSelected.OriginalItem;
            }
        }

        /// <summary>
        /// Purchase item
        /// </summary>
        public void Purchase(MItem item)
        {
            if (Data)
            {
                if (Data.Save.Coins - item.Value >= 0)          //If we have money to buy?
                {
                    // Diminish the game controller coins
                    Game_Controller.COINS -= item.Value;
                    Game_Controller.saveTrigger = true;

                    Data.Save.Coins -= item.Value;

                    // Set the value of the item to 0
                    item.Value = 0;

                    item.Locked = false;                //Unlock the Item

                    // Change the player material
                    player.GetComponent<MeshRenderer>().material = item.gameObject.GetComponent<MeshRenderer>().material;

                    // Change the particle system color
                    player.GetComponent<Paint_Management>().SetParticleSystemColor();
                }
                Data.UpdateData(this);
            }
            else
            {
                item.Locked = false;
                Controller.UpdateLockItems();
            }
        }

        /// <summary>
        /// Spawns the Original Object of the Item
        /// </summary>
        protected virtual void InstantiateItem()
        {
            if (ItemSelected == null) return;                           //If there's no Selected Item Skip

            if (RemoveLast && Last_SpawnedItem)                                 //If Remove Last Spawned is enable remove the Last Instantiate items
            {
                Destroy(Last_SpawnedItem);
            }
            Debug.Log("INS");

            //Instantiate the Item Origninal GameObject on the SpawnPoint, if there's no spawnpoint use this transform
            Last_SpawnedItem =
                Instantiate(ItemSelected.originalItem ? ItemSelected.originalItem : ItemSelected.gameObject,
                SpawnPoint ? SpawnPoint.position : transform.position,
                SpawnPoint ? SpawnPoint.rotation : Quaternion.identity);

            Sync_Material_ActiveMeshes();                                                    //Sync all Material Changers on Item and Original GO
        }


        /// <summary>
        /// Parent the Selected Item Instance to the Spawn Point
        /// </summary>
        /// <param name="ResetScale"></param>
        public virtual void _ParentToSpawnPoint(bool ResetScale = false)
        {
            if (!SpawnPoint) return;

            lastSpawnedItem.transform.SetParent(SpawnPoint, true);
            if (ResetScale) lastSpawnedItem.transform.localScale = Vector3.one;
        }


        /// <summary>
        /// Reduce the Amount of the Focused item 
        /// </summary>
        public virtual void _ReduceAmountSelected()
        {
            if (ItemSelected && ItemSelected.Amount > 0)
            {
                ItemSelected.Amount--;
            }

            if (Data) Data.UpdateData(this);      //Update the Data !!
        }

        /// <summary>
        /// Sync the values of the Item and Original Object if they have the Material & Mesh Changer
        /// </summary>
        protected virtual void Sync_Material_ActiveMeshes()
        {
            if (Last_SpawnedItem)
            {
                MaterialChanger OriginalMC = Last_SpawnedItem.GetComponent<MaterialChanger>();
                MaterialChanger ItemSelMC = ItemSelected.GetComponent<MaterialChanger>();

                if (OriginalMC && ItemSelMC)
                {
                    for (int i = 0; i < ItemSelMC.materialList.Count; i++)
                    {
                        OriginalMC.SetMaterial(i, ItemSelMC.CurrentMaterialIndex(i)); //Set the Materials from the Item to the Original
                    }
                }

                ActiveMeshes OriginalAM = Last_SpawnedItem.GetComponent<ActiveMeshes>();
                ActiveMeshes ItemSelAM = ItemSelected.GetComponent<ActiveMeshes>();

                if (OriginalAM && ItemSelAM)
                {
                    for (int i = 0; i < ItemSelAM.Meshes.Count; i++)
                    {
                        OriginalAM.ChangeMesh(i, ItemSelAM.GetActiveMesh(i).Current); //Set the Meshes from the Item to the Original 
                    }
                }
            }
        }

        /// <summary>
        /// Save to the Data the Default Data
        /// </summary>
        public virtual void SaveDefaultData()
        {
            if (Data) Data.SetDefaultData(this);
        }

        /// <summary>
        /// Restore to the Data using the Default Data
        /// </summary>
        public virtual void RestoreToDefaultData()
        {
            if (Data) Data.RestoreData(this);                           //Restore the Data 

            if (Application.isPlaying)
            {
                Controller.UpdateLockItems();                           //Updates the Items if they were locked
                UI.UpdateSelectedItemUI(ItemSelected.gameObject);       //Updates the UI.
            }
        }


        /// <summary>
        /// Change the material on the selected Item, also updates the data
        /// </summary>
        /// <param name="next">true: Next, false: Previous</param>
        public virtual void _ChangeCurrentItemMaterial(bool Next)
        {
            if (Controller.IndexSelected == -1) return;    //Skip if the Selection is clear
            Controller.CurrentItem.SetAllMaterials(Next);
            if (Data) Data.UpdateData(this);

        }


        /// <summary>
        /// Change on Material Changer a Material item to the next using the Name
        /// </summary>
        /// <param name="Name">name of the Material Item</param>
        public virtual void _ChangeCurrentItemMaterial(string Name)
        {
            _ChangeCurrentItemMaterial(Name, true);
        }

        /// <summary>
        /// Change on Material Changer a Material item to the next or before using the Name
        /// </summary>
        /// <param name="Name">name of the Material Item</param>
        public virtual void _ChangeCurrentItemMaterial(string Name, bool Next)
        {
            if (Controller.IndexSelected == -1) return;    //Skip if the Selection is clear
            Controller.CurrentItem.SetMaterial(Name, Next);
            if (Data) Data.UpdateData(this);
        }

        /// <summary>
        /// Change on Material Changer a Material item to the next material using the Index
        /// </summary>
        /// <param name="Name">name of the Material Item</param>
        public virtual void _ChangeCurrentItemMaterial(int Index)
        {
            _ChangeCurrentItemMaterial(Index, true);
        }

        /// <summary>
        /// Change on Material Changer a Material item to the next or before Material using the Index
        /// </summary>
        /// <param name="Name">name of the Material Item</param>
        public virtual void _ChangeCurrentItemMaterial(int Index, bool Next)
        {
            if (Controller.IndexSelected == -1) return;    //Skip if the Selection is clear
            Controller.CurrentItem.SetMaterial(Index, Next);
            if (Data) Data.UpdateData(this);
        }

        /// <summary>
        /// If the  Current Item has an ActiveMeshes component, it change an Active Mesh on the list to the Next Mesh using the List “Index”.
        /// </summary>
        public virtual void _ChangeCurrentItemMesh(int index)
        {
            _ChangeCurrentItemMesh(index, true);
        }

        /// <summary>
        /// If the  Current Item has an ActiveMeshes component, it change an Active Mesh on the list to the Next(true) Before(false) Mesh using the List “Index”.
        /// </summary>
        public virtual void _ChangeCurrentItemMesh(int index, bool Next)
        {
            if (Controller.IndexSelected == -1) return;    //Skip if the Selection is clear
            Controller.CurrentItem.ChangeMesh(index, Next);
            if (Data) Data.UpdateData(this);
        }

        /// <summary>
        /// Change a  Mesh by Name on the Active Meshes List
        /// </summary>
        public virtual void _ChangeCurrentItemMesh(string name)
        {
            if (Controller.IndexSelected == -1) return;                                    //Skip if the Selection is clear
            Controller.CurrentItem.ChangeMesh(name);
            if (Data) Data.UpdateData(this);
        }

        /// <summary>
        /// Change All Meshes on the Active Meshes List
        /// </summary>
        public virtual void _ChangeCurrentItemMesh(bool next)
        {
            if (Controller.IndexSelected == -1) return;                                 //Skip if the Selection is clear
            Controller.CurrentItem.ChangeMesh(next);
            if (Data) Data.UpdateData(this);
        }

        /// <summary>
        /// Select the Item if is not locked and the amount is greater than zero, also instantiate it if Instantiate Item is enabled.
        /// </summary>
        public virtual void SelectItem()
        {
            if (!ItemSelected) return;                                          //If there's no Item Skip

            if (!ItemSelected.Locked && ItemSelected.Amount > 0)                //If the items is not locked and the we have one or More Item ....
            {
                if (InstantiateItems) InstantiateItem();
                OnSelected.Invoke(Last_SpawnedItem);

                Debug.Log("Item Selected");

                // Change the player material
                player.GetComponent<MeshRenderer>().material = ItemSelected.gameObject.GetComponent<MeshRenderer>().material;

                // Change the particle system color
                player.GetComponent<Paint_Management>().SetParticleSystemColor();
            }

            if (UI) UI.UpdateSelectedItemUI(ItemSelected.gameObject);       //Update the UI
        }

       


        /// <summary>
        /// Change to the next scene using the scene name
        /// </summary>
        public virtual void _ChangeToScene(string SceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
        }

        /// <summary>
        /// Change to the next scene using the scene Index
        /// </summary>
        public virtual void _ChangeToScene(int SceneIndex)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneIndex);
        }


        /// <summary>
        /// Adds an Item at Runtime
        /// </summary>
        /// <param name="item">the Item to Add</param>
        public virtual void _AddItem(MItem item)
        {
            if (!item) return;
            AddItemCommon(item.gameObject);
        }

        /// <summary>
        /// Adds an Item at Runtime
        /// </summary>
        /// <param name="item">the Item to Add</param>
        public virtual void _AddItem(GameObject item)
        {
            if (!item) return;
            AddItemCommon(item);
        }

        /// <summary>
        /// Removes an Item at Runtime using its Mitem script
        /// </summary>
        /// <param name="item">item to remove</param>
        public virtual void _RemoveItem(MItem item)
        {
            MItem removeItem = editor.Items.Find(i => i == item);

            if (removeItem)
            {
                RemoveItemCommon(removeItem.gameObject);
            }
        }

        /// <summary>
        /// Removes an Item at Runtime using the name
        /// </summary>
        /// <param name="item">item to remove</param>
        public virtual void _RemoveItem(string itemName)
        {
            MItem removeItem = editor.Items.Find(item => item.name == itemName);

            if (removeItem)
            {
                RemoveItemCommon(removeItem.gameObject);
            }
        }

        /// <summary>
        /// Removes an Item at Runtime using the list index position
        /// </summary>
        /// <param name="item">item to remove</param>
        public virtual void _RemoveItem(int Index)
        {
            if (Index >= 0 && Index < editor.Items.Count)
            {
                RemoveItemCommon(editor.Items[Index].gameObject);
            }
        }

        private void AddItemCommon(GameObject gameObject)
        {
            gameObject = Instantiate(gameObject, Editor.transform, false);

            Editor.UpdateItemsList();                                           //Update the list!
            controller.ResetController();
            controller.CheckFirstItemFocus();
            LastAddedItem = gameObject.GetComponent<MItem>();
        }

        private void RemoveItemCommon(GameObject gameObject)
        {
            MItem toRemove = gameObject.GetComponent<MItem>();                          //Find the Item to remove.

            int toRemoveIndex = Editor.Items.FindIndex(item => item == toRemove);       //Find the index of the item to remove..

            if (Controller.FocusedItemIndex > toRemoveIndex) Controller.FocusedItemIndex--;

            Destroy(gameObject);

            StartCoroutine(DestroyItemsAfter());                            //Destroy first then the next frame update all
        }

        /// <summary>
        /// Plays an Enter or Exit Animation for the selector
        /// </summary>
        private IEnumerator PlayAnimation(TransformAnimation animTransform, bool Enter)
        {
            if (Enter)                                                      //Enable the controller and UI GAME OBJECTS
            {
                if (Controller)
                {
                    Controller.gameObject.SetActive(true);
                    if (Controller.FocusedItemIndex == -1) ItemSelected = null;
                    Controller.enabled = true;                       //Enable the Controller component after the animation.
                }
                if (UI)
                {
                    UI.UpdateSelectedItemUI(ItemSelected);
                    UI.gameObject.SetActive(true);
                }
            }
            else
            {
                //if (Controller)
                //{
                //    Controller.enabled = false;
                //}
            }


            if (animTransform && Target)
            {
                isAnimating = true;

                LastTarget.StoreTransform(Target);


                if (animTransform.delay > 0) yield return new WaitForSeconds(animTransform.delay);

                float elapsedTime = 0;

                while ((animTransform.time > 0) && (elapsedTime <= animTransform.time))

                {
                    float resultPos = animTransform.PosCurve.Evaluate(elapsedTime / animTransform.time);               //Evaluation of the Pos curve
                    float resultRot = animTransform.RotCurve.Evaluate(elapsedTime / animTransform.time);               //Evaluation of the Rot curve
                    float resultSca = animTransform.ScaleCurve.Evaluate(elapsedTime / animTransform.time);               //Evaluation of the Scale curve

                    if (animTransform.UsePosition)
                    {
                        Target.localPosition =
                        Vector3.LerpUnclamped(LastTarget.LocalPosition, LastTarget.LocalPosition + animTransform.Position, resultPos);
                    }

                    if (animTransform.UseRotation)
                    {
                        Target.localEulerAngles = Vector3.LerpUnclamped(LastTarget.LocalEulerAngles, animTransform.Rotation, resultRot);
                    }

                    if (animTransform.UseScale)
                        Target.localScale = Vector3.LerpUnclamped(LastTarget.LocalScale, Vector3.Scale(LastTarget.LocalScale, animTransform.Scale), resultSca);

                    elapsedTime += Time.deltaTime;

                    yield return null;
                }

                isAnimating = false;
                LastTarget.RestoreLocalTransform(Target);

            }

            if (!Enter)
            {
                if (Controller)
                {
                    Controller.gameObject.SetActive(false);
                    Controller.enabled = false;                      //Disable the Controller component after the animation.
                }
                if (UI) UI.gameObject.SetActive(false);

            }
            else
            {
                //if (Controller)
                //{
                //    Controller.enabled = true;                       //Enable the Controller component after the animation.
                //}
                if (UI)
                {
                    UI.gameObject.SetActive(true);
                    UI.UpdateSelectedItemUI(ItemSelected);
                }
            }

            yield return null;
        }

        private IEnumerator DestroyItemsAfter()
        {
            yield return null;

            editor.UpdateItemsList();                                           //Update the list!
            controller.ResetController();

            foreach (var item in editor.Items)
            {
                item.RestoreInitialTransform();
            }
        }

        private void OnApplicationQuit()
        {
            if (EnableSelector)
            {
                if (Data) Data.UpdateData(this);
            }
        }

        [HideInInspector] public bool ShowEvents = true;
        [HideInInspector] public bool ShowAnims = true;
    }
}