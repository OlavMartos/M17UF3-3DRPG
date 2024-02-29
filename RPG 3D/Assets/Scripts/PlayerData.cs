using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
    public Vector3 position;
    public float[] rotation;
    public float speed;

    public PlayerData(PlayerController controller)
    {
        position = controller.transform.position;

        rotation = new float[3];
        rotation[0] = controller.transform.rotation.x;
        rotation[1] = controller.transform.rotation.y;
        rotation[2] = controller.transform.rotation.z;

        speed = controller.playerSpeed;
    }
}
