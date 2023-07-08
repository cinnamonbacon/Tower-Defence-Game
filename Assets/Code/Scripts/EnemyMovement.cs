using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;   // the transform of the point being travelled towards by the enemy
    private int pathIndex = 0;  // the index of the point being travelled towards by the enemy

    private void Start() {
        target = Manager.main.path[pathIndex];
    }

    private void Update() { 
        // check if the target point has been reached
        if (Vector2.Distance(target.position, transform.position) <= 0.01f) { 
            pathIndex++;

            // check if the final point has been reached
            if (pathIndex == Manager.main.path.Length) {
                Spawner.onEnemyDestroy.Invoke();   // report that the enemy was destroyed to the manager script
                Destroy(gameObject);
                return;
            } else {
                target = Manager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate() {
        Vector2 direction = (target.position - transform.position).normalized;  // unit vector direction
        rb.velocity = direction * moveSpeed;
    }

}
