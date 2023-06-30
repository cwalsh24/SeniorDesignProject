using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Keeping Bomb as placeholder if you wanted to implement something like item limited amounts 
    public enum TypeOfPickUp{Rupee, Bomb, Heart, Heart_Container};
    public TypeOfPickUp typeOfPickUp;
    public bool preventPickup;
    public int cost;

    //Adding sound effects
    public AudioClip pickupSound; 

    public const string playerString = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerString) && !preventPickup)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, 1f);

            // check cost
            if (FindObjectOfType<RupeeWallet>().currentRupees >= this.cost)
            {
                FindObjectOfType<RupeeWallet>().DecreaseRupeeCount(cost);
                // PickUpEffect() will also destroy this object after effect
                PickUpEffect();
            }
        }
    }

    // PickUpEffect() uses the typeOfPickup to perform the corresponding effect
    public void PickUpEffect()
    {
        if (typeOfPickUp == TypeOfPickUp.Rupee)
        {
            PickUpRupee();
        }
        else if (typeOfPickUp == TypeOfPickUp.Heart)
        {
            PickUpHeart();
        }
        else if (typeOfPickUp == TypeOfPickUp.Heart_Container)
        {
            IncreaseHealth();
        }

        // Destroy after pickup
        Destroy(gameObject);
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
        // TODO: check for going over the maximum number of heart containers
        FindObjectOfType<PlayerHealth>().maxHealth += 1;
        FindObjectOfType<PlayerHealth>().AddHealth(1);
    }
}
