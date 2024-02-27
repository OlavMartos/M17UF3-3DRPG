using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    public float[] position;
    public float[] rotation;
    public float speed;

    public PlayerData(PlayerController controller)
    {
        position = new float[3];
        position[0] = controller.transform.position.x;
        position[1] = controller.transform.position.y;
        position[2] = controller.transform.position.z;

        rotation = new float[3];
        rotation[0] = controller.transform.rotation.x;
        rotation[1] = controller.transform.rotation.y;
        rotation[2] = controller.transform.rotation.z;

        speed = controller.playerSpeed;
    }
}
