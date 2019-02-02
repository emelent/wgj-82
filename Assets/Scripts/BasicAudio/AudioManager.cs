using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicAudio {
	public class AudioManager : MonoBehaviour {
		
		public Sound[] sounds;

		public Sound currentSound { get; private set; }

		void Start ()
		{
			for (int i = 0; i < sounds.Length; i++)
			{
				GameObject go = new GameObject("Sound_" + i + "_" + sounds[i].name);
				go.transform.SetParent(this.transform);
				sounds[i].SetSource (go.AddComponent<AudioSource>());
			}
		}

		public void PlaySound (string _name)
		{
			foreach(Sound sound in sounds){
				if(sound.name == _name){
					currentSound = sound;
					sound.Play();
					return;
				}
			}
			// no sound with _name
			Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
		}

		public void Pause(){
			if(currentSound == null) return;

			if(currentSound.isPlaying){
				currentSound.Pause();
			} else {
				currentSound = null;
			}
		}

		public void Play(){
			if(currentSound == null) return;

			if(currentSound.isPaused){
				currentSound.Play();
			}
		}
	}
}