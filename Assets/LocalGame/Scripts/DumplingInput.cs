using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DumplingInput : MonoBehaviour
{
    public bool _canJump;
    public float _yCameraRotate, _xRotation;

    public void OnMovement(InputAction.CallbackContext _context)
    {
        Vector2 input = _context.ReadValue<Vector2>();
        Vector3 _moveDirection = new Vector3(input.x, 0f, input.y);
    }

    public void OnJump(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            _canJump = true;
        }
    }

    public void OnLookX(InputAction.CallbackContext _context)
    {
        _xRotation = _context.ReadValue<float>();
    }

    public void OnLookY(InputAction.CallbackContext _context)
    {
        _yCameraRotate = _context.ReadValue<float>();
    }
}
