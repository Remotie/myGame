using UnityEngine;

public class Territory : MonoBehaviour
{
    [Header("기본 정보")]
    public string territoryName;
    public Guild ownerGuild;          // 점령 길드
    [Range(0, 100)]
    public int taxRate = 10;          // 사냥 시 세금 비율(%)

    [Header("사냥 관련")]
    public GameObject[] monsters;     // 이 영지에서 출현 가능한 몬스터
    public int baseGoldReward = 100;  // 사냥 시 기본 보상

    /// <summary>
    /// 사냥 후 세금 계산
    /// </summary>
    /// <param name="goldEarned">사냥 보상</param>
    /// <param name="killer">사냥한 캐릭터/길드</param>
    public void CollectTax(int goldEarned, Character killer)
    {
        if (ownerGuild != null)
        {
            int tax = Mathf.RoundToInt(goldEarned * (taxRate / 100f));
            ownerGuild.ReceiveTax(tax);
            Debug.Log($"{ownerGuild.guildName} collected {tax} gold from {killer.name} in {territoryName}");
        }
    }

    /// <summary>
    /// 영지 점령
    /// </summary>
    public void ChangeOwner(Guild newOwner)
    {
        if (ownerGuild != null)
        {
            ownerGuild.RemoveTerritory(this);
        }

        ownerGuild = newOwner;

        if (newOwner != null)
        {
            newOwner.AddTerritory(this);
            Debug.Log($"{territoryName} is now controlled by {newOwner.guildName}");
        }
    }
}