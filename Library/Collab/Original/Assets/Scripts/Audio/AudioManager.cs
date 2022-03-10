using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private List<Music> musicAudio;
    [SerializeField] private List<SFX> sfxAudio;
    [SerializeField] private AudioMixerSnapshot redSnapshot, wolfSnapshot;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator TogglePlayerMusic(bool isWolf)
    {
        MusicFile currentGirlSong = MusicFile.Level1Girl, currentWolfSong = MusicFile.Level1Wolf;

        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            currentGirlSong = MusicFile.Level1Girl;
            currentWolfSong = MusicFile.Level1Wolf;
        }
        else if (SceneManager.GetActiveScene().name == "Level 2")
        {
            currentGirlSong = MusicFile.Level2Girl;
            currentWolfSong = MusicFile.Level2Wolf;
        }

        var redMusic = musicAudio.Where(m => m.musicFile == currentGirlSong).SingleOrDefault().audioSource;
        var wolfMusic = musicAudio.Where(m => m.musicFile == currentWolfSong).SingleOrDefault().audioSource;

        redMusic.Pause();
        wolfMusic.Pause();

        yield return new WaitForSeconds(0.5f);

        if (isWolf)
        {
            redMusic.volume = 0;
            wolfMusic.volume = 1;
        }
        else
        {
            redMusic.volume = 1;
            wolfMusic.volume = 0;
        }

        redMusic.UnPause();
        wolfMusic.UnPause();
    }

    internal void StopAllMusic()
    {
        foreach (var audio in musicAudio)
        {
            audio.audioSource.Stop();
        }
    }

    public void PlayMusic(MusicFile file)
    {
        StopAllMusic();

        var music = musicAudio.Where(m => m.musicFile == file).SingleOrDefault().audioSource;

        music.Play();
    }

    public void PlaySFX(SFXFile file)
    {
        var sfx = sfxAudio.Where(s => s.sfxFile == file).SingleOrDefault().audioSource;

        sfx.pitch = UnityEngine.Random.Range(0.75f, 1.25f);
        sfx.Play();
    }
}

[Serializable]
public class Music
{
    public AudioSource audioSource;
    public MusicFile musicFile;
}

[Serializable]
public class SFX 
{
    public AudioSource audioSource;
    public SFXFile sfxFile;
}

public enum SFXFile
{
    Attack,
    Damage,
    Death,
    GirlTransform,
    GirlJump,
    WolfJump,
    MoonPickup,
    MushroomEnemy1,
    MushroomEnemy2,
    SilverPickup,
    SkeletonEnemy1,
    SkeletonEnemy2,
    StartGame,
    TreeEnemy1,
    TreeEnemy2,
    TrollEnemy1,
    TrollEnemy2,
    UIMovement,
    UISelect,
    WolfTranform
}

public enum MusicFile
{
    TitleTheme,
    TitleThemeLoop,
    Level1Girl,
    Level1Wolf,
    Level2Girl,
    Level2Wolf
}
