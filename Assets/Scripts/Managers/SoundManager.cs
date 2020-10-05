using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviourSingleton<SoundManager>
{
    public enum Sounds
    {
        ClickInteract,
        ClickNothing,
        Clouds,
        FliesDie,
        FliesLoop,
        FreezeOut,
        Grass,
        PlantFreeze,
        RainLoop,
        SeedPlant,
        WindLoop
    }

    public enum Songs
    {
        Day1,
        Night1,
        Day2,
        Night2,
        Day3,
        Night3
    }

    bool dayMusicOn = true;

    [SerializeField] float volumeIncreaseDuration = 1f;

    [SerializeField] AudioSource[] musicSources = null;

    public AudioClip[] sfx;

    [Header("Sound Options")]
    public bool soundOn = true;

    void Start()
    {
        PlayDayMusic();
        StartCoroutine(FadeIn(musicSources[(int)Songs.Day1]));
    }

    void Update()
    {
        if (dayMusicOn)
        {
            if (!musicSources[(int)Songs.Day1].isPlaying)
            {
                PlayNightMusic();
                dayMusicOn = false;
            }
        }
        else if (!musicSources[(int)Songs.Night1].isPlaying)
        {
            PlayDayMusic();
            dayMusicOn = true;
        }
    }

    void PlayDayMusic()
    {
        musicSources[(int)Songs.Day1].Play();
        musicSources[(int)Songs.Day2].Play();
        musicSources[(int)Songs.Day3].Play();
    }

    void PlayNightMusic()
    {
        musicSources[(int)Songs.Night1].Play();
        musicSources[(int)Songs.Night2].Play();
        musicSources[(int)Songs.Night3].Play();
    }

    public void AddNewInstrument()
    {
        foreach (AudioSource source in musicSources)
        {
            if (source.volume < 1f)
            {
                StartCoroutine(FadeIn(source));
                break;
            }
        }
    }

    public void PlaySound(Sounds sound)
	{
		if (soundOn)
			AudioSource.PlayClipAtPoint(sfx[(int)sound], Vector3.zero);
	}

    public void ToggleSound()
	{
		soundOn = !soundOn;
	}

    IEnumerator FadeIn(AudioSource source)
    {
        while (source.volume < 1f)
        {
            float addedValue = 1f / (volumeIncreaseDuration / Time.deltaTime);
            source.volume += addedValue;

            yield return null;
        }
    }
}
