using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    private GameObject player;
    public bool isCamera;
    private float difference;

    void Start() { player = GameManager.Instance.GetPlayer(); }

    void Update() 
    {
        if (isCamera) difference = 45f;
        else difference = 40f;
        transform.position = new Vector3(player.transform.position.x, /*player.transform.position.y + difference*/ difference, player.transform.position.z);
    }
}
