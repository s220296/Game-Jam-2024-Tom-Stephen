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

    private bool _isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _isGrounded = true;

        if (_jumpAction) _jumpAction.action.performed += OnJumpPerformed;
        if (_lookAction) _lookAction.action.performed += OnLookPerformed;
        if (_shootAction) _shootAction.action.performed += OnShootPerformed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        OnMovePerformed();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Half way between the middle of the character and contact point
        // Lifted to the center of the player and checked for distance of player's
        // y-extent
        Vector3 castPoint = collision.GetContact(0).point;
        castPoint.y = transform.position.y;

        bool rayHit = Physics.Raycast(castPoint, Vector3.down, transform.lossyScale.y + 0.01f);
        if (rayHit)
            _isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        bool rayHit = Physics.Raycast(transform.position, Vector3.down, transform.lossyScale.y + 0.01f);
        if (!rayHit)
            _isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 midLevelDir = new Vector3(
            other.transform.position.x - transform.position.x,
            0,
            other.transform.position.z - transform.position.z
            );
        bool rayHit = Physics.Raycast(transform.position, midLevelDir, out RaycastHit hit, 3f);

        // If the step is not at the mid level and is lower than our player
        if((!rayHit || hit.collider != other) && other.transform.position.y < transform.position.y)
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

        float dot = Mathf.Abs(Vector3.Dot(_playerCamera.transform.forward, Vector3.up));
        if (dot < 0.95)
            _playerCamera.transform.Rotate(Vector3.right, -xRotation);
        // If gone past the limit, reset
        dot = Mathf.Abs(Vector3.Dot(_playerCamera.transform.forward, Vector3.up));
        if (dot >= 0.95)
            _playerCamera.transform.Rotate(Vector3.right, xRotation);
    }

    private void OnMovePerformed()
    {
        Vector2 input = _moveAction.action.ReadValue<Vector2>();
        Vector3 movement = transform.forward * input.y + transform.right * input.x;

        Vector3 vel = _rigidbody.velocity;
        float gravity = vel.y;

        if (input == Vector2.zero)
        { // if not moving, stop moving, just fall
            vel.x = 0;
            vel.z = 0;
        }
        else // if inputting movement
        {
            vel += movement;
            vel.Normalize();
            vel *= _speed;
            vel.y = gravity;
        }

        _rigidbody.velocity = vel;
    }

    private void OnJumpPerformed(InputAction.CallbackContext cbc)
    {
        if (!_isGrounded) return;
        Vector3 jump = new Vector3(0, _jumpForce, 0);
        _rigidbody.AddForce(jump, ForceMode.VelocityChange);
        _isGrounded = false;
    }
}
