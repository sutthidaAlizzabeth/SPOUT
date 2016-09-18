using UnityEngine;
using System.Collections;

public class SoundButton : MonoBehaviour {
	private AudioSource sound;

	public void playEffect(){
		sound = new AudioSource ();
		sound.clip = Resources.Load ("rec", typeof(AudioClip)) as AudioClip;
		sound.Play ();
	}
}
