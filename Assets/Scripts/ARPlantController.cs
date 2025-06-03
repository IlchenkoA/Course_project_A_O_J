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

    void Start()
    {
        _raycastManager = FindObjectOfType<ARRaycastManager>();
        _anchorManager = FindObjectOfType<ARAnchorManager>();
        _initialScale = transform.localScale;
    }

    void Update()
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            float timeSinceLastTap = Time.time - _lastTapTime;
            _lastTapTime = Time.time;

            if (timeSinceLastTap <= _doubleTapThreshold)
            {
                // Обробка подвійного тапу
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

  
            // Масштабування
            if (Touchscreen.current.touches.Count == 2)
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

            // Переміщення
            if (Touchscreen.current.primaryTouch.press.isPressed && Touchscreen.current.touches.Count == 1)
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
