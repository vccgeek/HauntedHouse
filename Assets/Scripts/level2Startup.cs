using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class level2Startup : MonoBehaviour {

	public Transform playerStart;

	void Awake() {

		InputTracking.Recenter();
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		if (player == null) {
			Application.LoadLevel("Level1");
			return;
		}

		player.transform.position = playerStart.position;
		player.transform.eulerAngles = playerStart.eulerAngles;

	}

}
