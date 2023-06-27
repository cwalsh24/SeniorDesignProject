using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Keeping Bomb as placeholder if you wanted to implement something like item limited amounts 
    public enum TypeOfPickUp{Rupee, Bomb, Heart, Heart_Container};
    public TypeOfPickUp typeOfPickUp;
    public bool canPickup;

    public const string playerString = "Player";

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag(playerString) && canPickup) {
            PickUpEffect(typeOfPickUp);
            Destroy(gameObject);
        }
    }

    // PickUpEffect() uses the typeOfPickup to perform the corresponding effect
    public void PickUpEffect(TypeOfPickUp pickUpType)
    {
        if (pickUpType == TypeOfPickUp.Rupee)
        {
            PickUpRupee();
        }
        else if (pickUpType == TypeOfPickUp.Heart)
        {
            PickUpHeart();
        }
        else if (pickUpType == TypeOfPickUp.Heart_Container)
        {
            IncreaseHealth();
        }
    }

    private void PickUpRupee() {
        FindObjectOfType<RupeeWallet>().IncreaseRupeeCount(1);
    }

    private void PickUpHeart()
    {
        FindObjectOfType<PlayerHealth>().AddHealth(1);
    }

    private void IncreaseHealth()
    {
        // Adds 1 to maxHealth and then increases the current health by 1
        FindObjectOfType<PlayerHealth>().maxHealth += 1;
        FindObjectOfType<PlayerHealth>().AddHealth(1);
    }
}
