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
        Character c = caster.GetComponent<Character>();
        Character t = target.GetComponent<Character>();

        if (c == null || t == null) return;
        if (c.stat.GetTotalValue(StatType.MP) < manaCost) return;

        c.stat.AddBase(StatType.MP, c.stat.GetTotalValue(StatType.MP) - manaCost);
        lastUsedTime = Time.time;

        switch (skillType)
        {
            case SkillType.MeleeDamage:
                t.TakeMeleeDamage(c.stat.GetTotalValue(StatType.Strength) * power);
                break;
            case SkillType.Heal:
                //t.Heal((int)(c.stat.GetTotalValue(StatType.Intelligence) * power));
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
