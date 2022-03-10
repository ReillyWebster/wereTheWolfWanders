using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelaySceneLoad : MonoBehaviour
{
    [SerializeField] private string levelToLoad;
    [SerializeField] private float loadDelay;
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        UIManager.instance.FadeOut();
        yield return new WaitForSeconds(loadDelay);

        if(levelToLoad == "Credits Scene")
        {
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            UIManager.instance.MainMenu();
        }
        UIManager.instance.FadeIn();
    }
}
