using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Turret : MonoBehaviour {

    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attribute")]
    [SerializeField] private float targettingRange = 3f;
    [SerializeField] private float pps = 2f;    // projectiles per second

    private Transform target;
    private float timeSinceFire = 0f;

    private bool facingRight = false;
    private Vector3 spriteScale;
    private bool isFlipping = false;
    private float flipCnt = 1f;        // amount through which the flip has occurred
    private float flipFactor = 30f;    // rate at which the flip occurs

    void Start() {
        spriteScale = transform.localScale;
    }

    void Update() {
        if (target == null) {
            FindTarget();
            return;
        }

        FaceTarget();
        
        if (!CheckTargetInRange()) { 
            target = null;
        } else {
            timeSinceFire += Time.deltaTime;
            if (timeSinceFire >= 1f / pps) {
                Shoot();
                timeSinceFire = 0f;
            }
        }
    }

    private void Shoot() {
        GameObject projectileObj = Instantiate(projectilePrefab, firingPoint.position, Quaternion.identity);
        Projectile projectileScript = projectileObj.GetComponent<Projectile>();
        projectileScript.SetTarget(target);
    }

    private void FindTarget() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targettingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0) {
            // TODO: prioritize furthest enemy
            target = hits[0].transform;
        }
    }

    private bool CheckTargetInRange() {
        return Vector2.Distance(target.position, transform.position) <= targettingRange;
    }

    private void FaceTarget() {
        if (target != null && !isFlipping) {
            if ((transform.position.x - target.position.x > 0 && facingRight) ||
                (transform.position.x - target.position.x <= 0 && !facingRight)) {
                isFlipping = true;
            }
        }

        if (isFlipping) {
            flipCnt -= flipFactor * Time.deltaTime;

            Vector3 currentScale = spriteScale;
            currentScale.x *= Mathf.Clamp(flipCnt, -1f, 1f);
            transform.localScale = currentScale;

            if (flipCnt <= -1f) {
                isFlipping = false;
                flipCnt = 1f;
                facingRight = !facingRight;
                spriteScale = transform.localScale;
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targettingRange);
    }
}
