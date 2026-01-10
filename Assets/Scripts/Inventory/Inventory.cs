using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event System.Action OnChanged;

    private PlayerManager playerManager;

    readonly List<Item> usables = new();
    readonly List<Item> passives = new();

    int selectedUsableIndex = 0;
    int selectedPassiveIndex = 0;

    public IReadOnlyList<Item> Usables => usables;
    public IReadOnlyList<Item> Passives => passives;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    public Item GetSelected(ItemType type)
    {
        List <Item> list = type == ItemType.Usable ? usables : passives;
        if (list.Count == 0) return null;

        int index = type == ItemType.Usable ? selectedUsableIndex : selectedPassiveIndex;
        index = Mathf.Clamp(index, 0, list.Count - 1);
        return list[index];
    }

    public void Select(ItemType type, int index)
    {
        if (type == ItemType.Usable)
        {
            selectedUsableIndex = index;
            EquipSelectedUsable();
        }
        else selectedPassiveIndex = index;
    }

    public void Add(Item item)
    {
        if (item == null) return;

        if (item.type == ItemType.Usable)
        {
            usables.Add(item);
            Debug.Log("Añadido usable "+ item.name);
        }
        else
        {
            passives.Add(item);
            Debug.Log("Añadido pasivo " + item.name);
        }

        // si es pasivo, se registra al momento
        //if (item is PassiveItem p) p.Register(player);

        OnChanged?.Invoke();
    }

    public void Remove(Item item)
    {
        if (item == null) return;

        //if (item is PassiveItem p) p.Unregister(player);

        usables.Remove(item);
        passives.Remove(item);
        OnChanged?.Invoke();
    }

    public string GetItemDescription(int index, ItemType type)
    {
        string description;
        List<Item> list = type == ItemType.Usable ? usables : passives;

        return description = list[index].itemDescription;
    }

    public string GetItemName(int index, ItemType type)
    {
        string name;
        List<Item> list = type == ItemType.Usable ? usables : passives;
        return name = list[index].itemName;
    }

    public void EquipSelectedUsable()
    {
        UsableItem sel = GetSelected(ItemType.Usable) as UsableItem;
        playerManager.Equipment.EquipItem(sel);
    }
}
