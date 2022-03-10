using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stompbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var player = GetComponentInParent<PlayerControllerStateManager>();
            var enemy = collision.gameObject.GetComponent<EnemyBaseController>();

            if (enemy)
            {
                if(enemy.headPoint.position.y < gameObject.transform.position.y)
                {
                    player.redCollider.enabled = false;
                    player.wolfCollider.enabled = false;
                    enemy.ApplyDamage();
                    player.Knockback();
                }
            }
            var troll = collision.gameObject.GetComponent<TrollController>();

            if (troll)
            {
                if (troll.headPoint.position.y < gameObject.transform.position.y)
                {
                    player.redCollider.enabled = false;
                    player.wolfCollider.enabled = false;
                    troll.ApplyDamage();
                    player.Knockback();
                }
            }
        }
    }
}
