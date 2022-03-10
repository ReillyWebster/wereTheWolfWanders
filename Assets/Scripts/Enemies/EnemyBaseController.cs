using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class EnemyBaseController : MonoBehaviour
{
    [SerializeField] protected float maxHealth, moveSpeed, jumpForce, waitAtPoint;
    [SerializeField] protected Rigidbody2D rigidbody;
    [SerializeField] protected Transform[] patrolPoints;
    [SerializeField] public SpriteRenderer renderer;
    [SerializeField] protected Animator anim;
    [SerializeField] public Transform headPoint;
    [SerializeField] private BoxCollider2D collider;
    protected int currentPoint;
    protected float currentHealth, waitCounter;

    protected void Patrolling()
    {
        if (Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x) > 0.2f)
        {
            if (transform.position.x < patrolPoints[currentPoint].position.x)
            {
                rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);
                renderer.flipX = false;
            }
            else
            {
                rigidbody.velocity = new Vector2(-moveSpeed, rigidbody.velocity.y);
                renderer.flipX = true;
            }
        }
        else
        {
            rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);

            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                waitCounter = waitAtPoint;

                currentPoint++;
                PlayAudio(SFXFile.MushroomEnemy1);
                if (currentPoint >= patrolPoints.Length)
                {
                    currentPoint = 0;
                }
            }
        }
    }

    public void ApplyDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            collider.enabled = false;
            PlayAudio(SFXFile.MushroomEnemy2);
            Destroy(gameObject);
        }
    }

    private void PlayAudio(SFXFile file)
    {
        if (GetComponentInChildren<VisibleOnScreen>().isVisible)
        {
            AudioManager.instance.PlaySFX(file);
        }
    }
}