using UnityEngine;
using System.Collections;

public class CameraManager : Photon.MonoBehaviour {

    [SerializeField]
    GameObject oculusObject, noOculusObject;
    [SerializeField]
    GameObject noOculusCamera, oculusCamera;
    [SerializeField]
    GameObject helicopter, noOculusCopter;
   [SerializeField]
    bool useOculus;

    void Start()
    {
        if (useOculus)
        {
            noOculusObject.SetActive(false);
            noOculusCopter.SetActive(false);
        }
        else
        {
            oculusObject.SetActive(false);
            helicopter.SetActive(false);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (useOculus)
        {
            stream.SendNext(oculusCamera.transform.rotation);
            stream.SendNext(helicopter.transform.rotation);
            stream.SendNext(helicopter.transform.position);
        }
        else
        {
            noOculusCamera.transform.rotation = (Quaternion)stream.ReceiveNext();
            noOculusCopter.transform.rotation = (Quaternion)stream.ReceiveNext();
            noOculusCopter.transform.position = (Vector3)stream.ReceiveNext();
        }
    }

}
