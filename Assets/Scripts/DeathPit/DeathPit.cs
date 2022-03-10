using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(RespawnCo(collision.gameObject));
        }
    }

    private IEnumerator RespawnCo(GameObject player)
    {
        AudioManager.instance.PlaySFX(SFXFile.Death);
        UIManager.instance.FadeOut();
        yield return new WaitForSeconds(1.5f);
        
        RespawnManager.instance.RespawnPlayer(player.GetComponent<PlayerControllerStateManager>());
        yield return new WaitForSeconds(.5f);
        UIManager.instance.FadeIn();
    }
}
