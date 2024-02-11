using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Private Variables
    private Animator _animator;
    private CharacterController _controller;
    private Vector3 velocity;

    [Header("Test Bools")]
    public bool isDead;
    public bool isVictory;
    public bool isGrounded;
    public bool isLanded;
    public bool isCrouching;
    public bool isJumping;
    public bool isAiming;
    public bool isFalling;

    [Header("Stadistics")]
    public float playerSpeed;
    public float jumpHeight = 0.5f;
    public float gravityValue = -9.81f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
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
        isGrounded = _controller.isGrounded;
        if (isDead && !isVictory) StartCoroutine(BadEnd());
        if (isVictory && !isDead) StartCoroutine(GoodEnd());

        Move();
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
