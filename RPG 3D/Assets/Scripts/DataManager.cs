using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

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

    public string GetPersisentPath()
    {
        return Path.Combine(Application.persistentDataPath, "Data");
    }

    public void SaveGame()
    {
        PlayerData playerData = new PlayerData(GameManager.Instance.GetPlayerController());
        string path = GetPersisentPath() + "/save.json";
        System.IO.File.WriteAllText(path, JsonUtility.ToJson(playerData));
        Debug.Log(playerData);
    }

    public void LoadGame()
    {
        //string path = GetPersisentPath() + "/save.json";
        //string playerData = System.IO.File.ReadAllText(path);

        //PlayerController controller = GameManager.Instance.GetPlayer().GetComponent<PlayerController>();
        //controller = JsonUtility.FromJson<PlayerController>(playerData);
    }

    public bool SaveExist()
    {
        string path = GetPersisentPath() + "/save.json";
        return File.Exists(path);
    }
}
