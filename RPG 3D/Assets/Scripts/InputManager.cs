using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private PlayerControls playerControls;

    // Jump
    public delegate void OnPlayerJump();
    public static event OnPlayerJump PlayerJump;

    // Aim
    public delegate void OnPlayerAim();
    public static event OnPlayerAim PlayerAim;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
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
}
