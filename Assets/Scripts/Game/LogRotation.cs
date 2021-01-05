using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRotation : MonoBehaviour
{
    [System.Serializable]
    private class RotationPattern
    {
        public float RotationSpeed;
        public float RotateDuration;
    }

    [SerializeField]
    private RotationPattern[] rotationPattern;

    private WheelJoint2D wheelJoint;
    private JointMotor2D motor;

    private void Awake()
    {
        wheelJoint = GetComponent<WheelJoint2D>();
        motor = new JointMotor2D();
        StartCoroutine(RotateLog());
    }

    private IEnumerator RotateLog()
    {
        int rotIndex = 0;
        while (true)
        {
            yield return new WaitForFixedUpdate();

            motor.motorSpeed = rotationPattern[rotIndex].RotationSpeed;
            motor.maxMotorTorque = 10000;
            wheelJoint.motor = motor;

            yield return new WaitForSeconds(rotationPattern[rotIndex].RotateDuration);
            rotIndex++;

            if (rotIndex >= rotationPattern.Length)
            {
                rotIndex = 0;
            }
        }
    }
}
