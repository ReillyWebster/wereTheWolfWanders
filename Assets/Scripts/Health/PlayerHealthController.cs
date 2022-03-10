using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float invincibilityTime, wolfDamageReduction;
    private Material defaultMat;
    private SpriteRenderer renderer;
    private int currentHealth;
    private float invincibilityCounter;
    private Color defaultColor;

    private void Start()
    {
        renderer = gameObject.GetComponent<PlayerControllerStateManager>().currentRenderer;
        currentHealth = maxHealth;
        UIManager.instance.SetupHealth(currentHealth);
        defaultMat = renderer.material;
        defaultColor = defaultMat.color;
    }

    internal void ResetHealth()
    {
        currentHealth = maxHealth;
        UIManager.instance.SetupHealth(currentHealth);
    }

    private void Update()
    {
        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            if(invincibilityCounter <= 0)
            {
                defaultMat.color = defaultColor;
                renderer.material = defaultMat;
            }
        }
    }

    public void ApplyDamage(int damageToApply)
    {
        if(invincibilityCounter <= 0)
        {
            var player = gameObject.GetComponent<PlayerControllerStateManager>();

            currentHealth -= player.isWolf ? damageToApply - Mathf.RoundToInt(damageToApply * wolfDamageReduction) : damageToApply;

            AudioManager.instance.PlaySFX(SFXFile.Damage);

            renderer = player.currentRenderer;
            defaultMat = renderer.material;
            defaultColor = defaultMat.color;

            defaultMat.color = new Color(255, 255, 255, 1f);
            renderer.material = defaultMat;

            player.Knockback();

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                
                StartCoroutine(player.Death());
            }

            UIManager.instance.UpdateHealth(currentHealth);

            invincibilityCounter = invincibilityTime;
        }
    }
}
