using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;

    [SerializeField] private int _health = 1;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 15f;
    [SerializeField] private float _lookSensitivity = 1f;

    [SerializeField] private InputActionReference _moveAction = null;
    [SerializeField] private InputActionReference _jumpAction = null;
    [SerializeField] private InputActionReference _lookAction = null;
    [SerializeField] private InputActionReference _shootAction = null;

    private Rigidbody _rigidbody;

    public Gun gun;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (_jumpAction) _jumpAction.action.performed += OnJumpPerformed;
        if (_lookAction) _lookAction.action.performed += OnLookPerformed;
        if (_shootAction) _shootAction.action.performed += OnShootPerformed;
    }

    // Update is called once per frame
    void Update()
    {
        OnMovePerformed();
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 midLevelDir = new Vector3(
            other.transform.position.x - transform.position.x,
            0,
            other.transform.position.z - transform.position.z
            );
        bool rayHit = Physics.Raycast(transform.position, midLevelDir, out RaycastHit hit, 3f);

        if(!rayHit || hit.collider != other)
        {
            float translateDiff = (other.bounds.center.y + other.bounds.extents.y)
                - (transform.position.y - transform.lossyScale.y * 0.5f);
            transform.Translate(0, translateDiff, 0);
        }
    }

    private void OnShootPerformed(InputAction.CallbackContext cbc)
    {
        Vector2 reticle = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        gun.Shoot(reticle);
    }

    private void OnLookPerformed(InputAction.CallbackContext cbc)
    {
        Vector2 input = cbc.ReadValue<Vector2>();
        input *= _lookSensitivity;

        float xRotation = input.y;
        Vector3 yRotation = new Vector3(0, input.x, 0);

        // Use xRotation to lift & lower camera
        transform.Rotate(yRotation);
        _playerCamera.transform.Rotate(Vector3.right, -xRotation);
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
