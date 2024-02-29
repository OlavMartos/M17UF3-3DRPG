using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Singleton
    private static InputManager _instance;
    public static InputManager Instance
    {
        get { return _instance; }
    }

    // New Input System
    private PlayerControls playerControls;

    // Jump
    public delegate void OnPlayerJump();
    public static event OnPlayerJump PlayerJump;

    // Aim
    public delegate void OnPlayerAim();
    public static event OnPlayerAim PlayerAim;

    // Crouch
    public delegate void OnPlayerCrouch();
    public static event OnPlayerCrouch PlayerCrouch;


    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(gameObject);
        else _instance = this;
        playerControls = new PlayerControls();
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        playerControls.Game.Jump.started += _ => PlayerJump.Invoke();
        playerControls.Game.Aim.performed += _ => PlayerAim.Invoke();
        playerControls.Game.Crouch.performed += _ => PlayerCrouch.Invoke();
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Game.Jump.started -= _ => PlayerJump.Invoke();
        playerControls.Game.Aim.performed -= _ => PlayerAim.Invoke();
        playerControls.Game.Crouch.performed -= _ => PlayerCrouch.Invoke();
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.Game.Move.ReadValue<Vector2>();
    }

    public float GetPlayerIsRunning()
    {
        return playerControls.Game.Run.ReadValue<float>();
    }

    public void DisableControls()
    {
        playerControls.Game.Disable();
    }

    public bool GetCurrentControlsStatus()
    {
        return playerControls.Game.enabled;
    }

    public float IsInteractive()
    {
        return playerControls.Game.Interact.ReadValue<float>();
    }
}
