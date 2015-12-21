using UnityEngine;
using System.Collections;

public class footstepManager : MonoBehaviour {

	public footStepLoop []  footsteps;
	public footstepZone []  footstepZones;
	public int []			zoneIndices;

	void Update () {
	
		int zone = 0;

		for (int L = 0; L < footsteps.Length; L++) {
			footsteps[L].mute = true;
			if (footstepZones[L].here) zone = L;
		}

		footsteps[zone].mute = false;

	}

}
