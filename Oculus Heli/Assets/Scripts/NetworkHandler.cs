using UnityEngine;
using System.Collections;

//Tries to find a connection, or creates Lobby. Then joins 
public class NetworkHandler : Photon.MonoBehaviour {
	
	private string version = "OculusHeli";
	private string prefabName = "PlayerHeli"; //Deze prefab moet in de Resources folder zitten, wordt gebruikt voor de speler
	
	public Vector3[] playerSpawnPoints; 

	public int playerNumber = 0;
	
	private GameObject myPlayerObject;
	
	public bool debugLinesActive = true;
	
	public bool offlineMode;
	
	public void Start () {
		if (offlineMode) {
			PhotonNetwork.offlineMode = true;
			PhotonNetwork.JoinRandomRoom ();
		} else {
			Connect ();
		}
	}
	
	private void Connect() {
		if(debugLinesActive)
			Debug.Log ("Connect");
		PhotonNetwork.ConnectUsingSettings (version);
	}
	
	public void OnJoinedLobby() {
		if(debugLinesActive)
			Debug.Log ("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom ();
	}
	
	public void OnPhotonRandomJoinFailed() {
		if(debugLinesActive)
			Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom (null);
	}
	
	public void OnJoinedRoom() {
		if(debugLinesActive)
			Debug.Log ("OnJoinedRoom");
		SpawnMyPlayer ();
	}
	
	public void SpawnMyPlayer() {
		if(debugLinesActive)
			Debug.Log ("SpawnMyPlayer");
		
		Vector3 position = playerSpawnPoints[playerNumber];
		Quaternion rotation = Quaternion.Euler(0, 0, 0);
		myPlayerObject = PhotonNetwork.Instantiate (prefabName, position, rotation, 0);

		switch (playerNumber) //Player rotation goed zetten afhankelijk van spawnpoint
		{
			//Spawns beginnen vanaf 0 (player 1), momenteel maar twee spawnpoints maar zijn hieronder makkelijk toe te voegen
			case 0:
				//myPlayerObject.transform.Rotate (0, 270, 0);
				break;
			case 1:
				myPlayerObject.transform.Rotate (0, 180, 0);
				break;
			//Spawns voor speler 3 en 4 hieronder
			//case 2:
			//	break;
			//case 3:
			//	break;
		}
	}
	
	public GameObject getMyPlayerObject () {
		return myPlayerObject;
	}
	
}








