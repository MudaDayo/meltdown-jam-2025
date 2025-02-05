using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private InputActionAsset _inputAsset;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _airSpeed;
    [SerializeField] private float _jumpForce;
    private InputAction _movementAction;
    private InputAction _jumpAction;
    private InputAction _interactAction;
    private Rigidbody _rb;
    private Vector3 _desiredMovementDirection = Vector3.zero;
    void Start()
    {
        if (_inputAsset == null) return;

        _movementAction = _inputAsset.FindActionMap("Player").FindAction("Move");
        _jumpAction = _inputAsset.FindActionMap("Player").FindAction("Jump");
        _interactAction = _inputAsset.FindActionMap("Player").FindAction("Interact");
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
        HandleMovementInput();
    }

    private void FixedUpdate()
    {
        //Vector3 movement = _desiredMovementDirection;
        /*
        if (IsGrounded())
            movement *= _movementSpeed;
        else
            movement *= _airSpeed;
        
        _rb.linearVelocity = new Vector3(movement.x, _rb.linearVelocity.y, _rb.linearVelocity.z);
        */
        Vector3 nextPosition = transform.position + _desiredMovementDirection * _movementSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(nextPosition);
    }

    void HandleMovementInput()
    {
        if (_movementAction == null || _jumpAction == null) return;

        Vector2 movementInput = _movementAction.ReadValue<Vector2>();
        //Vector3 movement = movementInput * Vector3.right;
        _desiredMovementDirection = movementInput;

        if (_jumpAction.WasPressedThisFrame() && _jumpAction.IsPressed() && IsGrounded())
        {
            _rb.AddForce(Vector3.up * _jumpForce);
            //Jump();
        }

        if (_interactAction == null) return;
        if (_interactAction.WasPressedThisFrame())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
