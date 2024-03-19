using System.Drawing;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    public void Pickup(Transform parent)
    {
        transform.parent = parent;
        transform.localPosition = item.position;
        transform.Rotate(item.rotation);
        transform.localScale = item.scale;

        transform.GetComponent<ItemRotation>().rotation = new Vector3(0, 0,0);
        if(item.itemType == Item.ItemType.MagicKey)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        DataManager.instance.collectibles.Add(gameObject.GetComponent<ItemController>());
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player") Pickup(collision.transform);
    }
}
