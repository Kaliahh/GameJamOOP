using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float turnSpeed;

    public GameObject Player;

    private Rigidbody rigidbody;
    private Damage damageScript;

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        damageScript = GetComponent<Damage>();
    }

    public void Update()
    {
        if (damageScript.currentCooldown == 0.0f)
        {
            TurnTowardsPlayer();
            MoveForwards();
        }
    }

    private void TurnTowardsPlayer()
    {
        Vector3 targetPos = Player.transform.position;
        Vector3 targetDirection = targetPos - transform.position;

        float step = turnSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void MoveForwards()
    {
        rigidbody.velocity = transform.forward * movementSpeed;
    }

}
