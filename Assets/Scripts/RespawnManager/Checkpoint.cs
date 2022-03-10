using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Sprite activeCheckpoint, inactiveCheckpoint;

    private void Start()
    {
        renderer.sprite = activeCheckpoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFX(SFXFile.SilverPickup);
            RespawnManager.instance.SetRespawnPoint(gameObject.transform);
            renderer.sprite = inactiveCheckpoint;
            collision.gameObject.GetComponent<PlayerControllerStateManager>().ResetPlayer();
        }
    }
}
