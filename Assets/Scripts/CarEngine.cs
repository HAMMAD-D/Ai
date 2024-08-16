using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{
    public Transform path;
    private List<Transform> nodes;
    private int currentNode = 0;
    private float maxSteerAngle = 45.0f;
    public float maxMotorTorque = 500f;
    public float maxSpeed = 20f;
    public float nodeReachThreshold = 3f;
    private Rigidbody carRb;
    public WheelCollider Fl;
    public WheelCollider FR;
    public WheelCollider RL;
    public WheelCollider RR;
    public float sensorLength = 5f;
    public float frontSensorPosition = 0.5f;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = new Vector3(0, 0, 0);
        Transform[] pathTransform = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        for (int i = 0; i < pathTransform.Length; i++)
        {
            if (pathTransform[i] != path.transform)
            {
                nodes.Add(pathTransform[i]);
            }
        }
    }

    void FixedUpdate()
    {
        ApplySteering();
        Drive();
        CheckNodeDistance();
    }


    void ApplySteering()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        Fl.steerAngle = newSteer;
        FR.steerAngle = newSteer;
    }

    void Drive()
    {
        float speed = GetComponent<Rigidbody>().velocity.magnitude;
        if (speed < maxSpeed)
        {
            RL.motorTorque = maxMotorTorque;
            RR.motorTorque = maxMotorTorque;
            Fl.motorTorque = maxMotorTorque;
            FR.motorTorque = maxMotorTorque;
        }
        else
        {
            RL.motorTorque = 0f;
            RR.motorTorque = 0f;
            FR.motorTorque = 0f;
            Fl.motorTorque = 0f;
        }
    }

    void CheckNodeDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < nodeReachThreshold)
        {
            if (currentNode < nodes.Count - 1)
            {
                currentNode++;
            }
            else
            {
                currentNode = 0;
            }
        }
    }
}