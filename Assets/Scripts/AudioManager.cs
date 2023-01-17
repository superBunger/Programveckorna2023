using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;
	public AudioMixerGroup mixerGroup;
	public Sound[] sounds;
	public PlayerMovement movement;

	void Update()
	{
        if(movement.rb.velocity.magnitude > 0)
        {
            FindObjectOfType<AudioManager>().ChangeVolume("PlayerFootsteps", 1.0f);
        }
        else
        {
            FindObjectOfType<AudioManager>().ChangeVolume("PlayerFootsteps", 0.0f);
        }
    }

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

	public void StopMenuMusic()
	{
		FindObjectOfType<AudioManager>().StartCoroutine(FadeOut("MenuTheme", 0.5f));
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
