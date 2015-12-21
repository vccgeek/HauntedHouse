using UnityEngine;
using System.Collections;

public class randomVolume : MonoBehaviour {

	public float updateIntervalMin;
	public float updateIntervalMax;
	public float volumeMin;
	public float volumeMax;
	public float fadeTimeMin;
	public float fadeTimeMax;
	public AudioSource speaker;

	private void Start () {
	
		speaker.volume = Random.Range(volumeMin, volumeMax) / 10f;
		StartCoroutine(delayFade(Random.Range(updateIntervalMin, updateIntervalMax)));

	}

	private IEnumerator delayFade(float delay) {

		yield return new WaitForSeconds(delay);

		float newVolume = Random.Range(volumeMin, volumeMax);
		float fadeTime = Random.Range(fadeTimeMin, fadeTimeMax);

		StartCoroutine(fadeTo(newVolume, fadeTime));

	}

	private IEnumerator fadeTo(float volume, float time) {

		float startTime = Time.time;
		float startVolume = speaker.volume * 10;
		while (Time.time < startTime + time) {

			float progress = (Time.time - startTime) / time;
			speaker.volume = Mathf.Lerp(startVolume, volume, (progress * volume )) / 10f;
			yield return new WaitForSeconds(0f);

		}

		StartCoroutine(delayFade(Random.Range(updateIntervalMin, updateIntervalMax)));

	}

}
