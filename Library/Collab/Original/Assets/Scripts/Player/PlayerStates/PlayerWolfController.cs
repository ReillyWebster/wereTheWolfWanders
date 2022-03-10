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

        wolfTimeCounter = player.wolfTime;
    }

    public override void UpdateState(PlayerControllerStateManager player)
    {
        if (wolfTimeCounter > 0)
        {
            wolfTimeCounter -= Time.deltaTime;
            if (wolfTimeCounter <= 0)
            {
                AudioManager.instance.PlaySFX(SFXFile.WolfTranform);
                player.SwitchPlayerController(player.playerRedController);
            }

            //TODO update UI 
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            AudioManager.instance.PlaySFX(SFXFile.WolfTranform);
            player.SwitchPlayerController(player.playerRedController);
        }
    }
}
