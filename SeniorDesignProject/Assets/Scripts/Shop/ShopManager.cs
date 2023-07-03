using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : Singleton<ShopManager>
{
    #region Public Variables 

    [SerializeField] public GameObject itemBox;

    #endregion

    #region Private Variables

    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text cost;
    [SerializeField] private Image itemSprite;

    [SerializeField] private GameObject nameBox;

    #endregion

    #region Public Methods
    
    // newLines is passed through from the DialogueActivator class that calls this function
    public void ShowDialogue(string itemName, string description, int cost, Sprite itemSprite) {
        this.itemName.text = itemName;
        this.description.text = description;
        this.cost.text = cost.ToString();
        this.itemSprite.sprite = itemSprite;
        itemBox.SetActive(true);
        nameBox.SetActive(true);
    }

    public void CloseDialogue()
    {
        itemBox.SetActive(false);
        nameBox.SetActive(false);
    }

    #endregion
}