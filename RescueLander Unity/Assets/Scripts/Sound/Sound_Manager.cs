using UnityEngine;
using System.Collections;

public class Sound_Manager : MonoBehaviour {

	private AudioClip mainMusic;
	private AudioSource audioSourceMusic;
	private AudioSource audioSourceEffects;

	void Awake () {

		mainMusic = Resources.Load("Sounds/Pamgaea",typeof(AudioClip)) as AudioClip;

		if(dataManger.manager.Music == true){
			audioSourceMusic = gameObject.AddComponent<AudioSource>();
			audioSourceMusic.loop = true;
			audioSourceMusic.clip = mainMusic;
			audioSourceMusic.Play();
		}
		if(dataManger.manager.Music == true){
			audioSourceEffects = gameObject.AddComponent<AudioSource>();

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
