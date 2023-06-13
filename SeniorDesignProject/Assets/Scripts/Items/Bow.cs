using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private float throwDistance = 6f;
    [SerializeField] private float throwSpeed = 25f;
    [SerializeField] private int arrowDamage = 2;
    [SerializeField] private float thrust = 40f;
    [SerializeField] private bool moving = true; 
    [SerializeField] private float maxTravelDistance = 8f;
    Rigidbody2D m_Rigidbody;
    private PlayerController player;
    private Vector2 locationToThrow;

    #endregion

    #region Unity Methods

    private void Awake() {
        player = FindObjectOfType<PlayerController>();
    }

    private void Start() {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        FindPositionToThrow();
    }

    private void Update()
    {
        //transform.Rotate(new Vector3(0f, 0f, spinSpeed) * Time.deltaTime);

        //DetectDestination();
        MoveBoomerang();
    }

    #endregion

    #region Private Methods

    // Finds a Vector2 position away from the player's current facing direction + throwDistance of the Boomerang
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
    

    // Once the Boomerang gets closes to the target locationToThrow position, the boomerang will turn around and head back towards the hero
    //private void DetectDestination() {
    //    if (goForward && Vector2.Distance(locationToThrow, transform.position) < .5f) {
    //        goForward = false;
    //    }
    //}

    private void MoveBoomerang() {
        if (moving) {
            transform.position = Vector2.MoveTowards(transform.position, locationToThrow, throwSpeed * Time.deltaTime);
        }
        
        if (Vector2.Distance(player.transform.position, transform.position) > maxTravelDistance) {
            moving = false;
            player.itemInUse = false;
            //Freeze all positions
            m_Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            Destroy(this.gameObject, 4);
        }
    }

    // If the boomerang hits a collider before getting to it's targeted direction 
    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();

        if (enemy && moving)
        {
            enemy.TakeDamage(arrowDamage);
            other.gameObject.GetComponent<KnockBack>().getKnockedBack(transform, thrust);
            Destroy(this.gameObject);
            player.itemInUse = false;
        }
    }
    #endregion
}
