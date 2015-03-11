using UnityEngine;
using System.Collections;
//Handles Broadcasting and Receiving information from server to client and from client to server
public class CommunicationManager : Photon.MonoBehaviour {
	
	Vector2 realPosition;
	Vector2 destination;
	
	Quaternion oldRotation;
	Quaternion realRotation;
	float previousTimeStamp;
	float deltaTime;
	
	Helicopter movement;
	void Start(){
		movement = gameObject.GetComponent<Helicopter> ();
		destination = transform.position;
		previousTimeStamp = Time.time;
		deltaTime = 0.02f;
		realRotation = transform.rotation;
		oldRotation = transform.rotation;
	}

	public static bool IsValid(Quaternion quaternion)
	{
		bool isNaN = float.IsNaN(quaternion.x + quaternion.y + quaternion.z + quaternion.w);
		
		bool isZero = quaternion.x == 0 && quaternion.y == 0 && quaternion.z == 0 && quaternion.w == 0;
		
		return !(isNaN || isZero);
	}
	
	void Update () {
		if (!photonView.isMine) {
			if ((destination - realPosition).magnitude < (destination - (Vector2) transform.position).magnitude) {
				transform.position = (Vector3) (Vector2.Lerp((Vector2) transform.position, realPosition, Time.deltaTime));
			} 
			if (realRotation != transform.rotation) {
				Quaternion newRotation = Quaternion.Lerp(oldRotation, realRotation, Mathf.Min (1, Mathf.Max(0, (Time.time-previousTimeStamp)/deltaTime)));
				if (IsValid(newRotation)) {
					transform.rotation = newRotation;
				}
			}
		}
	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		bool isMyObject = stream.isWriting;
		
		if (isMyObject) {
			if (movement != null){
				stream.SendNext (transform.rotation);
				stream.SendNext ((Vector3) transform.position);
				//stream.SendNext (movement.getDestination());
			}
		} else {	
			if (movement != null && stream.Count > 0) {
				float currentTime = Time.time;
				deltaTime = currentTime - previousTimeStamp;
				previousTimeStamp = currentTime;
				
				oldRotation = transform.rotation;
				realRotation = (Quaternion) stream.ReceiveNext ();
				realPosition = (Vector2) stream.ReceiveNext ();
				destination = (Vector2) stream.ReceiveNext ();
				//movement.setDestination(destination);
            }
		}
	}
}
