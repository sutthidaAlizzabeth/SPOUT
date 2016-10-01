using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

	static public bool effectMute; // default = false
	//initiate = true when user start Application, then initiate = flase
	static public bool soundInitiate; // default = false
	private AudioSource effect;

	void Start(){
		effect = GameObject.Find ("effect_sound").GetComponent (typeof(AudioSource)) as AudioSource;
		//this condition (if condition) use only first time when user start application
		//if don't setting effectMute (effectMute = false) and soundInitiate = false
		if (!effectMute && !soundInitiate) {
			effect.mute = false;
			soundInitiate = true;
		} else {
			effect.mute = effectMute;
		}
	}

	public void setEffectMute(bool mute){
		if (mute) {
			effectMute = mute;
			effect.mute = mute;
		} else {
			effectMute = mute;
			effect.mute = mute;
		}
	}
}
