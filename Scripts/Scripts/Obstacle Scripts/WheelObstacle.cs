using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WheelCollider))]

public class WheelObstacle : MonoBehaviour {

    WheelCollider wc;
    public float maxMotorTorque = 400;
    public float maxSteeringAngle = 30;

    // Use this for initialization
    void Start () {
        wc = GetComponent<WheelCollider>();
        wc.transform.position = transform.position;
        wc.transform.rotation = transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        wc.motorTorque = maxMotorTorque * Input.GetAxis("Vertical");
        wc.steerAngle = maxSteeringAngle * Input.GetAxis("Horizontal");


        Vector3 position;
        Quaternion rotation;
        wc.GetWorldPose(out position, out rotation);

        transform.position = position;
        transform.rotation = rotation;

    }
}
