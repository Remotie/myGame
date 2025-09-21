using UnityEngine;

public class Stat : MonoBehaviour
{
    public float maxHP = 100f;
    public float currentHP = 100f;

    public float maxMP = 100f;
    public float currentMP = 100f;

    public float attack = 10f;
    public float defense = 5f;

    public float speed = 3f;

    public bool isAlive => currentHP <= 0;

    public void TakeDamage(float amount)
    {
        float damage = Mathf.Max(amount - defense, 0);
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

}
