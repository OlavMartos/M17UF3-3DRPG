using System.Collections;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

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
    private Vector3 velocity;

    // Input variables
    private InputManager inputManager;
    private Vector2 movementInput;
    private Vector2 mouseInput;
    private Transform _playerCamera;

    [Header("Test Bools")] // Posteriormente sustituir por [HideInInspector]
    [SerializeField] private bool isDead;
    [SerializeField] private bool isVictory;
    [Space]
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isLanded;
    [SerializeField] private bool isCrouching;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isAiming;
    [SerializeField] private bool isFalling;
    [SerializeField] private bool isWalking;

    [Header("Stadistics")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _sensitive;
    [SerializeField] private float gravityValue;

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(gameObject);
        else _instance = this;

        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _playerCamera = Camera.main.transform;


        StartValues();
    }

    void StartValues()
    {
        string color = "cyan";
        string color2 = "cyan";
        string color3 = "cyan";

        Debug.Log("<color=cyan>Player Stats:</color>");
        if (playerSpeed == 0)
        {
            playerSpeed = 30f;
            color = "red";
        }

        if (_jumpForce == 0)
        {
            _jumpForce = 1.5f;
            color2 = "red";
        }

        if (gravityValue == 0)
        {
            gravityValue = 9.81f;
            color3 = "red";
        }

        Debug.Log($"\t<color={color}>playerSpeed esta a {playerSpeed}</color>");
        Debug.Log($"\t<color={color2}>jumpForce puesta a {_jumpForce}</color>");
        Debug.Log($"\t<color={color3}>gravityValue esta a {gravityValue}</color>");
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        InputManager.PlayerJump += Jump;
        InputManager.PlayerAim += Aiming;
    }

    private void OnDisable()
    {
        InputManager.PlayerJump -= Jump;
        InputManager.PlayerAim -= Aiming;
    }

    void Update()
    {
        movementInput = inputManager.GetPlayerMovement();
        CheckIfIsMoving();
        isGrounded = IsGrounded();
        
        if (isDead && !isVictory) StartCoroutine(BadEnd());
        if (isVictory && !isDead) StartCoroutine(GoodEnd());

        Move();
    }

    private bool IsGrounded()
    {
        return GetComponent<Rigidbody>().velocity.y == 0;
    }

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
    }

    public void Move()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
            isFalling = false;
        }

        if (movementInput.x != 0.0f || movementInput.y != 0.0f)
        {
            Vector3 direction = transform.forward * movementInput.y + transform.right * movementInput.x;
            _rb.MovePosition(transform.position + direction * playerSpeed * Time.deltaTime);
        }
    }

    public void Jump()
    {
        if(isGrounded && !isLanded && !isCrouching)
        {
            isJumping = true;
            _rb.velocity = Vector3.up * _jumpForce * gravityValue;
        }
    }

    public void Aiming()
    {
        isAiming = !isAiming;
    }

    IEnumerator BadEnd()
    {
        _animator.Play("Die");
        yield return new WaitForSeconds(2.5f);
    }

    IEnumerator GoodEnd()
    {
        _animator.Play("Dance");
        yield return new WaitForSeconds(2.5f);
    }
}
