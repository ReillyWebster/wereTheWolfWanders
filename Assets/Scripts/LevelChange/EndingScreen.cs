using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreen : MonoBehaviour
{
    public string goodEndingScene;
    public string badEndingScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LoadEndingSceneCo(collision.gameObject));
        }
    }

    private IEnumerator LoadEndingSceneCo(GameObject player)
    {
        UIManager.instance.FadeOut();

        yield return new WaitForSeconds(1f);

        if (player.GetComponent<PlayerControllerStateManager>().isWolf)
        {
            SceneManager.LoadScene(badEndingScene);
        }
        else
        {
            SceneManager.LoadScene(goodEndingScene);
        }

        UIManager.instance.FadeIn();
    }
}
