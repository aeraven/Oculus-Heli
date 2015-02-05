using UnityEngine;
using System.Collections;

public class OcculusSwitch : MonoBehaviour {
	[SerializeField]
	GameObject occulusCamera = null,
			normalCamera = null;

	[SerializeField]
	bool usesOcculus = true;

	// Use this for initialization
	void Start () {
		if (usesOcculus)
			normalCamera.SetActive(false);
		else
			occulusCamera.SetActive(false);
	}
}
