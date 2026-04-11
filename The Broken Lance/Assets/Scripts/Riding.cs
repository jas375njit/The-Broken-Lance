using UnityEngine;

public class RidingPose : MonoBehaviour
{
    public Transform leftThigh;
    public Transform rightThigh;
    public float legSpreadAngle = 35f;
    public float legForwardAngle = 45f;

    void LateUpdate()
    {
        if (leftThigh != null)
            leftThigh.localRotation = Quaternion.Euler(legForwardAngle, 0f, legSpreadAngle);
        if (rightThigh != null)
            rightThigh.localRotation = Quaternion.Euler(legForwardAngle, 0f, -legSpreadAngle);
    }
}