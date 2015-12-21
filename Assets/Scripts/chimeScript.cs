using UnityEngine;
using System.Collections;

public class chimeScript : MonoBehaviour {

	private int [] notes;
	public int noteCount;

	public exteriorDoor door;

	public AudioClip wrongNote;
	public AudioSource speaker;
	public AudioClip unlockSound;
	public AudioSource unlockSpeaker;

	private int currentNote = 0;
	private int stopNote = 0;

	private bool beaten;
	private bool playingGuide;

	public chimeTrigger [] chimes;

	private void Awake() {
		resetChime();
	}

	public void pickNotes() {

		notes = new int[noteCount];

		for (int L = 0; L < noteCount; L++) {
			notes[L] = Random.Range(0,7);
		}

	}

	public void playNotes() {

		if (beaten) return;

		StartCoroutine(playTo(stopNote+1));

	}

	public void resetChime() {

		if (beaten) return;

		for (int L = 0; L < chimes.Length; L++) {
			chimes[L].vibrateDuration = 0.8f;
			chimes[L].vibrateDistance = 0.02f;
		}

		stopNote = 0;
		currentNote = 0;
		pickNotes();

		//playNotes();

	}

	private IEnumerator playTo(int note) {

		//if (note >= notes.Length) yield break;

		if (playingGuide) yield break;
		playingGuide = true;

		//Debug.Log("Playing guide");

		yield return new WaitForSeconds(1.5f);

		for (int L = 0; L < note; L++) {
			//Debug.Log("Playing " + chimes[notes[L]].note.name);
			//play(chimes[notes[L]].note);
			chimes[notes[L]].playNote();
			yield return new WaitForSeconds(0.8f);
		}

		playingGuide = false;
		yield break;

	}

	public void notePlayed(int index) {

		if (beaten) return;

		if (notes[currentNote] == index) {
			//Debug.Log("correct note");
			currentNote++;
			//playNotes();
		} else {
			play(wrongNote);
			resetChime();
			playNotes();
		}

		if (currentNote - 1 == stopNote) {
			//Debug.Log("stop note reached");
			stopNote++;
			currentNote = 0;
			if (stopNote < noteCount)playNotes();
		}

		if (stopNote == noteCount) {
			door.unlock();
			unlockSpeaker.PlayOneShot(unlockSound);
			beaten = true;
		}
		Debug.Log("currentNote = " + currentNote.ToString() + "; stopNote = " + stopNote.ToString());

	}

	private void play(AudioClip a) {

		//Debug.Log("Playing note " + a.name);
		speaker.clip = a;
		speaker.Play();

	}

}
