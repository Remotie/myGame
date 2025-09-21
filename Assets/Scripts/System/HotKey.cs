using UnityEngine;

public enum HotkeyActionType
{
    None,
    Skill,
    Item,
    Command
}

public class HotKeySlot
{
    public KeyCode key { get; set; }
    public HotkeyActionType ActionType { get; set; }
    public int ActionId { get; set; }

    public void Execute()
    {
        switch (ActionType) {
            case HotkeyActionType.Skill:
                UseSkill(ActionId);
                break;

            case HotkeyActionType.Item:
                UseItem(ActionId);
                break;

            case HotkeyActionType.Command:
                ExecuteCommand(ActionId);
                break;

            default:
                Debug.Log("�� ����");
                break;

        }
        
    }

    private void UseSkill(int skillId)
    {
        Debug.Log($"��ų {skillId} ���");
        // ��ų ��� ���� ����
    }

    private void UseItem(int itemId)
    {
        Debug.Log($"������ {itemId} ���");
        // ������ ��� ���� ����
    }

    private void ExecuteCommand(int commandId)
    {
        Debug.Log($"��� {commandId} ����");
        // ��� ���� ���� ����
    }
}
