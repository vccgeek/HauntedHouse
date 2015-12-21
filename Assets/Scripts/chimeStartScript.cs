using UnityEngine;
using System.Collections;

public class chimeStartScript : MonoBehaviour {

	public chimeScript chimes;

	private void OnTriggerEnter() {

		Debug.Log("Chime triggered");

		chimes.playNotes();

	}

}
