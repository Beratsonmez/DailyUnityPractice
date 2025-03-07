using Unity.Mathematics;
using UnityEngine;

public class RaycastTests : MonoBehaviour
{
    public Camera playerCamera;
    public float angleAdjustment = 5f;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RayTry();
        }
    }

    private void RayTry()
    {
        Vector3 origin = playerCamera.transform.position;
        Vector3 direction = playerCamera.transform.forward;


        for (int i = 0; i < 3; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(origin, direction, out hit, 15f))
            {
                Debug.DrawLine(origin, hit.point, Color.red, 15f);
                Vector3 cachedNormal = hit.normal;
                Vector3 newOrigin = hit.point + cachedNormal * 0.1f;
                Vector3 newDirection = cachedNormal;

                float angle = Vector3.Angle(direction, hit.normal);

                Vector3 reflectDir = Vector3.Reflect(direction, hit.normal);

                reflectDir = Quaternion.AngleAxis(angleAdjustment, hit.normal) * reflectDir;
                origin = hit.point + reflectDir * 0.01f;
                direction = reflectDir;

                if (Physics.Raycast(origin, direction, out hit, 10f))
                {
                    Debug.DrawRay(origin, direction, Color.red, 15f);
                }
            }
            else
            {
                break;
            }
        }
    }
}
