using TMPro;
using UnityEngine;
using Cinemachine;

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
            StartCoroutine(CanvasManager.Instance.SaveLoadedText());
        }
        else
        {
            StartCoroutine(CanvasManager.Instance.NewGameText());
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
