using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;
    private Vector3 respawnPoint;

    private void Awake()
    {
        instance = this;
    }
    
    public void SetRespawnPoint(Transform newRespawn)
    {
        respawnPoint = newRespawn.position;
    }

    public void RespawnPlayer(PlayerControllerStateManager player)
    {
        player.gameObject.transform.position = respawnPoint;
        player.isDead = false;
        player.GetComponent<PlayerHealthController>().ResetHealth();
    }
}
