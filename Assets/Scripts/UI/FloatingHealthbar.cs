using UnityEngine;

public class FloatingHealthbar : MonoBehaviour
{
    public Character character;          // 체력 바 달 캐릭터
    public Transform barForeground;      // 빨간색 체력 바
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

        // 체력 비율 안전하게 계산
        float maxHP = Mathf.Max(character.stat.GetTotalValue(StatType.MaxHP), 1); // 0 방지
        float hpPercent = Mathf.Clamp01((float)character.stat.GetTotalValue(StatType.HP) / maxHP);

        Vector3 scale = barForeground.localScale;
        scale.x = initialScaleX * hpPercent;
        barForeground.localScale = scale;

        // 캐릭터 머리 위 위치
        transform.position = character.transform.position + offset;
    }
}