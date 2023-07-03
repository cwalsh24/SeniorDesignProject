using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Put on gameobjects that can be toggled with opening the dialogue window (currently spacebar).  If isPerson isn't toggled true, the name box window will not appear.
public class ShopActivator : MonoBehaviour
{
    #region Public Variables

    public string itemName;
    public string description;
    public int cost;
    [SerializeField] public Sprite itemSprite;

    #endregion

    #region Private Variables 

    private bool canActivate;
    private const string playerString = "Player";

    #endregion

    #region Unity Methods

    #endregion

    #region Private Methods 

    private void OpenDialogue() {
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == playerString) {
            ShopManager.Instance.ShowDialogue(itemName, description, cost, itemSprite);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == playerString) {
            ShopManager.Instance.CloseDialogue();
        }
    }

    #endregion
}
