using UnityEngine;
using System.Collections;

public class footStepLoop : MonoBehaviour {

	public AudioClip [] footsteps;
	
	public string label;
	public bool mute;
	public float volume;
	public AudioSource speaker;
	public float stepInterval;
	public CharacterController playerCollider;

	//private int stepCount;

	private bool jumping;

	private float timer;

	void Start () {
	
		//stepCount = 0;

		if (footsteps.Length == 0) {
			Debug.LogWarning("No footsteps defined.");
		}

		speaker.volume = volume / 10;

	}

	void Update () {
	
		if (mute) return;
		timer += Time.deltaTime;
		
		if (!jumping && Input.GetButton("Jump")) {
			jumping = true;
		}

		// holy CRAP this is a pain
		bool landed;
		if ((playerCollider.collisionFlags & CollisionFlags.Below) > 0) {
			landed = true;
		} else {
			landed = false;
		}

		if (jumping && landed) {
			float currentVolume = speaker.volume;
			speaker.volume = currentVolume * 2;
			playFootstep();
			speaker.volume = currentVolume;
			jumping = false;
		}

		if (jumping) return;

		if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0) {
			return;
		}

		if (timer > stepInterval) {

			//Debug.Log("Footstep #" + stepCount++);

			playFootstep();
			timer = 0;

		}

	}

	private void playFootstep() {

		int pick = Random.Range(0, footsteps.Length);
		speaker.Stop();
		speaker.clip = footsteps[pick];
		speaker.Play();

	}

}
