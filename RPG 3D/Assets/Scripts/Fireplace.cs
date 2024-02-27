using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (InputManager.Instance.IsInteractive() > 0)
            {
                
            }
        }  
    }

}
