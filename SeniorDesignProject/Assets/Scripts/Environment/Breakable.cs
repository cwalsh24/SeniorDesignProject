using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private GameObject Heart;
    [SerializeField] private GameObject BlueRupee;
    //[SerializeField] private GameObject droppedItem;
    [SerializeField] private enum ObjectType {pot, bush};
    [SerializeField] private ObjectType objectType;

    //Added for a sound effect
    [SerializeField] private AudioSource BreakSound;

    private enum DropType { Rupee, Heart, None};

    public void BreakObject() {
        gameObject.GetComponent<Animator>().SetTrigger("Break");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        StartCoroutine(DelayDestroyRoutine(gameObject));
        BreakSound.Play();
    }

    private void InstantiateRandom() {
        //DropType dropType = (DropType)Random.Range(0, System.Enum.GetValues(typeof(DropType)).Length+5);
        DropType dropType = (DropType)Random.Range(0, System.Enum.GetValues(typeof(DropType)).Length);

        GameObject dropPrefab;
        switch (dropType)
        {
            case DropType.Heart:
                dropPrefab = Instantiate(Heart, transform.position, Quaternion.identity) as GameObject;
                break;
            case DropType.Rupee:
                // Possible add a random 
                dropPrefab = Instantiate(BlueRupee, transform.position, Quaternion.identity) as GameObject;
                break;
            default:
                break;
        }
    }

    private IEnumerator DelayDestroyRoutine(GameObject other) {

        switch (objectType) {
            case ObjectType.pot:
                InstantiateRandom();
                //GameObject dropPrefab = Instantiate(Heart, transform.position, Quaternion.identity) as GameObject;
                break;
        }

        yield return new WaitForSeconds(2f);
        Destroy(other);
    }
}
