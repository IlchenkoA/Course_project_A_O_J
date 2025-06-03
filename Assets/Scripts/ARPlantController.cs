using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ARPlantController : MonoBehaviour
{
    private ARRaycastManager _raycastManager;
    private ARAnchorManager _anchorManager;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    private bool _isPlaced = false;
    private Vector2 _firstTouchPosition;
    private float _initialDistance;
    private Vector3 _initialScale;
    private ARAnchor _currentAnchor;

    private float _lastTapTime = 0f;
    private float _doubleTapThreshold = 0.3f;

    private bool _isRotating = false;
    private Vector3 _lastMousePosition;

    void Start()
    {
        _raycastManager = FindObjectOfType<ARRaycastManager>();
        _anchorManager = FindObjectOfType<ARAnchorManager>();
        _initialScale = transform.localScale;
    }

    void Update()
    {
        // ==========================
        // Подвійний клік ЛКМ / тап — Поставити / прибрати об’єкт
        // ==========================
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            float timeSinceLastTap = Time.time - _lastTapTime;
            _lastTapTime = Time.time;

            if (timeSinceLastTap <= _doubleTapThreshold)
            {
                var touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                if (!_isPlaced)
                {
                    if (_raycastManager.Raycast(touchPosition, _hits, TrackableType.PlaneWithinPolygon))
                    {
                        Pose hitPose = _hits[0].pose;
                        GameObject anchorObject = new GameObject("ARAnchor");
                        anchorObject.transform.position = hitPose.position;
                        anchorObject.transform.rotation = hitPose.rotation;
                        _currentAnchor = anchorObject.AddComponent<ARAnchor>();
                        transform.SetParent(_currentAnchor.transform);
                        _isPlaced = true;
                    }
                }
                else
                {
                    if (_currentAnchor != null)
                    {
                        Destroy(_currentAnchor.gameObject);
                        transform.SetParent(null);
                        _isPlaced = false;
                    }
                }

                return;
            }
        }

        // ==========================
        // Коліщатко миші — Масштаб
        // ==========================
        float scroll = Mouse.current.scroll.ReadValue().y;
        if (Mathf.Abs(scroll) > 0.01f)
        {
            float scaleFactor = 1f + scroll * 0.01f;
            transform.localScale *= scaleFactor;
        }

        // ==========================
        // ПКМ + рух миші — Переміщення
        // ==========================
        if (Mouse.current.rightButton.isPressed && !Keyboard.current.altKey.isPressed)
        {
            Vector3 delta = Mouse.current.delta.ReadValue();
            Vector3 move = new Vector3(delta.x, 0, delta.y) * 0.001f;
            transform.position += move;
        }

        // ====================================================
        // Alt + ПКМ + рух миші (вліво/вправо) — Поворот навколо осі Y
        // Alt + ПКМ + рух миші (вгору/вниз) — Вертикальний нахил (обертання)
        // ====================================================
        if (Mouse.current.rightButton.isPressed && Keyboard.current.altKey.isPressed)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            float rotationY = mouseDelta.x * 0.5f;       // обертання навколо осі Y
            float rotationX = -mouseDelta.y * 0.5f;      // вертикальний нахил

            transform.Rotate(Vector3.up, rotationY, Space.World);
            transform.Rotate(Vector3.right, rotationX, Space.Self);
        }

        // ==========================
        // Пінч на сенсорі — Масштаб
        // ==========================
        if (Touchscreen.current != null && Touchscreen.current.touches.Count == 2)
        {
            var touch1 = Touchscreen.current.touches[0].position.ReadValue();
            var touch2 = Touchscreen.current.touches[1].position.ReadValue();

            if (Touchscreen.current.touches[0].phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began ||
                Touchscreen.current.touches[1].phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
            {
                _initialDistance = Vector2.Distance(touch1, touch2);
            }
            else
            {
                float currentDistance = Vector2.Distance(touch1, touch2);
                float scaleFactor = currentDistance / _initialDistance;
                transform.localScale = _initialScale * scaleFactor;
            }
        }

        // ==========================
        // Один дотик — Переміщення
        // ==========================
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed && Touchscreen.current.touches.Count == 1)
        {
            var touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            if (_raycastManager.Raycast(touchPosition, _hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = _hits[0].pose;
                transform.position = hitPose.position;
            }
        }
    }
}
