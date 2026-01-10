using UnityEngine;

public enum ItemType { Usable, Passive }

public abstract class Item : ScriptableObject
{
    public string itemName;
    [TextArea] public string itemDescription;
    public Sprite icon;
    public ItemType type;
}
