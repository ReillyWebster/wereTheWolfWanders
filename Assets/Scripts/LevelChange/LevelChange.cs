using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public string levelToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LevelChangeCo());
        }
    }

    private IEnumerator LevelChangeCo()
    {
        UIManager.instance.FadeOut();
        yield return new WaitForSeconds(1.5f);
        UIManager.instance.FadeIn();
        SceneManager.LoadScene(levelToLoad);
    }
}
