using UnityEngine;
using System.Collections;

public class chimeTrigger : Activator {

	public chimeScript windChime;
	public AudioClip note;
	public int index;
	public AudioSource speaker;
	public float vibrateDuration;
	public float vibrateDistance;

	public Transform chime;

	private float initX;
	private float initY;
	private float initZ;

	public float doubleTapFix;

	private float lastUsed;

	private void Awake() {
		lastUsed = 0;
		initX = chime.position.x;
		initY = chime.position.y;
		initZ = chime.position.z;
	}

	public void playNote() {
		speaker.clip = note;
		speaker.Play();
		StartCoroutine(vibrate());
	}

	public override void use () {

		if (Time.time - lastUsed < doubleTapFix) return;

		speaker.clip = note;
		speaker.Play();
		windChime.notePlayed(index);
		StartCoroutine(vibrate());

	}

	private IEnumerator vibrate() {

		//float initX = chime.position.x;
		//float initY = chime.position.y;
		//float initZ = chime.position.z;

		bool toggle = false;

		float startTime = Time.time;
		while (Time.time - startTime < vibrateDuration) {

			if (toggle) {
				chime.position = new Vector3(initX + vibrateDistance, initY, initZ);
			} else {
				chime.position = new Vector3(initX - vibrateDistance, initY, initZ);
			}

			toggle = !toggle;

			yield return new WaitForSeconds(Time.deltaTime);

		}

		chime.position = new Vector3(initX, initY, initZ);
		//Debug.Log("vibrate done");

	}

}
