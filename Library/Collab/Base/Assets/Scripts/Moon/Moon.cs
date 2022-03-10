using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    void OnBecameVisible()
    {
        TransformationManager.instance.SetMoonVisibility(true);
    }

    void OnBecameInvisible()
    {
        TransformationManager.instance.SetMoonVisibility(false);
    }
}
