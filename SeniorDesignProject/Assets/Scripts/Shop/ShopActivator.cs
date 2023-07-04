using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Put on gameobjects that can be toggled with opening the dialogue window (currently spacebar).  If isPerson isn't toggled true, the name box window will not appear.
public class ShopActivator : MonoBehaviour
{
    #region Public Variables

    public PickUp.TypeOfPickUp itemType;
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

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == playerString) {
            ShopManager.Instance.ShowDialogue(itemType, itemSprite, cost);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == playerString) {
            ShopManager.Instance.CloseDialogue();
        }
    }

    #endregion
}
