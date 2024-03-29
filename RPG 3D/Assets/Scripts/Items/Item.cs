using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
    public ItemType itemType;

    public enum ItemType
    {
        AncientKey,
        MagicKey,
        NormalKey
    }
}
