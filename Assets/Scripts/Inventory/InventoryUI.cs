using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] Image[] usableItemSlots;
    [SerializeField] Image[] pasiveItemSlots;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemDescription;

    ItemType type = ItemType.Usable;
    private bool isUsableOn=true;

    void OnEnable()
    {
        inventory.OnChanged += RefreshUI;
        RefreshUI();
    }

    void OnDisable()
    {
        inventory.OnChanged -= RefreshUI;
    }

    void RefreshUI()
    {
        Debug.Log("Inventario cambió → refrescando UI");

        for (int i = 0; i < usableItemSlots.Length; i++)
        {
            if (i < inventory.Usables.Count && inventory.Usables[i] != null)
            {
                usableItemSlots[i].enabled = true;
                usableItemSlots[i].sprite = inventory.Usables[i].icon;
            }
            else
                usableItemSlots[i].enabled = false;
        }

        for (int i = 0; i < pasiveItemSlots.Length; i++)
        {
            if (i < inventory.Passives.Count && inventory.Passives[i] != null)
            {
                pasiveItemSlots[i].enabled = true;
                pasiveItemSlots[i].sprite = inventory.Passives[i].icon;
            }
            else
                pasiveItemSlots[i].enabled = false;
        }        
    }

    public void SelectSlot(int index)   
    {
        print("seleccionamos " + index);
        itemDescription.text = inventory.GetItemDescription(index, type);
        itemName.text = inventory.GetItemName(index, type);
        inventory.Select(type, index);
    }

    public void OnTabChange()
    {
        if (isUsableOn)
        {
            type = ItemType.Passive;
            isUsableOn = false;
        }
        else
        {
            type = ItemType.Usable;
            isUsableOn = true;
        }

    }
}
