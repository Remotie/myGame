using System;
using System.Collections.Generic;
using UnityEngine;

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

    public StatValue(StatType type, float baseValue = 0f)
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
    public float value;
}

[System.Serializable]
public class Stat
{
    [SerializeField] 
    private List<StatEntry> statList = new List<StatEntry>();

    private Dictionary<StatType, StatValue> stats = new();
    public event Action<StatType, float> OnStatChanged;

    public Stat()
    {
        StatType[] types = (StatType[])Enum.GetValues(typeof(StatType));

        Debug.Log("Stat initialized.");
        foreach (var type in types)
        {
            stats[type] = new StatValue(type);
            statList.Add(new StatEntry { key = type.ToString(), value = stats[type].TotalValue });
        }

        foreach (var stat in stats.Values)
            stat.onValueChanged += () => OnStatChanged?.Invoke(stat.Type, stat.TotalValue);
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

    public void TakeMeleeDamage(float damage)
    {
        float defense = GetTotalValue(StatType.Defense);
        float finalDamage = damage * (100 / (100 + defense));
    }

    public void Heal(float amount)
    {
        float maxHP = GetTotalValue(StatType.MaxHP);
        float currentHP = GetTotalValue(StatType.HP);
        currentHP = Mathf.Min(currentHP + amount, maxHP);
        stats[StatType.HP] = new StatValue(StatType.HP, currentHP);
    }
}
