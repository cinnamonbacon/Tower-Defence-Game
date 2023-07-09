using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Turret : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;

    [Header("Attribute")]
    [SerializeField] private float targettingRange = 3f;

    private Transform target;
    private bool facingRight = false;

    private Vector3 spriteScale;
    private bool isFlipping = false;
    private float flipCnt;              // amount through which the flip has occurred
    private float flipFactor = 20f;    // rate at which the flip occurs

    void Start() {
        spriteScale = transform.localScale;
    }

    void Update() {
        if (target == null) {
            FindTarget();
            return;
        }

        FaceTarget();
        
        if (!CheckTargetInRange())
            target = null;
    }

    private void FindTarget() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targettingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0) {
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
                flipCnt = 1f;
            }
        }

        if (isFlipping) {
            flipCnt -= flipFactor * Time.deltaTime;

            Vector3 currentScale = spriteScale;
            currentScale.x *= Mathf.Clamp(flipCnt, -1f, 1f);
            transform.localScale = currentScale;

            if (flipCnt <= -1f) {
                isFlipping = false;
                flipCnt = 0;
                facingRight = !facingRight;
                spriteScale = transform.localScale;
            }
        }

        // TODO: prioritize furthest enemy
    }

    private void OnDrawGizmosDelected() {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targettingRange, enemyMask);
    }
}
