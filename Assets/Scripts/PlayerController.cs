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

    [SerializeField] private InputActionReference _moveAction = null;
    [SerializeField] private InputActionReference _jumpAction = null;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (_jumpAction) _jumpAction.action.performed += OnJumpPerformed;
    }

    // Update is called once per frame
    void Update()
    {
        OnMovePerformed();
    }

    private void OnMovePerformed()
    {
        Vector2 input = _moveAction.action.ReadValue<Vector2>();
        input *= _speed;
        Vector3 movement = new Vector3(input.x, 0, input.y);

        //transform.Translate(movement * Time.deltaTime);
        _rigidbody.MovePosition(transform.position + (movement * Time.deltaTime));
    }

    private void OnJumpPerformed(InputAction.CallbackContext cbc)
    {
        Vector3 jump = new Vector3(0, _jumpForce, 0);
        _rigidbody.AddForce(jump, ForceMode.VelocityChange);
    }
}
