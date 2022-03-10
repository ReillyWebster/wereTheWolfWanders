using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRedController : PlayerBaseController
{
    public override void EnterState(PlayerControllerStateManager player)
    {
        player.currentMoveSpeed = player.redMoveSpeed;
        player.currentJumpForce = player.redJumpForce;
        player.anim = player.redAnim;
        player.isWolf = false;
    }

    public override void UpdateState(PlayerControllerStateManager player)
    {
        if (Input.GetKeyDown(KeyCode.E) && TransformationManager.instance.CheckIfCanTransform())
        {
            player.anim.SetBool("turnIntoWolf", true);

            //player.SwitchPlayerController(player.playerWolfController);
        }
    }
}
