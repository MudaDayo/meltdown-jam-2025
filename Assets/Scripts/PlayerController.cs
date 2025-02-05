using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private InputActionAsset _inputAsset;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;
    private InputAction _movementAction;
    private InputAction _jumpAction;
    private Rigidbody _rb;
    private Vector3 _desiredMovementDirection = Vector3.zero;
    void Start()
    {
        if (_inputAsset == null) return;

        _movementAction = _inputAsset.FindActionMap("Player").FindAction("Move");
        _jumpAction = _inputAsset.FindActionMap("Player").FindAction("Jump");
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (_inputAsset == null) return;

        _inputAsset.Enable();
    }

    private void OnDisable()
    {
        if (_inputAsset == null) return;

        _inputAsset.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 movement = _desiredMovementDirection * _movementSpeed;
        _rb.linearVelocity = new Vector3(movement.x, _rb.linearVelocity.y, _rb.linearVelocity.z);
        HandleMovementInput();
    }

    void HandleMovementInput()
    {
        if (_movementAction == null || _jumpAction == null) return;

        Vector2 movementInput = _movementAction.ReadValue<Vector2>();
        //Vector3 movement = movementInput * Vector3.right;
        _desiredMovementDirection = movementInput;

        if (_jumpAction.IsPressed() && IsGrounded())
        {
            Jump();
        }
    }

    void Jump()
    {
        if (IsGrounded())
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, _jumpForce * Time.deltaTime, _rb.linearVelocity.z);
        }
    }
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 1.1f);
    }
}
