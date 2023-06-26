using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject explodePrefab;

    //throwing sound effect
    [SerializeField] private AudioSource Fuse;

    // Use in Bomb animation
    public void Explode() {
        GameObject newBomb = Instantiate(explodePrefab, transform.position, transform.rotation);
        Fuse.Play();
        newBomb.GetComponent<AttackDamage>().isBombExplosion = true;
        Destroy(gameObject);
    }
}
