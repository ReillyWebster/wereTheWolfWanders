using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.transform.position.y > gameObject.transform.position.y - (renderer.bounds.size.y / 2f))
            {
                collision.gameObject.GetComponent<PlayerHealthController>().ApplyDamage(2);
            }
        }
    }
}
