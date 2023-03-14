using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] public AudioSource musicSource, effectsSource;

    void Awake()
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

    public void PlaySound(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void FadeMusic(AudioClip clip)
    {
        StartCoroutine(MusicFade(clip));
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public IEnumerator MusicFade(AudioClip clip)
    {
        float currentTime = 0;
        float start = musicSource.volume;
        while (currentTime < 1f)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(start, 0, currentTime / 1f);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);

        musicSource.Stop();
        musicSource.volume = 0.75f;
        musicSource.clip = clip;

        yield return new WaitForSeconds(0.25f);

        musicSource.Play();
    }
}
