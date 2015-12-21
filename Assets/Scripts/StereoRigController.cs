using UnityEngine;
using System.Collections;

public class StereoRigController : MonoBehaviour {

	public Transform 	leftEye;
	public Transform 	rightEye;

	[Range(0, 2)]
	public float 		interaxialSeparation;

	[Range(0, 2)]
	public float 		eyeRelief;

	private void Start() {

		leftEye.GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1f);
		rightEye.GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1f);

	}

	private void Update() {

		leftEye.transform.localPosition  = new Vector3(-1 * interaxialSeparation, 0, eyeRelief);
		rightEye.transform.localPosition = new Vector3(     interaxialSeparation, 0, eyeRelief);

	}

}
