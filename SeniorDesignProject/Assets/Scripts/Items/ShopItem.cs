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
        PickUpScript = new PickUp();
        PickUpScript.typeOfPickUp = ShopItemType;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PickUpScript.PickUpEffect();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
