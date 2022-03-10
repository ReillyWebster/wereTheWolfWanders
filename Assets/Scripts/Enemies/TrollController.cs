using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollController : MonoBehaviour
{
    [SerializeField] protected float maxHealth, moveSpeed, waitAtPoint;
    [SerializeField] protected Rigidbody2D rigidbody;
    [SerializeField] protected Transform[] patrolPoints;
    [SerializeField] public SpriteRenderer renderer;
    [SerializeField] protected Animator anim;
    [SerializeField] public Transform headPoint;
    [SerializeField] private BoxCollider2D collider;
    protected int currentPoint;
    protected float currentHealth, waitCounter;
    [SerializeField] private float attackTimer, attackRange;
    private float attackCounter;
    private Transform target;
    private Material defaultMat;
    private Color defaultColor;

    private void Start()
    {
        currentHealth = maxHealth;
        waitCounter = waitAtPoint;

        foreach (var point in patrolPoints)
        {
            point.parent = null;
        }

        target = FindObjectOfType<PlayerControllerStateManager>().gameObject.transform;
        defaultMat = renderer.material;
        defaultColor = defaultMat.color;
    }

    private void Update()
    {
        if(Mathf.Abs(transform.position.x - target.position.x) < attackRange)
        {
            rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);

            if(transform.position.x < target.position.x)
            {
                renderer.flipX = true;
            }
            else
            {
                renderer.flipX = false;
            }
            
            if(attackCounter <= 0)
            {
                anim.SetTrigger("attack");
                PlayAudio(SFXFile.TrollEnemy1);
                attackCounter = attackTimer;
            }
            
            if(attackCounter > 0)
            {
                attackCounter -= Time.deltaTime;
            }
        }
        else
        {
            if (Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x) > 0.2f)
            {
                if (transform.position.x < patrolPoints[currentPoint].position.x)
                {
                    rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);
                    renderer.flipX = true;
                }
                else
                {
                    rigidbody.velocity = new Vector2(-moveSpeed, rigidbody.velocity.y);
                    renderer.flipX = false;
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
                    if (currentPoint >= patrolPoints.Length)
                    {
                        currentPoint = 0;
                    }
                }
            }
        }
        anim.SetFloat("moveSpeed", Mathf.Abs(rigidbody.velocity.x));
    }

    public void ApplyDamage()
    {
        currentHealth--;
        if(currentHealth <= 0)
        {
            collider.enabled = false;
            currentHealth = 0;
            StartCoroutine(Death());
        }
        else
        {
            StartCoroutine(Flicker());
        }
    }

    private IEnumerator Flicker()
    {
        defaultMat = renderer.material;
        defaultColor = defaultMat.color;

        defaultMat.color = new Color(255, 0, 0, 1f);
        renderer.material = defaultMat;

        yield return new WaitForSeconds(0.5f);

        defaultMat.color = defaultColor;
        renderer.material = defaultMat;
    }

    private IEnumerator Death()
    {
        PlayAudio(SFXFile.TrollEnemy2);
        anim.SetTrigger("death");
        rigidbody.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void PlayAudio(SFXFile file)
    {
        if (GetComponentInChildren<VisibleOnScreen>().isVisible)
        {
            AudioManager.instance.PlaySFX(file);
        }
    }
}
