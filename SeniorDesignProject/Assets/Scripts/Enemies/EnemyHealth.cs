using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region Private Variables 

    [SerializeField] private int startingHealth = 3;
    [SerializeField] private int currentHealth;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Material matWhiteFlash;
    // deathVFX was the sparkly things that appeared after death
    //[SerializeField] private GameObject deathVFX;
    [SerializeField] private GameObject possibleDrop; // normally a heart that may drop on enemy death
    [SerializeField] private float setDefaultMatRestorefloat = .1f;
    private Material matDefault;
    private SpriteRenderer spriteRenderer;

    //added for sound effects
    [SerializeField] private AudioSource DamageSound;
    [SerializeField] private AudioClip DeathSound;

    #endregion

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        matDefault = spriteRenderer.material;
    }

    private void Start() {
        currentHealth = startingHealth;
    }

    private void Update() { 
        DetectDeath();
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        spriteRenderer.material = matWhiteFlash;
        StartCoroutine(SetDefaultMatRoutine(setDefaultMatRestorefloat));
        DamageSound.Play();
    }

    private void DetectDeath() {
        if (currentHealth <= 0) {
            // Random drop chance
            if ( Random.Range(0, 3) == 0) { // 1/3 chance of dropping the possibleDrop
                Instantiate(possibleDrop, transform.position, transform.rotation);
            }
            AudioSource.PlayClipAtPoint(DeathSound, transform.position, 1f);
            // destroy the dead enemy
            Destroy(gameObject);
        }
    }

    private IEnumerator SetDefaultMatRoutine(float whiteFlashTime) {
        yield return new WaitForSeconds(whiteFlashTime);
        spriteRenderer.material = matDefault;
    }

}
