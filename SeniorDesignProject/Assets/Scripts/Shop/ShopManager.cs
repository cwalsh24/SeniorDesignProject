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
    public void ShowDialogue(PickUp.TypeOfPickUp itemType, Sprite itemSprite, int cost) {
        UpdateInfo(itemType, itemSprite, cost);
        
        itemBox.SetActive(true);
        nameBox.SetActive(true);
    }

    public void CloseDialogue()
    {
        itemBox.SetActive(false);
        nameBox.SetActive(false);
    }

    #endregion

    #region Private Methods

    private void UpdateInfo(PickUp.TypeOfPickUp itemType, Sprite itemSprite, int cost)
    {
        switch(itemType)
        {
            // ADD ITEM INFO HERE BASED ON PickUp.TypeOfPickUp TYPE
            case PickUp.TypeOfPickUp.Heart_Container:
                this.itemName.text = "Heart Container";
                this.description.text = "Increases your maximum amount of hearts.";
                this.cost.text = cost.ToString();
                this.itemSprite.sprite = itemSprite;
                break;
            default:
                this.itemName.text = "Item info not set in Shop Manager...";
                break;
        }
    }

    #endregion
}