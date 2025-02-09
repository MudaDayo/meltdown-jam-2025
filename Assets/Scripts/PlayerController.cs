using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private InputActionAsset _inputAsset;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _airSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _jumpClip, _landClip, _walkClip, _throwClip;
    [SerializeField] private Image _portrait;
    [SerializeField] private Sprite _portraitNormal, _portraitPanic, _portraitThrow, _portraitExplode;

    public bool panicMode = false;
    private bool _exploded = false;

    private InputAction _movementAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;
    private InputAction _interactAction;
    private InputAction _attackAction;
    [SerializeField] Animator animator;
    private Rigidbody _rb;
    private Vector3 _desiredMovementDirection = Vector3.zero;
    private Transform _visuals;

    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private float _launchForce;
    [SerializeField] private float _launchCooldown = 0.1f;
    private float _cooldownTimer;
    private bool _isDead = false;
    void Start()
    {
        if (_inputAsset == null) return;

        _movementAction = _inputAsset.FindActionMap("Player").FindAction("Move");
        _lookAction = _inputAsset.FindActionMap("Player").FindAction("Look");
        _jumpAction = _inputAsset.FindActionMap("Player").FindAction("Jump");
        _interactAction = _inputAsset.FindActionMap("Player").FindAction("Interact");
        _attackAction = _inputAsset.FindActionMap("Player").FindAction("Attack");
        _rb = GetComponent<Rigidbody>();
        _visuals = transform.Find("animation");
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
        if (!_isDead)
        {
            HandleMovementInput();
            _cooldownTimer += Time.deltaTime;
            if (_cooldownTimer >= _launchCooldown)
            {
                HandleAttackInput();
            }
        }

        SetAnimatorBools();

        HandleUIPortrait();
    }

    void HandleUIPortrait(){
        if(IsGrounded()){
            _exploded = false;
        }

        if(panicMode){
            _portrait.sprite = _portraitPanic;
        } else if(_cooldownTimer < _launchCooldown){
            _portrait.sprite = _portraitThrow;
        }else if(_exploded){
            _portrait.sprite = _portraitExplode;}
         else {
            _portrait.sprite = _portraitNormal;
        }
    }

    void SetAnimatorBools(){
        if(IsGrounded()){
            animator.SetBool("Jumping", false);
        } else {
            animator.SetBool("Jumping", true);
        }

        Vector2 movementInput = _movementAction.ReadValue<Vector2>();
        if(movementInput.x != 0){
            animator.SetBool("running", true);
        } else {
            animator.SetBool("running", false);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = _desiredMovementDirection.normalized;
        
        if (IsGrounded())
            movement *= _movementSpeed;
        else
            movement *= _airSpeed;

        if ((movement.x == 0 || (movement.x * _rb.linearVelocity.x) < 0) && IsGrounded())
        {
            float deceleration = Mathf.Lerp(_rb.linearVelocity.x, movement.x, _movementSpeed * Time.deltaTime);
            _rb.linearVelocity = new Vector3(deceleration, _rb.linearVelocity.y, _rb.linearVelocity.z);
        }
        else if (_rb.linearVelocity.x + movement.x * Time.deltaTime > _movementSpeed || _rb.linearVelocity.x + movement.x * Time.deltaTime < -_movementSpeed)
        { }
        else
            _rb.linearVelocity += movement * Time.deltaTime;
        //_rb.linearVelocity = new Vector3(movement.x, _rb.linearVelocity.y, _rb.linearVelocity.z);
        
        //Vector3 nextPosition = transform.position + _desiredMovementDirection * _movementSpeed * Time.fixedDeltaTime;
        //_rb.MovePosition(nextPosition);
    }

    void HandleMovementInput()
    {
        if (_movementAction == null || _jumpAction == null) return;

        Vector2 movementInput = _movementAction.ReadValue<Vector2>();
        //Vector3 movement = movementInput * Vector3.right;
        _desiredMovementDirection = movementInput * Vector3.right;

        if(IsGrounded() && movementInput.x != 0 && !_audioSource.isPlaying){
            _audioSource.clip = _walkClip;
            _audioSource.Play();
        }else if(movementInput.x == 0 && _audioSource.clip == _walkClip){
            _audioSource.Stop();
        }

        if (_visuals != null)
        {
            if (movementInput.x > 0)
            {
                if (_visuals.localScale.x > 0)
                {
                    _visuals.localScale = new Vector2(_visuals.localScale.x * -1, _visuals.localScale.y);
                }
            }
            else if (movementInput.x < 0)
            {
                _visuals.localScale = new Vector2(Math.Abs(_visuals.localScale.x), _visuals.localScale.y);
            }
        }

        if (_jumpAction.WasPressedThisFrame() && _jumpAction.IsPressed() && IsGrounded())
        {
            _rb.AddForce(Vector3.up * _jumpForce);
            //Jump();
            _audioSource.clip = _jumpClip;
            _audioSource.Play();
        }

            
    }

    void HandleAttackInput()
    {
        if (_interactAction == null || _attackAction == null) return;

        if (_attackAction.WasPressedThisFrame() && _bombPrefab != null)
        {
            _cooldownTimer = 0;
            _audioSource.clip = _throwClip;
            _audioSource.Play();

            Vector2 lookDirection = _lookAction.ReadValue<Vector2>();
            Vector3 launchDirection = new Vector3(lookDirection.x, lookDirection.y, 0).normalized;
            Debug.Log("One Boom for this guy");
            Debug.Log(launchDirection);
            if (launchDirection == Vector3.zero)
            {
                Debug.Log("Using mouse instead");
                Vector3 mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
                launchDirection = transform.TransformPoint(mousePosition);
                launchDirection.z = 0;
                launchDirection.Normalize();
                Debug.Log(launchDirection);
            }
            var pipeBomb = Instantiate(_bombPrefab, transform.position, Quaternion.identity);
            pipeBomb.GetComponent<Rigidbody>().AddForce(launchDirection * _launchForce + _rb.linearVelocity);
            pipeBomb.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, UnityEngine.Random.Range(-10,10)));
            //Vector3 mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            //_rb.AddForce(transform.TransformPoint(mousePosition).normalized * _explosionForce);
            //_rb.AddExplosionForce(_explosionForce, mousePosition, _explosionRadius);
        }

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
        LayerMask layerMask = LayerMask.GetMask("Default", "Breakable");
        return Physics.Raycast(transform.position, -Vector3.up, 1.1f, layerMask);
    }

    void GameOver()
    {
        SceneManager.LoadScene("DeathScene");
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _rb.AddExplosionForce(50000, collision.transform.position, 100);
            _isDead = true;
            Invoke("GameOver", 1f);
        }

        if ((collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("Breakable")) && IsGrounded() && collision.transform.position.y < transform.position.y)
        {
            _audioSource.clip = _landClip;
            _audioSource.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Explosion"))
        {
            _exploded = true;
        }
    }

}
