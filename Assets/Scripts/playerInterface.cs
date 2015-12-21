using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.VR;

public class playerInterface : MonoBehaviour {

	public float activationRange = 10f;
	[Range(0,10)]
	public float axisSpeed;

	public Image [] uiPreviews;

	public LayerMask layers;

	public MouseLook control;

	public StereoRigController oculus;

	void Start () {

		InputTracking.Recenter();
		oculus.enabled = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		DontDestroyOnLoad(transform.gameObject);
	}

	void Update () {
	
		//Debug.Log("Camera rotation: " + Camera.main.transform.eulerAngles.ToString());
		Transform mainCamera = GameObject.FindWithTag("MainCamera").transform;
		Vector3 rot = mainCamera.eulerAngles;
		float x = Mathf.Sin(rot.y * (Mathf.PI/180));
		float y = -1 * Mathf.Sin(rot.x * (Mathf.PI/180));
		float z = Mathf.Cos(rot.y * (Mathf.PI/180));

		for (int L = 0; L < uiPreviews.Length; L++) uiPreviews[L].enabled = false;

		if (Input.GetKeyDown("m")) {
			if (Cursor.lockState == CursorLockMode.Locked) {
				Cursor.lockState = CursorLockMode.None;
			} else {
				Cursor.lockState = CursorLockMode.Locked;
			}
			Cursor.visible = !Cursor.visible;
			control.enabled = !control.enabled;
		}

		//oculus.interaxialSeparation += Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * axisSpeed;
		//oculus.interaxialSeparation = Mathf.Clamp(oculus.interaxialSeparation, 0, 2);

		if (Input.GetKeyDown ("g")) {

			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#endif

		}

		Ray rc = new Ray(mainCamera.position, new Vector3(x,y,z));
		Debug.DrawRay(rc.origin, rc.direction * activationRange);

		RaycastHit rch;// = new RaycastHit();

		Physics.Raycast(rc, out rch, activationRange, layers);

		if (rch.collider != null) {

			Debug.Log("Detected a mouseover on " + rch.collider.gameObject.name);

			Activator act = (Activator) rch.collider.gameObject.GetComponent<Activator>();

			if (act != null) {
				Debug.Log("Detected " + act.gameObject.name + " at " + rch.distance.ToString());
				uiPreviews[act.activatePreview].enabled = true;
				if (Input.GetMouseButtonDown(0)) act.use();
			}
		}

	}
}
