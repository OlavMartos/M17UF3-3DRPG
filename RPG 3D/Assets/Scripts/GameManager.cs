using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
        DontDestroyOnLoad(gameObject);

        if (DataManager.instance.SaveExist())
        {
            DataManager.instance.LoadGame();
        }
    }

    public GameObject GetPlayer()
    {
        return GameObject.Find("Zombie Idle");
    }

    public PlayerController GetPlayerController()
    {
        return PlayerController.Instance;
    }
}
