using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

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
        var redMusic = musicAudio.Where(m => m.musicFile == MusicFile.Level1Girl).SingleOrDefault().audioSource;
        var wolfMusic = musicAudio.Where(m => m.musicFile == MusicFile.Level1Wolf).SingleOrDefault().audioSource;

        yield return new WaitForSeconds(0.5f);

        if (isWolf)
        {
            redMusic.volume = 0f;
            wolfMusic.volume = 1;
        }
        else
        {
            redMusic.volume = 1;
            wolfMusic.volume = 0f;
        }

        redMusic.Play();
        wolfMusic.Play();
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

        sfx.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
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
