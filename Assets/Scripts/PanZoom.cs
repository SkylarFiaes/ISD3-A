using UnityEngine;
using Cinemachine;

public class PanZoom : MonoBehaviour
{
    [SerializeField]
    public float panSpeed = 5f;
    [SerializeField]
    public float zoomSpeed = 10f;
    [SerializeField]
    public float rotateSpeed = 5f;
    [SerializeField]
    public float zoomInMax = 10f;
    [SerializeField]
    public float zoomOutMax = 90f;

    [SerializeField]
    private float lookSpeed = 10f;
    
    private CinemachineInputProvider inputProvider;
    private CinemachineFreeLook freeLookCamera;
    private Transform cameraTransform;

    private void Awake()
    {
        inputProvider = GetComponent<CinemachineInputProvider>();
        freeLookCamera = GetComponent<CinemachineFreeLook>();
        cameraTransform = freeLookCamera.VirtualCameraGameObject.transform;
    }

    public void Update()
    {
        float x = inputProvider.GetAxisValue(0);
        float y = inputProvider.GetAxisValue(1);
        float z = inputProvider.GetAxisValue(2);
        if (x != 0 || y != 0)
        {
            PanScreen(x,y);
        }
        if (z != 0)
        {
            ZoomScreen(z);
        }
    }

    public void ZoomScreen(float increment)
    {
        float fov = freeLookCamera.m_Lens.FieldOfView;
        float target = Mathf.Clamp(fov + increment, zoomInMax, zoomOutMax);
        freeLookCamera.m_Lens.FieldOfView = Mathf.Lerp(fov, target, zoomSpeed * Time.deltaTime);
    }

    public void PanScreen(float x, float y)
    {
        Vector2 direction = PanDirection(x,y);
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraTransform.position + (Vector3)direction * panSpeed, Time.deltaTime);
    }

    public Vector2 PanDirection(float x, float y)
    {
        Vector2 direction = Vector2.zero;
        if (y >= Screen.height * 0.95f)
        {
            direction.y += 1;
        }
        else if (y <= Screen.height * 0.05f)
        {
            direction.y -= 1;
        }
        if (x >= Screen.width * 0.95f)
        {
            direction.x -= 1;
        }
        if (x <= Screen.width * 0.05f)
        {
            direction.x += 1;
        }
        return direction;
    }
}
