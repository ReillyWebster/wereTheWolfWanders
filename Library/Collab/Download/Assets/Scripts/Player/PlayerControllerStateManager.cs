using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerStateManager : MonoBehaviour
{
    PlayerBaseController currentPlayerController;

    public PlayerRedController playerRedController = new PlayerRedController();
    public PlayerWolfController playerWolfController = new PlayerWolfController();

    [SerializeField] public Rigidbody2D rigidbody;
    [SerializeField] public Transform groundPoint;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public float moveSpeed, jumpForce;
    [SerializeField] private Sprite redSprite, wolfSprite;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] public float wolfTime;
    [SerializeField] private Moon moon;
    public bool isWolf;
    private bool isGrounded;
    private Vector2 moveDirection;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        currentPlayerController = playerRedController;
        currentPlayerController.EnterState(this);

        UpdateSprite();
    }

    private void Update()
    {
        currentPlayerController.UpdateState(this);

        CheckIfGrounded();
        Movement();
    }

    private void Movement()
    {
        if (moveDirection.x != 0)
        {
            rigidbody.velocity = new Vector2(moveDirection.x * moveSpeed, rigidbody.velocity.y);

            if (moveDirection.x > 0)
            {
                renderer.flipX = false;
            }
            else if(moveDirection.x < 0)
            {
                renderer.flipX = true;
            }
        }
        else
        {
            rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);
        }
    }

    public void SwitchPlayerController(PlayerBaseController playerBaseController)
    {
        if (isWolf == false && !GameObject.Find("Moon").GetComponent<Moon>().isVisible) return;
        currentPlayerController = playerBaseController;
        currentPlayerController.EnterState(this);

        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (isWolf)
        {
            renderer.sprite = wolfSprite;
        }
        else
        {
            renderer.sprite = redSprite;
        }
    }

    public void MovementInput(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }


    public void JumpInput(InputAction.CallbackContext context)
    {
        if(context.started && isGrounded)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        }
    }

    private void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, groundLayer);
    }
}
