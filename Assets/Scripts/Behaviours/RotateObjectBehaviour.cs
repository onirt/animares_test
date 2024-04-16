using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class RotateObjectBehaviour : MonoBehaviour
{
    [SerializeField] private InputActionReference _mouseInputActionReference;
    [SerializeField] private float _rotationSpeed = 10;

    private Vector3 _mousePosition;
    private Vector3 _lastMousePosition;
    private bool _isRotating;

    private void Start()
    {
        _mouseInputActionReference.action.Enable();
    }
    private void OnEnable()
    {
        _mouseInputActionReference.action.performed += Position;
    }

    private void OnDisable()
    {
        _mouseInputActionReference.action.performed -= Position;
    }
    private void OnMouseDown()
    {
        Debug.Log($"[PointerDown]: {name}");
        _isRotating = true;
        _lastMousePosition = _mousePosition;
    }
    private void OnMouseUp()
    {
        Debug.Log($"[PointerUp]: {name}");
        _isRotating = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (_isRotating)
        {
            var delta = _mousePosition - _lastMousePosition;
            transform.Rotate(Vector3.up, -delta.x * _rotationSpeed, Space.World);
            transform.Rotate(Vector3.right, delta.y * _rotationSpeed, Space.World);
            _lastMousePosition = _mousePosition;
        }
    }

    private void Position(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
    }
}
