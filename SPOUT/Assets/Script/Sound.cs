using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {
	//1 = not mute
	//-1 = mute
	//0 = not setting
	static public bool effectMute;
	//initiate = true when user start Application, then initiate = flase
	static public bool soundInitiate;
	private AudioSource effect;

	void Start(){
		effect = GameObject.Find ("effect_sound").GetComponent (typeof(AudioSource)) as AudioSource;
		//if not setting effectMute
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
