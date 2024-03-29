using System;
using UnityEngine;

public class ItemPickup : MonoBehaviour, ICollectable
{
    public Item item;
    public static event Action<bool> OnGetKey = delegate { }; //STATIC

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

    }

    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.tag == "Player") Pickup(collision.transform);
    //}

    public void Collected()
    {
        Collected(true);
        Pickup(GameManager.Instance.GetPlayer().transform);
        DataManager.instance.collectibles.Add(gameObject.GetComponent<ItemController>());
    }
    public void Collected(bool f)
    {
        OnGetKey.Invoke(f);
    }
}
