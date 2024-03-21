using UnityEngine;

public class Victory : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.transform.position = new Vector3(3.58106518f, -123.815979f, -1.0176754f);
            player.isVictory = true;
        }
    }
}
