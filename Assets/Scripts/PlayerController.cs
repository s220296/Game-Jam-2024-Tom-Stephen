using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private int _health = 1;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 15f;
    [SerializeField] private float _lookSensitivity = 1f;

    [SerializeField] private InputActionReference _moveAction = null;
    [SerializeField] private InputActionReference _jumpAction = null;
    [SerializeField] private InputActionReference _lookAction = null;
    [SerializeField] private InputActionReference _shootAction = null;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (_jumpAction) _jumpAction.action.performed += OnJumpPerformed;
        if (_lookAction) _lookAction.action.performed += OnLookPerformed;
    }

    // Update is called once per frame
    void Update()
    {
        OnMovePerformed();
    }

    private void OnLookPerformed(InputAction.CallbackContext cbc)
    {
        Vector2 input = cbc.ReadValue<Vector2>();
        input *= _lookSensitivity;

        Vector3 xRotation = new Vector3(input.y, 0, 0);
        Vector3 yRotation = new Vector3(0, input.x, 0);

        // Use xRotation to lift & lower camera
        transform.Rotate(yRotation);
    }

    private void OnMovePerformed()
    {
        Vector2 input = _moveAction.action.ReadValue<Vector2>();
        Vector3 movement = transform.forward * input.y + transform.right * input.x;

        movement *= _speed;

        //transform.Translate(movement * Time.deltaTime);
        _rigidbody.MovePosition(transform.position + (movement * Time.deltaTime));
    }

    private void OnJumpPerformed(InputAction.CallbackContext cbc)
    {
        Vector3 jump = new Vector3(0, _jumpForce, 0);
        _rigidbody.AddForce(jump, ForceMode.VelocityChange);
    }
}
