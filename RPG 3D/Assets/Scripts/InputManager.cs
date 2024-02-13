using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

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

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(gameObject);
        else _instance = this;
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Game.Jump.started += _ => PlayerJump.Invoke();
        playerControls.Game.Aim.performed += _ => PlayerAim.Invoke();
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Game.Jump.started -= _ => PlayerJump.Invoke();
        playerControls.Game.Aim.performed -= _ => PlayerAim.Invoke();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.Game.Move.ReadValue<Vector2>();
    }
}
