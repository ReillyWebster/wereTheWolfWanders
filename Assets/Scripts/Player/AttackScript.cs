using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemy = collision.gameObject.GetComponent<EnemyBaseController>();

            if (enemy) enemy.ApplyDamage();

            var troll = collision.gameObject.GetComponent<TrollController>();

            if (troll) troll.ApplyDamage();
        }
    }
}
