using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerController cont;


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

    private void Update()
    {

        cont = GetPlayerController();
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
