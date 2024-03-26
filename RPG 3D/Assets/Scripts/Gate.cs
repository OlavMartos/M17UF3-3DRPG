using UnityEngine;

public class Gate : MonoBehaviour
{
    public int KeysCollected;
    public static Gate Instance;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;

        ItemPickup.OnGetKey += AddKey;
    }

    //private void OnEnable()
    //{
    //}

    private void OnDisable()
    {
        ItemPickup.OnGetKey -= AddKey;
    }

    public void AddKey(bool f)
    {
        KeysCollected++;
        if(KeysCollected == 3)
        {
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
