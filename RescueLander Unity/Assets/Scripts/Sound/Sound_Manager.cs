using UnityEngine;
using System.Collections;

public class Sound_Manager : MonoBehaviour {

	public AudioClip alarm;
	[Range(0, 1)] public float alarmVolume;
	public AudioClip coin;
	[Range(0, 1)] public float coinVolume;
	public AudioClip explosion;
	[Range(0, 1)] public float explosionVolume;
	public AudioClip pickUp;
	[Range(0, 1)] public float pickUpVolume;
	public AudioClip engineStart;
	[Range(0, 1)] public float engineStartVolume;
	public AudioClip win;
	[Range(0, 1)] public float winVolume;
	public AudioClip lose;
	[Range(0, 1)] public float loseVolume;




	private AudioSource source;

	void Awake () {

		if(dataManger.manager.Music == true){
			if (!dataManger.manager.GetComponent<AudioSource>().isPlaying)
				dataManger.manager.GetComponent<AudioSource>().Play();
			//audioSourceMusic.Play();
		}else{
			if (dataManger.manager.GetComponent<AudioSource>().isPlaying)
				dataManger.manager.GetComponent<AudioSource>().Pause();
		}

		source = GetComponent<AudioSource>();
	}
	
	public void PlaySound(string type){
		if (!dataManger.manager.Sounds)
			return;
		switch (type){
			case "Coin":
				source.volume = coinVolume;
				source.PlayOneShot(coin);
				break;
			case "Explosion":
				source.volume = explosionVolume;
				source.PlayOneShot(explosion);
				break;
			case "PickUp":
				source.volume = pickUpVolume;
				source.PlayOneShot(pickUp);
				break;
			case "Alarm":
				source.volume = alarmVolume;
				source.PlayOneShot(alarm);
				break;
			case "EngineStart":
				source.volume = engineStartVolume;
				source.PlayOneShot(engineStart);
				break;
			case "Win":
				source.volume = winVolume;
				source.PlayOneShot(win);
				break;
			case "Lose":
				source.volume = loseVolume;
				source.PlayOneShot(lose);
				break;
		}
	}
}
