using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

/// <summary>
/// ����Ŀ������ö��
/// </summary>
public enum ETaskTargetType
{
    [LabelText("��")]
    None = 0,
    [LabelText("ȥĳ������")]
    Position = 1 << 0,
    [LabelText("ȥĳ������")]
    Scene = 1 << 1,
    [LabelText("��NPC�Ի�")]
    Talk2Npc = 1 << 2,
    [LabelText("ɱ��")]
    KillMonster = 1 << 3,
    
}

/// <summary>
/// ����Ŀ�����ݽṹ
/// </summary>
public struct STaskTarget
{
    /// <summary>
    /// Ŀ������
    /// </summary>
    public ETaskTargetType type;

}