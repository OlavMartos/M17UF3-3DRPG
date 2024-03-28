using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
    // Singleton
    private static PlayerController _instance;
    public static PlayerController Instance
    {
        get { return _instance; }
    }

    // Private Variables
    private Animator _animator;
    private Rigidbody _rb;
    private Vector3 _velocity;
    [HideInInspector] private float _magnitude;

    // Input variables
    private InputManager inputManager;
    private Vector2 movementInput;
    public Vector3 _playerCamera;

    [Header("Test Bools")]
    public bool isDead;
    public bool isVictory;
    [Space]
    [SerializeField] private bool isGrounded;
    [HideInInspector] public bool isCrouching;
    [HideInInspector] public bool isJumping = false;
    [HideInInspector] private bool isAiming;
    [HideInInspector] private bool isFalling;
    [HideInInspector] private bool isWalking;
    [HideInInspector] private bool isRunning;

    [Header("Stadistics")]
    [SerializeField] public float playerSpeed;
    private float initialSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _sensitive;
    [SerializeField] private float gravityValue;
    [SerializeField] private float speedRotation;

    [Header("Shotting")]
    public GameObject bullet;
    private List<Transform> pool;
    public GameObject cannon;
    public int cloneMax;

    [Header("a")] [SerializeField] private bool PlayerControlsStatus;
    public Transform _transform { get; set; }

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(gameObject);
        else _instance = this;

        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _playerCamera = Camera.main.transform.forward;

        StartValues();
        InstantiatePoolItem();
    }

    private void InstantiatePoolItem()
    {
        pool = new List<Transform>();

        for (int i = 0; i <= cloneMax; i++)
        {
            GameObject shot = Instantiate(bullet, cannon.transform.position, Quaternion.identity, cannon.transform);
            shot.SetActive(false);
            pool.Add(shot.transform);
        }
    }

    /// <summary>
    /// Si las "Stadistics" son iguales a 0 se pondran unos valores hardcodeados
    /// </summary>
    void StartValues()
    {
        if (playerSpeed == 0)  playerSpeed = 30f;
        if (_jumpForce == 0)  _jumpForce = 1.5f;
        if (gravityValue == 0)  gravityValue = 9.81f;
        isJumping = false;
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
        initialSpeed = playerSpeed;
    }

    private void OnEnable()
    {
        InputManager.PlayerJump += Jump;
        InputManager.PlayerAim += Aiming;
        InputManager.PlayerCrouch += Crouch;
        InputManager.PlayerShot += Shot;
    }

    private void OnDisable()
    {
        InputManager.PlayerJump -= Jump;
        InputManager.PlayerAim -= Aiming;
        InputManager.PlayerCrouch -= Crouch;
        InputManager.PlayerShot -= Shot;
    }

    void Update()
    {
        DetectMovement();
        DetectJump();
        DetectFalling();
        DetectRunning();
        PlayerControlsStatus = inputManager.GetCurrentControlsStatus();

        // Death and Win Detect
        if (isDead && !isVictory) StartCoroutine(BadEnd());
        if (isVictory && !isDead) StartCoroutine(GoodEnd());
        if (isGrounded) _animator.SetBool("isFalling", false);
        if (isFalling) _animator.SetBool("isFalling", true); _animator.SetBool("startJump", false);
        if (isJumping) _animator.SetBool("startJump", true);

        // Mouse X
        float mouseXMove = Input.GetAxis("Mouse X");
        RotateCharacter(mouseXMove);

        // Call to functions
        Move();
        Run();
    }

    /// <summary>
    /// Detect if the player is moving
    /// </summary>
    private void DetectMovement()
    {
        movementInput = inputManager.GetPlayerMovement();
        CheckIfIsMoving();
    }

    /// <summary>
    /// Detect if the player is jumping
    /// </summary>
    private void DetectJump()
    {
        isGrounded = IsGrounded();
        if (isGrounded) { isJumping = false; isFalling = false; }
    }

    /// <summary>
    /// Detect if the player is falling
    /// </summary>
    private void DetectFalling()
    {
        isFalling = IsFalling();
        if (isFalling) isJumping = false;
    }

    /// <summary>
    /// Detect if the player is running
    /// </summary>
    private void DetectRunning()
    {
        _magnitude = inputManager.GetPlayerIsRunning() + 1f;
        isRunning = isWalking && _magnitude > 1.0f;
    }


    /// <summary>
    /// Detect if the player is grounded
    /// </summary>
    /// <returns>If the player is touching the ground returns true</returns>
    private bool IsGrounded()
    {
        return _rb.velocity.y == 0;
    }

    /// <summary>
    /// Detect if the player is falling
    /// </summary>
    /// <returns>If the player is falling to the ground returns true</returns>
    private bool IsFalling()
    {
        return _rb.velocity.y < 0;
    }

    /// <summary>
    /// Function that detects if the player is moving or not
    /// </summary>
    private void CheckIfIsMoving()
    {
        if (!(movementInput.x != 0 || movementInput.y != 0)) StartCoroutine(WaitForBoolToChange());
        else isWalking = true; 
    }

    private IEnumerator WaitForBoolToChange()
    {
        StopCoroutine(WaitForBoolToChange());
        yield return new WaitForSeconds(0.1f);
        isWalking = false;
        _animator.SetBool("isWalking", false) ;
    }

    /// <summary>
    /// Funtion that actually moves the player
    /// </summary>
    public void Move()
    {
        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
            isFalling = false;
        }

        if (movementInput.x != 0.0f || movementInput.y != 0.0f)
        {
            _animator.SetBool("isWalking", true);
            _animator.SetFloat("posX", movementInput.x);
            _animator.SetFloat("posY", movementInput.y);
            Vector3 direction = transform.forward * movementInput.y + transform.right * movementInput.x;
            _rb.MovePosition(transform.position + direction * playerSpeed * Time.deltaTime);
        }
    }

    private void RotateCharacter(float mouseXMove)
    {
        Vector3 rotation = new Vector3(0, mouseXMove * speedRotation, 0);
        transform.Rotate(rotation);
    }

    /// <summary>
    /// Function to jump using a gravityValue
    /// </summary>
    public void Jump()
    {
        if (isGrounded && !isCrouching && !isFalling)
        {
            isJumping = true;
            _rb.velocity = Vector3.up * _jumpForce * gravityValue;
        }
    }

    /// <summary>
    /// Function to run
    /// </summary>
    public void Run()
    {
        if(isRunning)
        {
            _animator.SetBool("isRunning", true);
            _animator.SetBool("isWalking", false);
            playerSpeed = 45f;
            _jumpForce = 3f;
        }
        else
        {
            _animator.SetBool("isRunning", false);
            playerSpeed = initialSpeed;
            _jumpForce = 1.5f;
        }
    }

    /// <summary>
    /// Function to aim
    /// </summary>
    public void Aiming()
    {
        isAiming = !isAiming;
        if (isAiming) SwapCamera.Instance.AimCamera();
        else  SwapCamera.Instance.NormalCamera();
        CanvasManager.Instance.AimPointer();
        _animator.SetBool("isAiming", isAiming);
    }

    public void Shot()
    {
        if (isAiming)
        {
            foreach (Transform shotTransform in pool)
            {
                if (!shotTransform.gameObject.activeSelf)
                {
                    shotTransform.position = cannon.transform.position;
                    shotTransform.rotation = cannon.transform.rotation;
                    shotTransform.gameObject.SetActive(true);

                    Rigidbody rbShot = shotTransform.GetComponent<Rigidbody>();
                    if (rbShot != null)
                    {
                        rbShot.AddForce(transform.forward * 50f, ForceMode.Impulse);
                    }

                    StartCoroutine(DisableBullet(shotTransform.gameObject, 2.0f));
                    return;
                }
            }
        }
    }

    IEnumerator DisableBullet(GameObject shot, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        shot.SetActive(false);
    }

    /// <summary>
    /// Function to crouch
    /// </summary>
    void Crouch()
    {
        isCrouching = !isCrouching;
        ChangeAnimatorLayer();
    }

    public void ChangeAnimatorLayer()
    {
        if (isCrouching)
        {
            Debug.Log("<b><i>No entiendo porque no se reproducen las anims de crouch</i></b>");
            _animator.SetBool("IsCrouching", true);
            _animator.SetLayerWeight(1, 1);
        }
        else
        {
            _animator.SetBool("IsCrouching", false);
            _animator.SetLayerWeight(1, 0);
        }
    }


    /// <summary>
    /// Function of the Bad End a.k.a the players die
    /// </summary>
    /// <returns></returns>
    IEnumerator BadEnd()
    {
        AudioManager.instance.Death();
        _animator.Play("Die");
        inputManager.DisableControls();
        yield return new WaitForSeconds(4f);
        // Se ha probado al recargar la escena, enviarlo a otra..., pero petaba por lo que as� se queda
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    /// <summary>
    /// The Good Ending, the player arrive to his destiny and make a silly dance
    /// </summary>
    /// <returns></returns>
    IEnumerator GoodEnd()
    {
        _animator.Play("Dance");
        inputManager.DisableControls();
        yield return new WaitForSeconds(2.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ICollectable>(out ICollectable iColl))
        {
            iColl.Collected();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<IDamagable>(out IDamagable iDam))
        {
            isDead = true;
        }
    }
}
