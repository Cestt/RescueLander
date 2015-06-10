using UnityEngine;
using System.Collections;

public class Sound_Manager : MonoBehaviour {

	/*public AudioClip alarm;
	public AudioClip coin;
	public AudioClip explosion;
	public AudioClip pickUp;*/

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

		//source = GetComponent<AudioSource>();
	}
	
	public void PlaySound(string type){
		if (!dataManger.manager.Sounds)
			return;
		/*switch (type){
		case "Coin":
			source.PlayOneShot(coin);
			break;
			
		}*/
	}
}
