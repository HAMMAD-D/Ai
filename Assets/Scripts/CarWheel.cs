using UnityEngine;

public class CarWheel : MonoBehaviour
{
    public WheelCollider targetWheel;
    private Vector3 wheelPosition = new Vector3();
    private Quaternion wheelRotation = new Quaternion();

    void Update()
    {
        targetWheel.GetWorldPose(out wheelPosition, out wheelRotation);
        transform.position = wheelPosition;
        transform.rotation = wheelRotation;
        AddMotor();
    }

    void AddMotor()
    {
        targetWheel.motorTorque = 100f;
    }
}