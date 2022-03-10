using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupType type;
    [SerializeField] private Animator anim;
    private int currentPoints;

    private void Start()
    {
        switch (type)
        {
            case PickupType.Red:
                currentPoints = (int)type;
                break;
            case PickupType.Yellow:
                currentPoints = (int)type;
                anim.SetTrigger("yellow");
                break;
            case PickupType.Blue:
                currentPoints = (int)type;
                anim.SetTrigger("blue");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFX(SFXFile.MoonPickup);
            ScoreManager.instance.UpdateScore(currentPoints);
            Destroy(gameObject);
        }
    }
}

[Serializable]
public enum PickupType
{
    Red = 10, 
    Yellow = 50,
    Blue = 100
}
