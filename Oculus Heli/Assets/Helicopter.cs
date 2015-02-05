using UnityEngine;
using System.Collections;

public class Helicopter : MonoBehaviour {
    
    [SerializeField]
    float upForce = 0;
    [SerializeField]
    float acceleration = 5f;
    [SerializeField]
    float drag = 0.999f;
    [SerializeField]
    float rotateSpeed = 10f;
	[SerializeField]
	float baseSpeed = 9.8f, multiplier = 4f;

    [SerializeField]
    Transform rotor;
    [SerializeField]
    float rotorAcceleration = 15;

    void Update()
    {
        if (upForce < 0) upForce = 0;
        transform.rigidbody.velocity += upForce * transform.up*Time.deltaTime;
		rotor.Rotate(new Vector3(0,0,15 * Time.deltaTime * rotorAcceleration)); //rotor.Rotate(new Vector3(0,0,upForce * Time.deltaTime * rotorAcceleration));

		Debug.Log (Input.GetAxis ("Throttle"));
		upForce = baseSpeed + Input.GetAxis ("Throttle") * multiplier;
//        if (Input.GetKey(KeyCode.LeftShift))
//			upForce = 14;//upForce += acceleration * Time.deltaTime;
//        else if (Input.GetKey(KeyCode.LeftControl))
//			upForce = 6;//upForce -= acceleration * Time.deltaTime;
//		else
//			upForce = 9.8f;

		if (Input.GetKey (KeyCode.Joystick1Button1) || Input.GetKey(KeyCode.R))
			Application.LoadLevel ("Scene4");
			//Debug.Log ("Reset");

        if (Input.GetAxis("Pitch") != 0) 
			transform.Rotate(new Vector3(-Input.GetAxis("Pitch") * rotateSpeed * Time.deltaTime,0,0));
        if (Input.GetAxis("Roll") != 0) 
			transform.Rotate(new Vector3( 0, 0,Input.GetAxis("Roll") * rotateSpeed * Time.deltaTime));

		Vector3 up = new Vector3(0, 1, 0);
        if (Input.GetAxis("Yaw") != 0)
			transform.RotateAround(transform.position, up, (Input.GetAxis("Yaw") * rotateSpeed * Time.deltaTime));
    }

    void Reset()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
}
