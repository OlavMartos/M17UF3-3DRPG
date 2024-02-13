using System.Collections;
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
    private CharacterController _controller;
    private Vector3 velocity;

    // Input variables
    private InputManager inputManager;
    private Vector2 input;

    [Header("Test Bools")] // Posteriormente sustituir por [HideInInspector]
    [SerializeField] private bool isDead;
    [SerializeField] private bool isVictory;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isLanded;
    [SerializeField] private bool isCrouching;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isAiming;
    [SerializeField] private bool isFalling;
    [SerializeField] private bool isWalking;

    [Header("Stadistics")]
    public float playerSpeed;
    public float jumpHeight = 0.5f;
    public float gravityValue = -9.81f;

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(gameObject);
        else _instance = this;

        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
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
        input = inputManager.GetPlayerMovement();
        CheckIfIsMoving();
        isGrounded = _controller.isGrounded;
        
        if (isDead && !isVictory) StartCoroutine(BadEnd());
        if (isVictory && !isDead) StartCoroutine(GoodEnd());

        Move();
    }

    private void CheckIfIsMoving()
    {
        if (!(input.x != 0 || input.y != 0)) StartCoroutine(WaitForBoolToChange());
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
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
            isJumping = false;
            isFalling = false;
        }

        if(_controller.velocity.y < 0f && !isGrounded) isFalling = true;
    }

    public void Jump()
    {
        if (isGrounded && _controller.enabled == true && !isLanded && !isCrouching)
        {
            isJumping = true;
            velocity.y += Mathf.Sqrt(jumpHeight * -3f * gravityValue);
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
