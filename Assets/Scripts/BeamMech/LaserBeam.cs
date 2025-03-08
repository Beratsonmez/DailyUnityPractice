using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public Transform source;
    public LayerMask targetMask;
    public LineRenderer lineRenderer;

    public Renderer cube;
    public Material originalMaterial;
    public Material newMaterial;

    public Transform player;
    public PlayerMovement playerMovement;
    public bool controllingLaser = false;
    public Camera playerCamera;
    private float sens = 2f;

    private void Start()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        player = GameObject.FindWithTag("Player").transform;
        playerMovement = player.GetComponent<PlayerMovement>();

        originalMaterial = cube.material;
    }

    private void Update()
    {
        ShootLaser();
        LaserControl();
    }

    private void LaserControl()
    {
        if (Input.GetKey(KeyCode.E))
        {
            controllingLaser = !controllingLaser;

            if (controllingLaser)
            {
                LockPlayer();
            }
            else
            {
                UnlockPlayer();
            }
        }
        if(controllingLaser)
        {
            RotateLaser();
        }
    }

    private void LockPlayer()
    {
        playerMovement.enabled = false;
    }

    private void UnlockPlayer()
    {
        playerMovement.enabled= true;
    }

    private void RotateLaser()
    {
        float mouseX = Input.GetAxis("Mouse X") * sens;

        source.Rotate(Vector3.up * mouseX, Space.World);

    }

    private void ChangeColor(bool hitTarget)
    {
        if (hitTarget == true)
        {
            cube.material = newMaterial;
            Debug.Log("Küp rengi deðiþtirildi.");
        }
        else
        {
            cube.material = originalMaterial;
        }
    }

    private void ShootLaser()
    {
        Ray ray = new Ray(source.position, source.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 15f))
        {
            lineRenderer.SetPosition(0, source.position);
            lineRenderer.SetPosition(1, hit.point);


            if (hit.collider.CompareTag("target"))
            {
                Debug.Log("Hedef Vuruldu");
                ChangeColor(true);
            }
        }
        else
        {
            lineRenderer.SetPosition(0, source.position);
            lineRenderer.SetPosition(1, source.position + source.forward * 100f);
            ChangeColor(false);
        }
    }
}
