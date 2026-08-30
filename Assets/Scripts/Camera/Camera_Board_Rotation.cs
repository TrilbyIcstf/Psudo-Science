using UnityEngine;

public class Camera_Board_Rotation : MonoBehaviour
{
    [SerializeField]
    private Transform center;

    [SerializeField]
    private const float rotateSpeed = 5.0f;

    private float xPos = 0;
    private float yPos = 0;

    private void Awake()
    {
        Vector3 angles = transform.eulerAngles;
        xPos = angles.x;
        yPos = angles.y;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            xPos += Input.GetAxis("Mouse X") * rotateSpeed;
        }

        if (center != null)
        {
            Quaternion rotation = Quaternion.Euler(yPos, xPos, 0);

            float dist = Vector3.Distance(transform.position, center.position);
            Vector3 pos = rotation * new Vector3(0.0f, 0.0f, -dist) + center.position;

            transform.rotation = rotation;
            transform.position = pos;
        }
    }
}
