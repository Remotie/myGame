using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public List<Item> items = new List<Item>();

    public int maxSlot = 20;

    public void AddItem(Item newItem)
    {
        if (newItem.isStackable)
        {
            Item existingItem = items.Find(item => item.itemName == newItem.itemName);
            if (existingItem != null)
            {
                existingItem.amount = Mathf.Min(existingItem.amount + newItem.amount, existingItem.maxStack);
                return;
            }
        }
        items.Add(newItem);
    }

    public void RemoveItem(Item item, int amount)
    {
        if (items.Contains(item))
        {
            item.amount -= amount;
            if (item.amount <= 0)
            {
                items.Remove(item);
            }
        }
    }

    public void UseItem(int idx, Transform target)
    {
        if (idx < 0 || idx >= items.Count) return;
        Item item = items[idx];
        if (item != null)
        {
            item.Use(target);
            RemoveItem(item, 1);
        }
    }
}