using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour {
    [SerializeField]
    WheelCollider lf, rf, lr, rr;

    [SerializeField]
    float acceleration = 5;
    [SerializeField]
    float brakeSpeed = 5;
    [SerializeField]
    float handbrake = 5;
    [SerializeField]
    float rotateSpeed = 5;
    [SerializeField]
    float drag;
    [SerializeField]
    float torque;
    [SerializeField]
    float brakeTorque;
    [SerializeField]
    float handbrakeTorque;
    [SerializeField]
    float currentRotation = 0;
    [SerializeField]
    float turnBackSpeed = 2;

    void Start()
    {
        rigidbody.centerOfMass = new Vector3(0, -0.2f, 0);
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.W) && torque < 80)
        {
            torque += acceleration * Time.deltaTime;
            brakeTorque = 0;
        }
        else if (Input.GetKey(KeyCode.S))
        {

            brakeTorque += brakeSpeed * Time.deltaTime;
            torque = 0;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            torque = 0;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            brakeTorque = 0;
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            brakeTorque = handbrake;
        }
        

        if (Input.GetKey(KeyCode.A))
        {
            if (currentRotation > -10)
            {
                rf.transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0));
                lf.transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0));
                currentRotation += -rotateSpeed * Time.deltaTime;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (currentRotation < 10)
            {
                rf.transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
                lf.transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
                currentRotation += rotateSpeed * Time.deltaTime;
            }
        }
        else
        {
            rf.transform.Rotate(new Vector3(0, -currentRotation * Time.deltaTime*turnBackSpeed, 0));
            lf.transform.Rotate(new Vector3(0, -currentRotation * Time.deltaTime * turnBackSpeed, 0));
            currentRotation += -currentRotation * Time.deltaTime * turnBackSpeed;
        }

        lr.motorTorque = torque;
        rr.motorTorque = torque;
        lr.brakeTorque = brakeTorque;
        rr.brakeTorque = brakeTorque;
    }
}
