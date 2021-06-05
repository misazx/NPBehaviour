using ETModel;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using Plugins.NodeEditor.Editor.Canvas;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Plugins
{
    [Node(false, "任务目标数据/t杀怪", typeof(NPBehaveCanvas))]
    public class KillMonsterTaskTargetNode : TaskTargetNodeBase
    {
        public override string GetID => Id;

        public const string Id = "t杀怪";

        public TaskTargetData Data =
                new TaskTargetData()
                {
                    Des = "t杀怪",
                    BaseData = new KillMonsterTaskTargetData() { type = ETaskTargetType.KillMonster }
                };


        public override void NodeGUI()
        {
            EditorGUILayout.TextField(Data?.Des);
        }

        public override NodeDataBase Target_GetNodeData()
        {
            return this.Data;
        }
    }
}
