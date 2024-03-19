using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public Vector3 position;
    public float[] rotation;
    public float speed;
    public bool isCrouching;
    public int brainCount;
    public List<ItemController> collectibles;

    public PlayerData(PlayerController controller)
    {
        position = controller.transform.position;

        rotation = new float[3];
        rotation[0] = controller.transform.rotation.x;
        rotation[1] = controller.transform.rotation.y;
        rotation[2] = controller.transform.rotation.z;

        speed = controller.playerSpeed;
        isCrouching = controller.isCrouching;
        brainCount = GameManager.Instance.BrainCount;
        collectibles = DataManager.instance.collectibles;
        
    }

    ///
    /// COSAS QUE SE AÑADIERON Y SE DECIDIERON QUITAR:
    /// 1. NormalCameraPriority : Motivo en el punto 2
    /// 2. AimCameraPriority : Al estar con la AimCamera siendo prioridad no se puede guardar partida
    /// 3. IsJumping : No sirve de absolutamente nada por que igualmente se activara el isFalling
    ///
}
