using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera Camera;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float minZoom = 5f;
    [SerializeField] private float maxZoom = 20f;

    private Vector3 lastMousePosition;

    private void Start()
    {
        Camera = GetComponent<Camera>();
        CenterCamera();
    }

    void CenterCamera()
    {
        if (Camera == null)
            Camera = Camera.main; // Fallback to the main camera if not assigned

        if (spriteRenderer != null)
        {
            // Get the bounds of the SpriteRenderer
            Bounds spriteBounds = spriteRenderer.bounds;

            // Center the camera on the sprite's center
            Camera.transform.position = new Vector3(spriteBounds.center.x, spriteBounds.center.y, Camera.transform.position.z);
        }
    }

    void Update()
    {
        HandlePanning();
        HandleZooming();
    }

    void HandlePanning()
    {
        // Panning with the middle mouse button
        if (Input.GetMouseButton(2)) // Middle mouse button
        {
            Vector3 delta = Camera.ScreenToWorldPoint(Input.mousePosition) - Camera.ScreenToWorldPoint(lastMousePosition);
            delta.z = 0;

            Camera.transform.position -= delta;
        }

        lastMousePosition = Input.mousePosition;

        // Clamp the camera position after panning
        Bounds cameraBounds = GetCameraBounds(Camera);
        Bounds spriteBounds = spriteRenderer.bounds;
        Camera.transform.position = ClampCameraPosition(Camera, cameraBounds, spriteBounds);
    }

    void HandleZooming()
    {
        // Zooming with the scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.orthographicSize -= scroll * zoomSpeed;
        Camera.orthographicSize = Mathf.Clamp(Camera.orthographicSize, minZoom, maxZoom);

        // Clamp the camera position after zooming
        Bounds cameraBounds = GetCameraBounds(Camera);
        Bounds spriteBounds = spriteRenderer.bounds;
        Camera.transform.position = ClampCameraPosition(Camera, cameraBounds, spriteBounds);
    }

    Bounds GetCameraBounds(Camera cam)
    {
        float cameraHeight = 2f * Camera.orthographicSize;
        float cameraWidth = cameraHeight * Camera.aspect;
        Vector3 cameraCenter = Camera.transform.position;

        return new Bounds(cameraCenter, new Vector3(cameraWidth, cameraHeight, 0));
    }

    Vector3 ClampCameraPosition(Camera cam, Bounds targetBounds, Bounds spriteBounds)
    {
        Vector3 newPosition = Camera.transform.position;
        float halfWidth = targetBounds.extents.x;
        float halfHeight = targetBounds.extents.y;

        // Clamp the camera position
        newPosition.x = Mathf.Clamp(newPosition.x, spriteBounds.min.x + halfWidth, spriteBounds.max.x - halfWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, spriteBounds.min.y + halfHeight, spriteBounds.max.y - halfHeight);

        return newPosition;
    }
}
