using ETModel;
using NodeEditorFramework;
using Plugins.NodeEditor.Editor.Canvas;
using UnityEditor;
using UnityEngine;

namespace Plugins
{
    [Node(false, "任务目标数据结点", typeof(NeverbeUsedCanvas))]
    public class TaskTargetNodeBase : Node
    {
        private const string Id = "任务目标数据结点";

        public override string GetID => Id;

        public override Vector2 DefaultSize => new Vector2(150, 60);

        [ValueConnectionKnob("PrevType", Direction.In, "PrevNodeDatas", NodeSide.Left, 30, MaxConnectionCount = ConnectionCount.Multi)]
        public ValueConnectionKnob PrevNode;

        [ValueConnectionKnob("NextType", Direction.Out, "NextNodeDatas", NodeSide.Right, 30)]
        public ValueConnectionKnob NextNode;

        public virtual void AutoAddLinkedTargets()
        {

        }

        public override void NodeGUI()
        {
            EditorGUILayout.TextField("不允许使用此结点");
        }

        public virtual NodeDataBase Target_GetNodeData()
        {
            return null;
        }
    }
}

