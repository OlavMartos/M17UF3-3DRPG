using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (InputManager.Instance.IsInteracting() > 0)
            {
                DataManager.instance.SaveGame();
            }
        }  
    }

}
