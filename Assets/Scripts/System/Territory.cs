using UnityEngine;

public class Territory : MonoBehaviour
{
    [Header("�⺻ ����")]
    public string territoryName;
    public Guild ownerGuild;          // ���� ���
    [Range(0, 100)]
    public int taxRate = 10;          // ��� �� ���� ����(%)

    [Header("��� ����")]
    public GameObject[] monsters;     // �� �������� ���� ������ ����
    public int baseGoldReward = 100;  // ��� �� �⺻ ����

    /// <summary>
    /// ��� �� ���� ���
    /// </summary>
    /// <param name="goldEarned">��� ����</param>
    /// <param name="killer">����� ĳ����/���</param>
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
    /// ���� ����
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