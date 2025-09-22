using UnityEngine;

[System.Serializable]
public class Skill
{
    public string skillName;
    public Sprite icon;
    public float cooldown = 1f;
    public float manaCost = 10f;

    public enum SkillType {  MeleeDamage, MagicDamage, Heal, Buff, Passive }
    public SkillType skillType;

    public float power;

    public float lastUsedTime = -999f;

    public bool CanUse()
    {
        return Time.time >= lastUsedTime + cooldown;
    }

    public void Use(Transform caster, Transform target)
    {
        if (!CanUse()) return;
        Stat casterStat = caster.GetComponent<Stat>();
        Stat targetStat = target.GetComponent<Stat>();
        if (casterStat == null || targetStat == null) return;
        if (casterStat.GetTotalValue(StatType.MP) < manaCost) return;

        casterStat.AddBase(StatType.MP, casterStat.GetTotalValue(StatType.MP) - manaCost);
        lastUsedTime = Time.time;

        switch (skillType)
        {
            case SkillType.MeleeDamage:
                targetStat.TakeMeleeDamage(casterStat.GetTotalValue(StatType.Strength) * power);
                break;
            case SkillType.Heal:
                targetStat.Heal((int)(casterStat.GetTotalValue(StatType.Intelligence) * power));
                break;
            case SkillType.Buff:
                // Implement buff logic here
                break;
            case SkillType.Passive:
                // Implement passive skill logic here
                break;
        }
    }
}
