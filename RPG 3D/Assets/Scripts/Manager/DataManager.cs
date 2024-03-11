using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public GameObject player;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public string GetPersisentPath()
    {
        return Path.Combine(Application.persistentDataPath, "Data");
    }

    public void SaveGame()
    {
        CreatePersistent();
        PlayerData playerData = new PlayerData(GameManager.Instance.GetPlayerController());
        string path = GetPersisentPath() + "/save.json";
        File.WriteAllText(path, JsonUtility.ToJson(playerData));
        StartCoroutine(CanvasManager.Instance.SavingGame());
    }

    public void LoadGame()
    {
        string path = GetPersisentPath() + "/save.json";
        string playerData = File.ReadAllText(path);
        PlayerData data = JsonUtility.FromJson<PlayerData>(playerData);

        player = GameManager.Instance.GetPlayer();

        // Change the values from the player
        player.transform.position = data.position;
        PlayerController controller = player.GetComponent<PlayerController>();
        controller.isCrouching = data.isCrouching;
        GameManager.Instance.loadedBrainCount = data.brainCount;

    }

    public bool SaveExist()
    {
        return File.Exists(GetPersisentPath() + "/save.json");
    }

    public void CreatePersistent()
    {
        string sourcePath = Path.Combine(Application.streamingAssetsPath, "Data");
        string targetPath = GetPersisentPath();

        if (!Directory.Exists(Application.persistentDataPath)) { Directory.CreateDirectory(Application.persistentDataPath); }
        if (!Directory.Exists(targetPath))
        {
            Directory.CreateDirectory(targetPath);
            string[] filesS = Directory.GetFiles(sourcePath);

            foreach (string file in filesS)
            {
                string targetFile = Path.Combine(targetPath, Path.GetFileName(file));
                File.Copy(file, targetFile);
            }
        }

        string[] files = Directory.GetFiles(sourcePath);
        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file);
            string targetFile = Path.Combine(targetPath, fileName);

            if (!File.Exists(targetFile)) { File.Copy(file, targetFile); }
        }
    }
}
