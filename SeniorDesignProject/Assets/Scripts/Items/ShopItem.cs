using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    private PickUp PickUpScript;
    [SerializeField] private PickUp.TypeOfPickUp ShopItemType;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<PickUp>();
        gameObject.GetComponent<PickUp>().canPickup = false;
        gameObject.GetComponent<PickUp>().typeOfPickUp = ShopItemType;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(PickUp.playerString))
        {
            gameObject.GetComponent<PickUp>().canPickup = true;
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
