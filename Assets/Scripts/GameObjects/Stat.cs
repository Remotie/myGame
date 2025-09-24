using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum StatType 
{
    Strength,
    Agility,
    Intelligence,
    Defense,
    Resist,         // %
    MagicResist,    // %
    MoveSpeed,
    CriticalRate,
    CriticalDamage, // %
    HP,
    MP,
    MaxHP,
    MaxMP,


    // 필요시 확장
}

public class StatValue
{ 
    public StatType Type { get; private set; }
    public float baseValue;
    public float bonusValue;
    public float buffValue;

    public float BaseValue { get => baseValue; 
        set
        {
            if (baseValue != value)
            {
                baseValue = value;
                onValueChanged?.Invoke();
            }
        } 
    }
    public float BonusValue { get => bonusValue; 
        set
        {
            if (bonusValue != value)
            {
                bonusValue = value;
                onValueChanged?.Invoke();
            }
        }
    }

    public float BuffValue { get => buffValue; 
        set
        {
            if (buffValue != value)
            {
                buffValue = value;
                onValueChanged?.Invoke();
            }
        }
    }

    public float TotalValue => BaseValue + BonusValue + BuffValue;
    public Action onValueChanged;

    public StatValue(StatType type, float baseValue = 1000f)
    {
        Type = type;
        BaseValue = baseValue;
        BonusValue = 0;
        BuffValue = 0;
    }

    public void AddBase(float value) => BaseValue += value;
    public void AddBonus(float value) => BonusValue += value;
    public void AddBuff(float value) => BuffValue += value;

    public void ClearBonus() => BonusValue = 0;
    public void ClearBuff() => BuffValue = 0;
}

[System.Serializable]
public class StatEntry
{
    public string key;
    public float baseValue;
    public float bonusValue;
    public float buffValue;
    public float totalValue;
}

//[Serializable]
//public class StatDictionary : SerializedDictionary<StatType, StatValue> { }

[System.Serializable]
public class Stat
{
    [SerializeField]
    private List<StatEntry> statList = new List<StatEntry>();

    //[SerializeField]
    //private StatDictionary stats = new();

    public Dictionary<StatType, StatValue> stats = new();

    public event Action<StatType, float> OnStatChanged;

    public Stat()
    {
    }

    public void init()
    {
        StatType[] types = (StatType[])Enum.GetValues(typeof(StatType));
        foreach (var type in types)
        {
            stats[type] = new StatValue(type);
            Debug.Log(stats[type].baseValue);
            statList.Add(new StatEntry
            {
                key = type.ToString(),
                baseValue = stats[type].baseValue,
                bonusValue = stats[type].BonusValue,
                buffValue = stats[type].BuffValue,
                totalValue = stats[type].TotalValue,
            });
        }

        foreach (var stat in stats.Values)
        {
            stat.onValueChanged += () => OnStatChanged?.Invoke(stat.Type, stat.TotalValue);
            stat.onValueChanged += SyncStatList;
        }
    }

    public void SyncStatList()
    {
        Debug.Log("SyncStatList called");
        StatType[] types = (StatType[])Enum.GetValues(typeof(StatType));

        foreach (var type in types)
        {
            var stat = stats[type];
            var entry = statList.Find(e => e.key == type.ToString());
            if (entry != null)
            {
                entry.baseValue = stat.BaseValue;
                entry.bonusValue = stat.BonusValue;
                entry.buffValue = stat.BuffValue;
                entry.totalValue = stat.TotalValue;
            }
        }
    }

    public StatValue GetStat(StatType type) {
        if (!stats.ContainsKey(type)) {
            Debug.LogError($"Stat {type} not initialized.");
            stats[type] = new StatValue(type);
            return stats[type];
        }

        return stats[type];
    }

    public float GetTotalValue(StatType type) => stats[type].TotalValue;

    public void AddBase(StatType type, float value) => stats[type].AddBase(value);
    public void AddBonus(StatType type, float value) => stats[type].AddBonus(value);
    public void AddBuff(StatType type, float value) => stats[type].AddBuff(value);

    public void ResetBonus()
    {
        foreach (var s in stats.Values) s.ClearBonus();
    }

    public void ResetBuff()
    {
        foreach (var s in stats.Values) s.ClearBuff();
    }

    public void Heal(float amount)
    {
        float maxHP = GetTotalValue(StatType.MaxHP);
        float currentHP = GetTotalValue(StatType.HP);
        currentHP = Mathf.Min(currentHP + amount, maxHP);
        stats[StatType.HP] = new StatValue(StatType.HP, currentHP);
    }
}
