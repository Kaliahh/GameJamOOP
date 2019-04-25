using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Editor fields
    [SerializeField] private float movementSpeed;
    [SerializeField] private float turnSpeed;
    
    // Reference to the player to follow
    public GameObject Player;
    
    // Reference to own components
    private Rigidbody rigidbody;
    private Damage damageScript;

    public void Start()
    {
        // Get references to components
        rigidbody = GetComponent<Rigidbody>();
        damageScript = GetComponent<Damage>();
    }

    public void Update()
    {   
        // Check if it is not current cooling down
        if (damageScript.currentCooldown == 0.0f)
        {
            // Rotate self partly towards player
            TurnTowardsPlayer();
            
            // Move in new direction
            MoveForwards();
        }
    }

    private void TurnTowardsPlayer()
    {
        // Get the target position from the player
        Vector3 targetPos = Player.transform.position;
        // Calculate the direction towards the player from own perspective
        Vector3 targetDirection = targetPos - transform.position;
    
        // Calculate the radians to turn this frame
        float step = turnSpeed * Time.deltaTime;
        // Calculate the new direction by rotating step radians towards target direction
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
        
        // Update rotation
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void MoveForwards()
    {
        // Set the velocity to go forwards
        rigidbody.velocity = transform.forward * movementSpeed;
    }

}
