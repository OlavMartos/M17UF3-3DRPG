using Cinemachine;
using UnityEngine;

public class SwapCamera : MonoBehaviour
{
    public static SwapCamera Instance;
    public CinemachineVirtualCamera normalCamera;
    public CinemachineVirtualCamera aimCamera;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
        DontDestroyOnLoad(gameObject);
    }

    public void AimCamera()
    {
        aimCamera.Priority += 10;
    }

    public void NormalCamera()
    {
        aimCamera.Priority -= 10;
    }
}
