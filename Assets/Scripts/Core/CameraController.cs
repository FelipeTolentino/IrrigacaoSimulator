using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    // Singleton ref for this script
    public static CameraController instance;
    
    public Transform followTransform;
    [SerializeField] private Transform cameraTransform;
    
    [SerializeField] private float normalSpeed;
    [SerializeField] private float fastSpeed;
    [SerializeField] private float movementTime;
    [SerializeField] private float rotationAmount;
    [SerializeField] private Vector3 zoomAmount;
    private float _movementSpeed;
    
    [SerializeField] private Vector3 newPosition;
    [SerializeField] private Quaternion newRotation;
    [SerializeField] private Vector3 newZoom;

    private Vector3 dragStartPosition;
    private Vector3 dragCurrentPosition;
    private Vector3 rotateStartPosition;
    private Vector3 rotateCurrentPosition;

    void Start()
    {
        instance = this;
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (followTransform != null)
        {
            transform.position = followTransform.position;
        }
        else
        {
            HandleMovementInput();
            HandleMouseInput();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            followTransform = null;
        }
    }

    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }

        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
        }
        
        
        if (Input.GetKey(KeyCode.F) || (Input.GetAxis("Mouse ScrollWheel") < 0f))
        {
            newZoom -= zoomAmount;
        }
       

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
        
    }

    void HandleMouseInput()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * (zoomAmount * 2);
        }
        if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftShift))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftShift))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
            
            
        }

        if (Input.GetMouseButtonDown(2))
        {
            rotateStartPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }
        
    }
}
