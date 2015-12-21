using UnityEngine;
using System.Collections;

public class soundChooser : MonoBehaviour {

	public bool mute;
	public AudioClip [] sounds;
	public AudioSource speaker;
	public float minDelay;
	public float maxDelay;
	public float initialDelay;
	public float volume;
	public float minRange;
	public float maxRange;
	public bool debug;

	private void Start () {

		speaker.minDistance = minRange;
		speaker.maxDistance = maxRange;
		speaker.volume = volume / 10f;

		if (initialDelay == -1) {
			delaySound(minDelay, maxDelay);
			return;
		}
		delaySound(initialDelay, initialDelay);

	}

	private void delaySound(float min, float max) {

		if (debug) Debug.Log("min time = " + min.ToString() + " max = " + max.ToString());

		AudioClip a = getRandomSound();

		if (debug) Debug.Log("sound = " + a.name);

		if (min == 0 && max == 0) {
			StartCoroutine(playSound(a, 0));
			return;
		}
		float delay = Random.Range(min, max);
		StartCoroutine(playSound(a, delay));

	}

	private IEnumerator playSound(AudioClip a, float delay) {

		while (mute) yield return new WaitForSeconds(1.0f);

		yield return new WaitForSeconds(delay);
		speaker.clip = a;
		speaker.Play();
		while (speaker.isPlaying) {
			yield return new WaitForSeconds(0.1f);
		}

		delaySound(minDelay, maxDelay);

	}

	private AudioClip getRandomSound() {

		int pick = Random.Range(0, sounds.Length);
		return sounds[pick];

	}
}
