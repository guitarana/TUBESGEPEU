using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform Target;
    public float distance = 10;
    public float height = 10;

    private void Update()
    {
        Vector3 newPosition = Target.position;
        newPosition.z = Target.position.z + distance;
        newPosition.y = Target.position.y + height;
        transform.position = Vector3.Slerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);
    }
}