using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private string levelToLoad;
    [SerializeField] private GameObject mainMenu, playerUI;
    [SerializeField] public TMP_Text currentScore, highScore;
    [SerializeField] private Slider healthSlider;

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

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
        AudioManager.instance.PlaySFX(SFXFile.StartGame);
        AudioManager.instance.PlayMusic(MusicFile.Level1Girl);
        mainMenu.SetActive(false);
        playerUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
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
}
