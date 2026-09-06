using UnityEngine;

public class Camera_Board_Rotation : MonoBehaviour
{
    [SerializeField]
    private Transform center;

    private const float rotateSpeed = 3.0f;
    private const float aVal = 1.0f / 10;
    private const float bVal = -1.0f / 7;

    private float dist = 12.2f;

    private float xRot = 0;
    private float yRot = 0;

    private void Awake()
    {
        Vector3 angles = transform.eulerAngles;
        xRot = angles.x;
        yRot = angles.y;
    }

    private void LateUpdate()
    {
        float scrollDelta = Input.mouseScrollDelta.y;

        if (scrollDelta > 0 && dist > 6.0f)
        {
            dist -= 0.4f;
        } else if (scrollDelta < 0 && dist < 16.0f)
        {
            dist += 0.4f;
        }

        if (Input.GetMouseButton(1))
        {
            yRot += Input.GetAxis("Mouse X") * rotateSpeed;
        }

        if (center != null)
        {
            Quaternion rot = Quaternion.Euler(0, yRot, 0);

            Vector3 pos = rot * new Vector3(0, aVal * UVal(dist), bVal * UVal(dist)) + center.position;
            transform.position = pos;
            transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        }
    }

    private float UVal(float dist)
    {
        float aSqrd = Mathf.Pow(aVal, 2);
        float bSqrd = Mathf.Pow(bVal, 2);
        return dist / Mathf.Sqrt(aSqrd + bSqrd);
    }
}
