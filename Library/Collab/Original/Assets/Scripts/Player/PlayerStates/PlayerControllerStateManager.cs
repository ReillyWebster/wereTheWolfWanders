using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerStateManager : MonoBehaviour
{
    PlayerBaseController currentPlayerController;

    public PlayerRedController playerRedController = new PlayerRedController();
    public PlayerWolfController playerWolfController = new PlayerWolfController();

    [SerializeField] public Rigidbody2D rigidbody;
    [SerializeField] public Transform groundPoint;
    [SerializeField] public LayerMask groundLayer, enemyLayer;
    [SerializeField] public float redMoveSpeed, redJumpForce, wolfMoveSpeed, wolfJumpForce, bounceForce, knockBackTime;
    [SerializeField] private SpriteRenderer redRenderer, wolfRenderer;
    [SerializeField] public Animator redAnim, wolfAnim;
    [SerializeField] public float wolfTime;
    internal SpriteRenderer currentRenderer;
    private bool isGrounded, isDead;
    private Vector2 moveDirection;
    private float knockBackCounter;
    internal float currentMoveSpeed, currentJumpForce;
    internal bool isWolf;
    internal Animator anim;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        currentPlayerController = playerRedController;
        currentPlayerController.EnterState(this);

        UpdateSprite();
    }

    private void Update()
    {
        if (!isDead && Time.timeScale == 1f)
        {
            currentPlayerController.UpdateState(this);

            CheckIfGrounded();
            Movement();
        }
    }

    private void Movement()
    {
        if(knockBackCounter <= 0)
        {
            if (moveDirection.x != 0)
            {
                rigidbody.velocity = new Vector2(moveDirection.x * currentMoveSpeed, rigidbody.velocity.y);

                if (moveDirection.x > 0)
                {
                    currentRenderer.flipX = false;
                }
                else if (moveDirection.x < 0)
                {
                    currentRenderer.flipX = true;
                }
            }
            else
            {
                rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
        }

        anim.SetFloat("xVelocity", Mathf.Abs(rigidbody.velocity.x));
        anim.SetFloat("yVelocity", rigidbody.velocity.y);
    }

    public void SwitchPlayerController(PlayerBaseController playerBaseController)
    {
        currentPlayerController = playerBaseController;
        currentPlayerController.EnterState(this);

        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (isWolf)
        {
            wolfRenderer.gameObject.SetActive(true);
            redRenderer.gameObject.SetActive(false);
            currentRenderer = wolfRenderer;

        }
        else
        {
            redRenderer.gameObject.SetActive(true);
            wolfRenderer.gameObject.SetActive(false);
            currentRenderer = redRenderer;
        }
    }

    public void MovementInput(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UIManager.instance.TogglePause();
        }
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if(context.started && isGrounded && Time.timeScale == 1f)
        {
            if (isWolf)
            {
                AudioManager.instance.PlaySFX(SFXFile.WolfJump);
            }
            else
            {
                AudioManager.instance.PlaySFX(SFXFile.GirlJump);
            }
            
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, currentJumpForce);
        }
    }

    private void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.05f, groundLayer);

        anim.SetBool("isGrounded", isGrounded);
    }

    internal void SwitchTranform()
    {
        anim.SetBool("transform", true);
        StartCoroutine(CheckAnimationCompleted());
    }

    private IEnumerator CheckAnimationCompleted()
    {
        yield return new WaitForSeconds(1f);

        if (isWolf)
        {
            SwitchPlayerController(playerRedController);
        }
        else
        {
            SwitchPlayerController(playerWolfController);
        }
    }

    public void Knockback()
    {
        knockBackCounter = knockBackTime;
        var bounceDir = currentRenderer.flipX ? 3f : -3f;
        rigidbody.velocity = new Vector2(bounceDir, bounceForce);
    }

    public IEnumerator Death()
    {
        anim.SetTrigger("death");

        isDead = true;
        rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);
        rigidbody.bodyType = RigidbodyType2D.Kinematic;

        yield return new WaitForSeconds(1f);

        RespawnManager.instance.RespawnPlayer();
    }
}
