using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private InputActionReference _mouseInputActionReference;
    [SerializeField] private InputActionReference _orbitInputActionReference;
    [SerializeField] private InputActionReference _zoomInputActionReference;

    [SerializeField] private Transform aim;
    [SerializeField] private float angle = 10;
    [Range(0.5f, 10)]
    [SerializeField] private float zoomMin = 0.5f;
    [Range(10, 100)]
    [SerializeField] private float zoomMax = 100.0f;

    private Vector2 _mousePosition;

    private Vector3 _direction;
    private Vector3 _startPosition;

    private bool _orbit;


    private void Start()
    {
        _mouseInputActionReference.action.Enable();
        _orbitInputActionReference.action.Enable();
        _zoomInputActionReference.action.Enable();
    }
    private void OnEnable()
    {
        _mouseInputActionReference.action.performed += Position;
        _orbitInputActionReference.action.performed += Orbit;
        _zoomInputActionReference.action.performed += Zoom;
    }

    private void OnDisable()
    {
        _mouseInputActionReference.action.performed -= Position;
        _orbitInputActionReference.action.performed -= Orbit;
        _zoomInputActionReference.action.performed -= Zoom;
    }

    private void Update()
    {
        if (_orbit)
        {
            OnOrbit();
        }
    }


    private void Position(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
    }

    private void Orbit(InputAction.CallbackContext context)
    {
        _direction = _mousePosition;
        _orbit = !_orbit;
    }

    private void OnOrbit()
    {
        _startPosition = _mousePosition;
        _direction = (_startPosition - _direction);
        Vector3 axis = Vector3.zero;

        if (_direction.y > 0.5f)
        {
            axis += -transform.right;
        }
        else if (_direction.y < -0.5f)
        {
            axis += transform.right;
        }
        if (_direction.x > 0.5f)
        {
            axis += transform.up;
        }
        else if (_direction.x < -0.5f)
        {
            axis += -transform.up;
        }
        //}
        transform.RotateAround(aim.position, axis, angle);

        _direction = _startPosition;
    }

    private void Zoom(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        var zoomDiff = input.y;
        if (zoomDiff != 0)
        {

            if (zoomDiff > 0)
            {
                if (Vector3.Distance(transform.position, aim.position) > zoomMin)
                {
                    transform.position += transform.forward * 0.5f;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, aim.position) < zoomMax)
                {
                    transform.position -= transform.forward * 0.5f;
                }
            }
        }
    }
}
