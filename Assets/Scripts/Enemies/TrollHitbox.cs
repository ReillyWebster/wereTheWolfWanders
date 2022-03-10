using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealthController>().ApplyDamage(2);
        }
    }
}
