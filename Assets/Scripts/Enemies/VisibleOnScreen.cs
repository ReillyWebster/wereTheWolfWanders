using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleOnScreen : MonoBehaviour
{
    public bool isVisible;
    private void OnBecameVisible()
    {
        isVisible = true;
    }

    private void OnBecameInvisible()
    {
        isVisible = false;
    }
}
