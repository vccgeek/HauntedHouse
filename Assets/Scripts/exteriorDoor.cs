using UnityEngine;
using System.Collections;

public class exteriorDoor : Activator {
	
	public Transform leftDoor;
	public Transform rightDoor;
	
	public float leftClosedAngle;
	public float rightClosedAngle;
	
	public float leftOpenAngle;
	public float rightOpenAngle;
	
	public float openSpeed;

	//private bool open = false;

	private bool locked = true;

	public AudioClip lockedSound;
	public AudioClip openSound;
	public AudioSource speaker;

	private void Awake() {
		locked = true;
	}

	public void unlock() {
		locked = false;
	}

	public override void use() {

		if (locked) {
			play(lockedSound);
			return;
		}

		Debug.Log("Opening");

		StartCoroutine(openDoors());

	}

	private void play(AudioClip a) {

		speaker.clip = a;
		speaker.Play();

	}

	public IEnumerator openDoors() {

		play(openSound);

		while (Mathf.Abs(leftDoor.localEulerAngles.y - leftOpenAngle) > 1) {

			float y = Mathf.MoveTowardsAngle(leftDoor.localEulerAngles.y, leftOpenAngle, openSpeed * Time.deltaTime);
			leftDoor.localEulerAngles = new Vector3(0,y,0);
		
			y = Mathf.MoveTowardsAngle(rightDoor.localEulerAngles.y,rightOpenAngle, openSpeed * Time.deltaTime);
			rightDoor.localEulerAngles = new Vector3(0,y,0);

			yield return new WaitForSeconds(0);

		}

		//open = true;
		
		Application.LoadLevel("level2");

		yield return new WaitForSeconds(2f);

		leftDoor.localEulerAngles = new Vector3(0,leftClosedAngle,0);
		rightDoor.localEulerAngles = new Vector3(0,rightClosedAngle, 0);

	}

}
