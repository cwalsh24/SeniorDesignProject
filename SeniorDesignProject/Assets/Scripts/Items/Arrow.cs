using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private float throwDistance = 6f;
    [SerializeField] private float throwSpeed = 25f;
    [SerializeField] private int arrowDamage = 2;
    [SerializeField] private float thrust = 40f;
    [SerializeField] private bool moving = true; 
    [SerializeField] private float maxTravelDistance = 8f;
    Rigidbody2D m_Rigidbody;
    SpriteRenderer m_SpriteRenderer;

    private PlayerController player;
    private Vector2 locationToThrow;

    //fire sound effect
    [SerializeField] private AudioSource FireSound;

    #endregion

    #region Unity Methods

    private void Awake() {
        player = FindObjectOfType<PlayerController>();
        FireSound.Play();
    }

    private void Start() {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        FindPositionToThrow();
    }

    private void Update()
    {
        MoveArrow();
    }

    #endregion

    #region Private Methods

    // Finds a Vector2 position away from the player's current facing direction + throwDistance of the Arrow
    // also angles the arrow in the correct direction
    private void FindPositionToThrow() {
        Animator playerAnimator = player.GetComponent<Animator>();

        if (playerAnimator.GetFloat("lastMoveX") == 1) {
            locationToThrow = new Vector2(player.transform.position.x + throwDistance, player.transform.position.y);
            transform.Rotate(0f, 0f, 270f);
        }
        else if (playerAnimator.GetFloat("lastMoveX") == -1) {
            locationToThrow = new Vector2(player.transform.position.x - throwDistance, player.transform.position.y);
            transform.Rotate(0f, 0f, 90f);
        }
        else if (playerAnimator.GetFloat("lastMoveY") == 1) {
            locationToThrow = new Vector2(player.transform.position.x, player.transform.position.y + throwDistance);
            // no rotation needed
        }
        else if (playerAnimator.GetFloat("lastMoveY") == -1) {
            locationToThrow = new Vector2(player.transform.position.x, player.transform.position.y - throwDistance);
            transform.Rotate(0f, 0f, 180f);
        }

        moving = true;
    }

    private void MoveArrow() {
        if (moving) {
            transform.position = Vector2.MoveTowards(transform.position, locationToThrow, throwSpeed * Time.deltaTime);
        }
        
        if (Vector2.Distance(player.transform.position, transform.position) > maxTravelDistance) {
            moving = false;
            player.itemInUse = false;
            //Freeze all positions
            m_Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            // Start destroy "animation"
            StartCoroutine(WaitDestroy(3.5f, 1.5f));
        }
    }

    private IEnumerator WaitDestroy(float delay, float blinkTime)
    {
        // wait before blink
        yield return new WaitForSeconds(delay);

        // blinking
        for (int i = 0; i < blinkTime * 2; i++)
        {
            m_SpriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.25f);
            m_SpriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.25f);
        }

        // destroy
        Destroy(this.gameObject);
    }

    // If the boomerang hits a collider before getting to it's targeted direction 
    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();

        if (enemy && moving)
        {
            enemy.TakeDamage(arrowDamage);
            other.gameObject.GetComponent<KnockBack>().getKnockedBack(transform, thrust);
            player.itemInUse = false;
            Destroy(this.gameObject);
        }
        else
        {
            player.itemInUse = false;
            Destroy(this.gameObject);
        }
    }
    #endregion
}
