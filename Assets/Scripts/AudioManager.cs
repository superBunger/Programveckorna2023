using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;
	public AudioMixerGroup mixerGroup;
	public Sound[] sounds;

	//Grunden var tagen fr�n en Brackeys Tutorial men alla funktioner och coroutines �r gjorda av Erik

	void Awake()
	{
        foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = s.mixerGroup;
		}
	}

	void Start()
	{
		if(SceneManager.GetActiveScene().buildIndex == 0)
        {
		Play("MenuTheme");
        }
		else if (SceneManager.GetActiveScene().buildIndex >= 1 && SceneManager.GetActiveScene().buildIndex < 9)
        {
            FindObjectOfType<AudioManager>().Play("Ambience");
            FindObjectOfType<AudioManager>().Play("AmbienceDetected");
            FindObjectOfType<AudioManager>().Play("PlayerFootsteps");
        }
        else if(SceneManager.GetActiveScene().buildIndex == 9)
        {
            FindObjectOfType<AudioManager>().Play("PlayerFootsteps");
        }
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

    public void Stop(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            return;
        }

        s.source.Stop();
    }

    public void ChangeVolume(string sound, float volume)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		s.source.volume = volume;
	}

    public IEnumerator FadeOut(string sound, float FadeTime)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		float startVolume = s.source.volume;
        while (s.source.volume > 0)
        {
            s.source.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        startVolume = s.source.volume;
	}

	public IEnumerator FadeIn(string sound, float FadeTime)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		float startVolume = 0.2f;
		s.source.volume = 0;
        while (s.source.volume < 1.0f)
        {
            s.source.volume += startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        startVolume = s.source.volume;
	}

	public IEnumerator StopMusicCoroutine()
	{
		if(SceneManager.GetActiveScene().buildIndex == 0)
        {
			StartCoroutine(FadeOut("MenuTheme", 1f));
            yield return new WaitForSeconds(1.1f);
			Stop("MenuTheme");

        }
        if(SceneManager.GetActiveScene().buildIndex >= 1)
        {
			StartCoroutine(FadeOut("Ambience", 1f));
			StartCoroutine(FadeOut("AmbienceDetected", 1f));
			Stop("PlayerFootsteps");
			Stop("JuggernautFootsteps");
            yield return new WaitForSeconds(0.9f);
			Stop("Ambience");
			Stop("AmbienceDetected");
        }
	}

	public void StopMusic()
	{
		StartCoroutine(StopMusicCoroutine());
	}

    public void ChangeAmbienceDetected()
	{
		StartCoroutine(FadeOut("Ambience", 1f));
		StartCoroutine(FadeIn("AmbienceDetected", 0.2f));
	}

	public void ChangeAmbienceNormal()
	{
		StartCoroutine(FadeOut("AmbienceDetected", 1f));
		StartCoroutine(FadeIn("Ambience", 0.2f));
	}
}
