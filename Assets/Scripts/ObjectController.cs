using UnityEngine;
using DG.Tweening;

public class ObjectController : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField]
    private Transform target;         // The object to rotate and zoom
    [SerializeField]
    private Camera cam;               // Camera to zoom in/out

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float rotationSmoothTime = 0.2f;
    private Vector2 rotationInput;
    private Vector2 currentRotationVelocity;
    private Vector2 smoothedRotation;

    [Header("Zoom Settings")]

    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZoomDistance = 2f;
    [SerializeField] private float maxZoomDistance = 15f;
    [SerializeField] private float zoomSmoothTime = 0.3f;

    private float targetDistance;
    private Tween zoomTween;

    Vector3 v3Down = Vector3.down, v3Right = Vector3.right;

    public static ObjectController Instance { get; private set; }

    bool m_bInteractable = true;
    public bool Interactable { get { return m_bInteractable; } set { m_bInteractable = value; } }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        if (cam == null) cam = Camera.main;
        targetDistance = Vector3.Distance(cam.transform.position, target.position);
    }

    void Update()
    {
        HandleRotation();
        HandleZoom();
    }

    // void HandleRotation()
    // {
    //     if (Input.GetMouseButton(0))
    //     {
    //         float mouseX = Input.GetAxis("Mouse X");
    //         float mouseY = Input.GetAxis("Mouse Y");
    //         target.RotateAround(target.position, v3Down, mouseX * rotationSpeed);
    //         target.RotateAround(target.position, v3Right, mouseY * rotationSpeed);
    //     }
    // }

    void HandleRotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UIInfoPrompt.Instance.Hide();
        }
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Accumulate input deltas
            rotationInput.x = mouseX * rotationSpeed;
            rotationInput.y = mouseY * rotationSpeed;
        }
        else
        {
            rotationInput = Vector2.zero;
        }

        // Smooth the rotation deltas
        smoothedRotation = Vector2.SmoothDamp(smoothedRotation, rotationInput, ref currentRotationVelocity, rotationSmoothTime);

        // Apply smoothed rotation
        if (target != null)
        {
            target.RotateAround(target.position, v3Down, smoothedRotation.x * Time.deltaTime);
            target.RotateAround(target.position, v3Right, smoothedRotation.y * Time.deltaTime);
        }
    }

    void HandleZoom()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scrollDelta) > 0.01f)
        {
            UIInfoPrompt.Instance.Hide();
            float newDistance = Mathf.Clamp(targetDistance - scrollDelta * zoomSpeed, minZoomDistance, maxZoomDistance);

            // Kill any existing zoom tween
            if (zoomTween != null && zoomTween.IsActive()) zoomTween.Kill();

            zoomTween = DOTween.To(() => targetDistance, x => targetDistance = x, newDistance, zoomSmoothTime)
                .SetEase(Ease.OutQuad)
                .OnUpdate(UpdateCameraZoom);
        }
    }

    void UpdateCameraZoom()
    {
        Vector3 dir = (cam.transform.position - target.position).normalized;
        cam.transform.position = target.position + dir * targetDistance;
        cam.transform.LookAt(target);
    }
}
