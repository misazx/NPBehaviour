using ETModel;
using NodeEditorFramework;
using Plugins.NodeEditor.Editor.Canvas;
using UnityEditor;
using UnityEngine;

namespace Plugins
{
    [Node(false, "����Ŀ�����ݽ��", typeof(NeverbeUsedCanvas))]
    public class TaskTargetNodeBase : Node
    {
        private const string Id = "����Ŀ�����ݽ��";

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
            EditorGUILayout.TextField("������ʹ�ô˽��");
        }

        public virtual NodeDataBase Target_GetNodeData()
        {
            return null;
        }
    }
}

