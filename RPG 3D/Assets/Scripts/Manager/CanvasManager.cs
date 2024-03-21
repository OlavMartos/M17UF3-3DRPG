using System.Collections;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    [Header("PersistentInfo")]
    public GameObject newGame;
    public GameObject saveLoaded;
    public GameObject savingGame;
    public GameObject persistentCanvas;

    [Header("Minimap")]
    public GameObject minimap;

    [Header("Interact")]
    public GameObject interact;

    [Header("Victory")]
    public GameObject keysCount;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
        DontDestroyOnLoad(gameObject);

        interact.SetActive(false);
        keysCount.SetActive(false);
        minimap.SetActive(true);
        EnableHUD();
    }

    public void EnableHUD()
    {
        persistentCanvas.SetActive(false);
        newGame.SetActive(false);
        saveLoaded.SetActive(false);
        savingGame.SetActive(false);
    }

    public void EnablePersistent()
    {
        persistentCanvas.SetActive(true);
    }

    public IEnumerator NewGameText()
    {
        EnablePersistent();
        newGame.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        EnableHUD();
    }

    public IEnumerator SaveLoadedText()
    {
        EnablePersistent();
        saveLoaded.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        EnableHUD();
    }

    public IEnumerator SavingGame()
    {
        EnablePersistent();
        savingGame.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        EnableHUD();
    }

    public void VictoryStatus(int count)
    {
        keysCount.SetActive(true);
        if (keysCount.GetComponentInChildren<TextMeshProUGUI>() != null)
        {
            keysCount.GetComponentInChildren<TextMeshProUGUI>().text = $"Keys: {count}/3";
        }
    }
}
