using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private string levelToLoad, mainMenuScene;
    [SerializeField] private GameObject mainMenu, playerUI, pauseMenuUI;
    [SerializeField] private Image fadeScreen;
    [SerializeField] public TMP_Text currentScore, highScore;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float fadeSpeed;
    private bool isPaused, fadeIn, fadeOut;

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

    private void Start()
    {
        AudioManager.instance.PlayMusic(MusicFile.TitleThemeLoop);
    }

    private void Update()
    {
        if (fadeOut)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 1f)
            {
                fadeOut = false;
            }
        }
        else if (fadeIn)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                fadeIn = false;
            }
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCo());
    }

    private IEnumerator StartGameCo()
    {
        FadeOut();
        yield return new WaitForSeconds(1.5f);
        mainMenu.SetActive(false);
        SceneManager.LoadScene(levelToLoad);
        AudioManager.instance.StopAllMusic();
        AudioManager.instance.PlaySFX(SFXFile.UISelect);
        AudioManager.instance.PlaySFX(SFXFile.StartGame);
        FadeIn();
        playerUI.SetActive(true);
    }

    public void MainMenu()
    {
        StartCoroutine(MainMenuCo());
    }

    private IEnumerator MainMenuCo()
    {
        FadeOut();
        yield return new WaitForSeconds(1.5f);
        Destroy(FindObjectOfType<PlayerControllerStateManager>().gameObject);
        AudioManager.instance.PlaySFX(SFXFile.UISelect);
        pauseMenuUI.gameObject.SetActive(false);
        playerUI.SetActive(false);
        mainMenu.SetActive(true);
        Time.timeScale = 1f;
        AudioManager.instance.PlayMusic(MusicFile.TitleThemeLoop);
        SceneManager.LoadScene(mainMenuScene);
        FadeIn();
    }

    public void QuitGame()
    {
        AudioManager.instance.PlaySFX(SFXFile.UISelect);
        Application.Quit();
    }

    public void TogglePause()
    {
        AudioManager.instance.PlaySFX(SFXFile.UISelect);

        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseMenuUI.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenuUI.gameObject.SetActive(false);
        }
    }
    public void SetupHealth(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void UpdateHealth(int currentHealth)
    {
        healthSlider.value = currentHealth;
    }

    public void FadeOut()
    {
        fadeOut = true;
        fadeIn = false;
    }

    public void FadeIn()
    {
        fadeIn = true;
        fadeOut = false;
    }

    public void SFXHover()
    {
        AudioManager.instance.PlaySFX(SFXFile.UIMovement);
    }
}
