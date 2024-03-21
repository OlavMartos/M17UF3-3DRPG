using UnityEngine;
using UnityEngine.Tilemaps;

public class Gate : MonoBehaviour
{
    public int KeysCollected;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

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
            //DataManager.instance.player.GetComponent<PlayerController>().isVictory = true;
            _animator.Play("OpenAnim");
            DisableFirstCollider();
        }
    }

    private void DisableFirstCollider()
    {
        BoxCollider[] colliders = GetComponents<BoxCollider>();
        colliders[0].enabled = false;

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController player) && KeysCollected < 3)
        {
            CanvasManager.Instance.VictoryStatus(KeysCollected);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CanvasManager.Instance.keysCount.SetActive(false);
    }
}
