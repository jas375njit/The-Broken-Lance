using UnityEngine;

public class LockCamera : MonoBehaviour
{
    private Quaternion lockedRotation;

    void Start()
    {
        lockedRotation = transform.rotation;
    }

    void LateUpdate()
    {
        transform.rotation = lockedRotation;
    }
}