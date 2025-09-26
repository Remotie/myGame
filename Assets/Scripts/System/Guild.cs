using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Guild
{
    public string guildName;
    public int treasury = 0;
    public List<Territory> controlledTerritories = new List<Territory>();

    public Guild(string name)
    {
        guildName = name;
        treasury = 0;
        controlledTerritories = new List<Territory>();
    }

    public void AddTerritory(Territory territory)
    {
        if (!controlledTerritories.Contains(territory))
        {
            controlledTerritories.Add(territory);
        }
    }

    public void RemoveTerritory(Territory territory)
    {
        if (controlledTerritories.Contains(territory))
        {
            controlledTerritories.Remove(territory);
        }
    }

    public void ReceiveTax(int amount)
    {
        treasury += amount;
        Debug.Log($"{guildName} received {amount} gold. Total treasury: {treasury}");
    }
}