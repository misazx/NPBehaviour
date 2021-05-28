using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

/// <summary>
/// 任务目标类型枚举
/// </summary>
public enum ETaskTargetType
{
    [LabelText("无")]
    None = 0,
    [LabelText("去某个坐标")]
    Position = 1 << 0,
    [LabelText("去某个场景")]
    Scene = 1 << 1,
    [LabelText("和NPC对话")]
    Talk2Npc = 1 << 2,
    [LabelText("杀怪")]
    KillMonster = 1 << 3,
    
}

/// <summary>
/// 任务目标数据结构
/// </summary>
public struct STaskTarget
{
    /// <summary>
    /// 目标类型
    /// </summary>
    public ETaskTargetType type;

}