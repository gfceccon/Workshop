﻿using UnityEngine;
using System.Collections;

namespace CompletedWorkshop
{
	public class SoundManager : MonoBehaviour
	{
		public static SoundManager instance = null;

		public AudioSource efxSource;
		public AudioSource musicSource;

		public float lowPitchRange = .95f;
		public float highPitchRange = 1.05f;


		void Awake ()
		{
            if (SoundManager.instance == null)
				SoundManager.instance = this;
            else if (SoundManager.instance != this)
				Destroy (gameObject);
			
			DontDestroyOnLoad (gameObject);
		}


		public void PlaySingle(AudioClip clip)
		{
			efxSource.clip = clip;

			efxSource.Play ();
		}


		public void RandomizeSfx (params AudioClip[] clips)
		{
			int randomIndex = Random.Range(0, clips.Length);

			float randomPitch = Random.Range(lowPitchRange, highPitchRange);

			efxSource.pitch = randomPitch;

			efxSource.clip = clips[randomIndex];

			efxSource.Play();
		}
	}
}