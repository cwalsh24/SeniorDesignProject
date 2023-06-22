using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Keeping Bomb as placeholder if you wanted to implement something like item limited amounts 
    public enum TypeOfPickUp{Rupee, Bomb, Heart, Heart_Container};
    public TypeOfPickUp typeOfPickUp;

    private const string playerString = "Player";

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag(playerString)) {
            Destroy(gameObject);

            if (typeOfPickUp == TypeOfPickUp.Rupee) {
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
