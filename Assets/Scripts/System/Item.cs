

using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;
    public int amount = 1;
    public int maxStack = 99;
    public bool isStackable = true;

    public enum ItemType {
        Consumable,
        Equipment,
        Quest,
        Miscellaneous
    }
    public ItemType itemType;

    public void Use(Transform target)
    {
        Stat targetStat = target.GetComponent<Stat>();
        if (targetStat != null) return;

        if (itemType == ItemType.Consumable)
        {
            if (itemName == "Health Potion")
            {
                targetStat.Heal(50);
                amount--;
            }
        }
    }
}