using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    [SerializeField] public BoxCollider2D redCollider, wolfCollider, redStompCollider, wolfStompCollider, biteBox;
    internal SpriteRenderer currentRenderer;
    internal bool isGrounded, isDead;
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

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Level 1")
        {
            AudioManager.instance.PlayCharacterMusic(MusicFile.Level1Girl, MusicFile.Level1Wolf);
        }
        else if(SceneManager.GetActiveScene().name == "Level 2")
        {
            AudioManager.instance.PlayCharacterMusic(MusicFile.Level2Girl, MusicFile.Level2Wolf);
        }
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
                    biteBox.transform.localScale = new Vector3(1f, 1f, 1f);
                }
                else if (moveDirection.x < 0)
                {
                    currentRenderer.flipX = true;
                    biteBox.transform.localScale = new Vector3(-1f, 1f, 1f);
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

        StartCoroutine(AudioManager.instance.TogglePlayerMusic(isWolf));

        UpdateSprite();
        if (isWolf)
        {
            AudioManager.instance.PlaySFX(SFXFile.WolfTranform);
        }
        else
        {
            AudioManager.instance.PlaySFX(SFXFile.GirlTransform);
        }
    }

    private void UpdateSprite()
    {
        if (isWolf)
        {
            wolfRenderer.gameObject.SetActive(true);
            redRenderer.gameObject.SetActive(false);
            currentRenderer = wolfRenderer;
            wolfStompCollider.enabled = true;
            redStompCollider.enabled = false;
        }
        else
        {
            redRenderer.gameObject.SetActive(true);
            wolfRenderer.gameObject.SetActive(false);
            currentRenderer = redRenderer;
            redStompCollider.enabled = true;
            wolfStompCollider.enabled = false;
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
        if (TransformationManager.instance.CheckIfCanTransform())
        {
            anim.SetTrigger("transform");
            StartCoroutine(CheckAnimationCompleted());
        }
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
        CheckIfGrounded();
    }

    public void Knockback()
    {
        knockBackCounter = knockBackTime;
        var bounceDir = currentRenderer.flipX ? 3f : -3f;
        rigidbody.velocity = new Vector2(bounceDir, bounceForce);
        if (isWolf)
        {
            wolfCollider.enabled = true;
        }
        else
        {
            redCollider.enabled = true;
        }
    }

    public IEnumerator Death()
    {
        AudioManager.instance.PlaySFX(SFXFile.Death);
        anim.SetTrigger("death");
        rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);

        isDead = true;

        yield return new WaitForSeconds(2f);
        
        UIManager.instance.FadeOut();

        yield return new WaitForSeconds(1f);

        RespawnManager.instance.RespawnPlayer(this);

        ResetPlayer();

        yield return new WaitForSeconds(1f);

        UIManager.instance.FadeIn();

        yield return new WaitForSeconds(1f);

        isDead = false;
    }

    public void ResetPlayer()
    {
        SwitchPlayerController(playerRedController);
        anim.SetTrigger("respawned");
    }
}
