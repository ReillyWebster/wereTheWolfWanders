using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationManager : MonoBehaviour
{
    public static TransformationManager instance;

    private bool playerCanTransform;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMoonVisibility(bool isMoonVisible)
    {
        playerCanTransform = isMoonVisible;
    }

    public bool CheckIfCanTransform()
    {
        return playerCanTransform;
    }
}
