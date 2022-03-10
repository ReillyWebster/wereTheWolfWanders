using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public bool isVisible;

    private void Start()
    {
        isVisible = false;
        TransformationManager.instance.SetMoonVisibility(false);
    }

    void OnBecameVisible()
    {
        isVisible = true;

        TransformationManager.instance.SetMoonVisibility(true);
    }

    void OnBecameInvisible()
    {
        isVisible = false;

        TransformationManager.instance.SetMoonVisibility(false);
    }

}
