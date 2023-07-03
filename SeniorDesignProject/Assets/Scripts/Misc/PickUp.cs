using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum TypeOfPickUp{Rupee, Heart, Heart_Container};
    public TypeOfPickUp typeOfPickUp;
    public bool preventPickup;
    public int cost = 0;

    //Adding sound effects
    public AudioClip pickupSound;
    public AudioClip failedPickupSound;
    public float pickupVolume = 1f; // default volume

    public const string playerString = "Player";
    private PlayerControls playerControls;



    #region Unity Methods
    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        playerControls.Spacebar.Use.performed += _ => PickUpItem();
    }
    #endregion



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerString) && cost <= 0)
        {
            // if the item has a cost, space has to be pressed for pickup
            if (cost > 0)
            {
                // TODO: For shop items, a description of the item should appear on the screen
                return;
            }
            else // no cost, pick up item
            {
                PickUpItem();
            }
        }
    }

    private void PickUpItem()
    {
        // check cost
        if (FindObjectOfType<RupeeWallet>().currentRupees >= this.cost)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, pickupVolume);

            FindObjectOfType<RupeeWallet>().DecreaseRupeeCount(cost);

            // PickUpEffect() will also destroy this object after effect
            PickUpEffect();
        }
        else
        {
            // if not enough rupees, play failedPickupSound
            AudioSource.PlayClipAtPoint(failedPickupSound, transform.position, .5f);
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
