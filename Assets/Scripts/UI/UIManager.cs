using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public PartyManager partyManager;
    
    public GameObject partyLayout;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        partyManager.OnPartyChanged += ShowParty;
    }

    private void OnDisable()
    {
        partyManager.OnPartyChanged -= ShowParty;
    }

    void ShowParty()
    {

        Debug.Log("Party member counts : " + partyManager.partyMembers.Length);
        for(int i=0; i < partyManager.partyMembers.Length; i++)
        {
            int idx = i;
            GameObject memberUI = partyLayout.transform.GetChild(idx).gameObject;
            memberUI.SetActive(true);
            Debug.Log("member : " + idx);
            

            Image img = memberUI.GetComponent<Image>();

            Stat memberStat = partyManager.partyMembers[idx].GetComponent<Character>().stat;
            //float currentHP = memberStat.GetTotalValue(StatType.HP);
            //float maxHP = memberStat.GetTotalValue(StatType.MaxHP);

            Image healthBar = memberUI.transform.GetChild(1).GetComponent<Image>();
            //healthBar.fillAmount = currentHP / maxHP;

            if (partyManager.selectedIndex == i)
            {
                partyManager.partyMembers[idx].name = partyManager.partyMembers[idx].name + " (Selected)";
                img.color = Color.yellow;
            } else
            {
                img.color = Color.white;
            }
            
            Button btn = memberUI.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() =>
            {
                partyManager.FocusMember(idx);
            });
        }
    }
}
