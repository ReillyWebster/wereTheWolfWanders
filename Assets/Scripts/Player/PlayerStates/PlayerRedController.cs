using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerRedController : PlayerBaseController
{
    public override void EnterState(PlayerControllerStateManager player)
    {
        player.currentMoveSpeed = player.redMoveSpeed;
        player.currentJumpForce = player.redJumpForce;
        player.anim = player.redAnim;
        player.isWolf = false;
        player.redCollider.enabled = true;
        player.wolfCollider.enabled = false;
    }

    public override void UpdateState(PlayerControllerStateManager player)
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) && TransformationManager.instance.CheckIfCanTransform())
        {
            player.SwitchTranform();
        }
    }

}
