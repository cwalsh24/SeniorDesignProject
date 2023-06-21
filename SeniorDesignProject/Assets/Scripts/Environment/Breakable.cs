using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private GameObject droppedItem;
    [SerializeField] private enum ObjectType {pot, bush};
    [SerializeField] private ObjectType objectType;

    //Added for a sound effect
    [SerializeField] private AudioSource BreakSound;

    public void BreakObject() {
        gameObject.GetComponent<Animator>().SetTrigger("Break");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        StartCoroutine(DelayDestroyRoutine(gameObject));
        BreakSound.Play();
    }

    private string GetRandomDrop() {
        int random;
        random = Random.Range(0, 1);
        switch (random)
        {
            case 0:
                return "Heart";
            case 1:
                return "Blue_Rupee";
            default:
                return "";
        }
    }

    private IEnumerator DelayDestroyRoutine(GameObject other) {
        switch (objectType) {
            case ObjectType.pot:
                GameObject dropPrefab = Instantiate(droppedItem, transform.position, Quaternion.identity) as GameObject;
                dropPrefab.transform.parent = GameObject.Find(GetRandomDrop()).transform;
                break;
        }

        yield return new WaitForSeconds(2f);
        Destroy(other);
    }
}
