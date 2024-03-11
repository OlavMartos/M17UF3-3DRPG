using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int BrainCount;
    public int loadedBrainCount;
    [SerializeField] private List<GameObject> playerHUD = new List<GameObject>();


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
            loadedBrainCount = 0;
        }
    }

    private void Start()
    {
        GetHUD();
        BrainCountHUD(loadedBrainCount.ToString());
        BrainCount = loadedBrainCount;
    }

    public void GetHUD()
    {
        GameObject hud = GameObject.Find("HUD");

        GameObject[] ui = new GameObject[hud.transform.childCount];
        for (int i = 0; i < ui.Length; i++) ui[i] = hud.transform.GetChild(i).gameObject;
        for (int i = 0; i < hud.transform.childCount; i++)
        {
            playerHUD.Add(ui[i]);
        }
    }

    public void BrainCountHUD(string count)
    {
        foreach (GameObject go in playerHUD)
        {
            if (go.GetComponentInChildren<TextMeshProUGUI>() != null)
            {
                go.GetComponentInChildren<TextMeshProUGUI>().text = count;
            }
        }
    }
    public void AddBrain()
    {
        BrainCount++;
        BrainCountHUD(BrainCount.ToString());
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
