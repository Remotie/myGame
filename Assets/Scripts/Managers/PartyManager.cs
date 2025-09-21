using System;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public PlayerController[] partyMembers;
    public int selectedIndex = 0;
    public Action OnPartyChanged;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetControlledCharacter(0);
        OnPartyChanged?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetControlledCharacter(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetControlledCharacter(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetControlledCharacter(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SetControlledCharacter(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SetControlledCharacter(4);

        UpdateControl();
    }

    void SetControlledCharacter(int index)
    {
        for (int i = 0; i < partyMembers.Length; i++)
        {
            partyMembers[i].isPlayerControlled = (i == index);
        }
        selectedIndex = index;
    }

    void UpdateControl()
    {
        for (int i = 0; i < partyMembers.Length; i++)
        {
            bool isPlayer = (i == selectedIndex);

            AIController ai = partyMembers[i].GetComponent<AIController>();
            if (ai != null)
            {
                ai.followTarget = isPlayer ? null : partyMembers[selectedIndex].transform;
            }
        }

        CameraManager.Instance.SetFocus(partyMembers[selectedIndex].transform);
    }

    public void FocusMember(int idx)
    {
        selectedIndex = idx;
        SetControlledCharacter(selectedIndex);
        OnPartyChanged?.Invoke();
    }

    public void AddMember(Transform member)
    {
        OnPartyChanged?.Invoke();
    }

    public void RemoveMember(int idx)
    {
        if (idx < 0 || idx >= partyMembers.Length) return;
        PlayerController[] newParty = new PlayerController[partyMembers.Length - 1];
        int newIdx = 0;
        for (int i = 0; i < partyMembers.Length; i++)
        {
            if (i == idx) continue;
            newParty[newIdx] = partyMembers[i];
            newIdx++;
        }
        partyMembers = newParty;
        if (selectedIndex >= partyMembers.Length)
        {
            selectedIndex = partyMembers.Length - 1;
        }
        SetControlledCharacter(selectedIndex);
        OnPartyChanged?.Invoke();
    }


}