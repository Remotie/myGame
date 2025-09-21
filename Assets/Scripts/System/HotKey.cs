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
                Debug.Log("빈 슬롯");
                break;

        }
        
    }

    private void UseSkill(int skillId)
    {
        Debug.Log($"스킬 {skillId} 사용");
        // 스킬 사용 로직 구현
    }

    private void UseItem(int itemId)
    {
        Debug.Log($"아이템 {itemId} 사용");
        // 아이템 사용 로직 구현
    }

    private void ExecuteCommand(int commandId)
    {
        Debug.Log($"명령 {commandId} 실행");
        // 명령 실행 로직 구현
    }
}
