using UnityEngine;

[System.Serializable]
public class Skill
{
    public string skillName;
    public Sprite icon;
    public float cooldown = 1f;
    public float manaCost = 10f;

    public enum SkillType {  Damage, Heal, Buff, Passive }
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
        if (casterStat.currentMP < manaCost) return;

        casterStat.currentMP -= manaCost;
        lastUsedTime = Time.time;

        switch (skillType)
        {
            case SkillType.Damage:
                targetStat.TakeDamage(casterStat.attack * power);
                break;
            case SkillType.Heal:
                targetStat.Heal((int)(casterStat.attack * power));
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
