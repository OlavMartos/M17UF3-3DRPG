using UnityEngine;

public class Gate : MonoBehaviour
{
    public int KeysCollected;

    private void OnEnable()
    {
        ItemPickup.OnGetKey += AddKey;
    }

    private void OnDisable()
    {
        ItemPickup.OnGetKey -= AddKey;
    }

    public void AddKey(bool f)
    {
        KeysCollected++;
        if(KeysCollected == 3)
        {
            DataManager.instance.player.GetComponent<PlayerController>().isVictory = true;
        }
    }
}
