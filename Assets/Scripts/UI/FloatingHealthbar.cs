using UnityEngine;

public class FloatingHealthbar : MonoBehaviour
{
    public Character character;          // ü�� �� �� ĳ����
    public Transform barForeground;      // ������ ü�� ��
    public Vector3 offset = new Vector3(0, 1.5f, 0);

    private float initialScaleX;

    void Start()
    {
        if (character == null)
            character = GetComponentInParent<Character>();

        if (barForeground != null)
            initialScaleX = barForeground.localScale.x;
    }

    void Update()
    {
        if (character == null || barForeground == null) return;

        // ü�� ���� �����ϰ� ���
        float maxHP = Mathf.Max(character.stat.GetTotalValue(StatType.MaxHP), 1); // 0 ����
        float hpPercent = Mathf.Clamp01((float)character.stat.GetTotalValue(StatType.HP) / maxHP);

        Vector3 scale = barForeground.localScale;
        scale.x = initialScaleX * hpPercent;
        barForeground.localScale = scale;

        // ĳ���� �Ӹ� �� ��ġ
        transform.position = character.transform.position + offset;
    }
}