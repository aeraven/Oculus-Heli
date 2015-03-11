using UnityEngine;
using System.Collections;

public class PlayerNetworkUpdate : Photon.MonoBehaviour {
	Vector3 realPosition;
	Quaternion realRotation, oldRotation, newRotation;
	float deltaTime, previousTimeStamp;
	Helicopter heli;
	public Quaternion rotoration;
	//public GameObject[] wheels;

	void Awake() {
		heli = GetComponent<Helicopter> ();
	}

	void Start () {
		realPosition = transform.position;
		realRotation = transform.rotation;
		oldRotation = transform.rotation;
		newRotation = transform.rotation;
		if (!photonView.isMine) {
			(GetComponent ("Helicopter") as MonoBehaviour).enabled = false;
			Camera[] cameras = GetComponentsInChildren<Camera> ();
			foreach (Camera camera in cameras) {
					camera.gameObject.SetActive (false);
			}
		}
	}

	public static bool IsValid(Quaternion quaternion)
	{
		bool isNaN = float.IsNaN(quaternion.x + quaternion.y + quaternion.z + quaternion.w);
		
		bool isZero = quaternion.x == 0 && quaternion.y == 0 && quaternion.z == 0 && quaternion.w == 0;
		
		return !(isNaN || isZero);
	}
	
	void Update () {
		if (!photonView.isMine) {
			transform.position = (Vector3.Lerp (transform.position, realPosition, Time.deltaTime));		
			if (realRotation != transform.rotation) {
					newRotation = Quaternion.Lerp (oldRotation, realRotation, Mathf.Min (1, Mathf.Max (0, (Time.time - previousTimeStamp) / deltaTime)));
			}
			if(IsValid (newRotation))
			transform.rotation = newRotation;
			heli.rotor.transform.Rotate(Vector3.forward * 500 * Time.deltaTime);
		}
		//transform.position = realPosition;
	}
	
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if (stream.isWriting && photonView.isMine) {
			stream.SendNext ((Vector3) transform.position);
			stream.SendNext ((Quaternion) transform.rotation);
			stream.SendNext((Quaternion)heli.rotor.transform.rotation);
		} else if(stream.Count > 0) {
			float currentTime = Time.time;
			deltaTime = currentTime - previousTimeStamp;
			previousTimeStamp = currentTime;

			oldRotation = transform.rotation;
			realPosition = ((Vector3) stream.ReceiveNext());
			realRotation = ((Quaternion) stream.ReceiveNext());
			rotoration = ((Quaternion) stream.ReceiveNext());
		}
	}
}