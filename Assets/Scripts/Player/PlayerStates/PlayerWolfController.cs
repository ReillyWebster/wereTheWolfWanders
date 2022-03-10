using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWolfController : PlayerBaseController
{
    private float wolfTimeCounter;
    public override void EnterState(PlayerControllerStateManager player)
    {
        player.currentMoveSpeed = player.wolfMoveSpeed;
        player.currentJumpForce = player.wolfJumpForce;
        player.anim = player.wolfAnim;
        player.isWolf = true;
        player.redCollider.enabled = false;
        player.wolfCollider.enabled = true;
    }

    public override void UpdateState(PlayerControllerStateManager player)
    {
        if (player.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.anim.SetTrigger("attack");

                AudioManager.instance.PlaySFX(SFXFile.Attack);
            }
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            player.SwitchTranform();
        }
    }
}
