using UnityEngine;
using System.Collections;

public class footstepZone : MonoBehaviour {

	public string label;
	public bool here;

	void Start () {
		
	}
	
	void OnTriggerEnter () {

		//Debug.Log("Entering " + label);

		here = true;

	}

	void OnTriggerExit() {

		//Debug.Log ("Exiting " + label);

		here = false;

	}

}
