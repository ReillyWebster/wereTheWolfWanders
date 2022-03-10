using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateStartingPoint : MonoBehaviour
{
    private void Awake()
    {
        RespawnManager.instance.SetRespawnPoint(transform);
        RespawnManager.instance.RespawnPlayer(FindObjectOfType<PlayerControllerStateManager>());
    }
}
