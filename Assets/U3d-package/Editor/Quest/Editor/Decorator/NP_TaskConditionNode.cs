using ETModel;
using NodeEditorFramework;
using Plugins.NodeEditor.Editor.Canvas;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Plugins.NodeEditor.Editor.NPBehaveNodes
{
    [Node(false, "Task��Ϊ��/Task Decorator/TaskCondition", typeof(NPBehaveCanvas))]
    public class NP_TaskConditionNode : NP_DecoratorNodeBase
    {
        /// <summary>
        /// �ڲ�ID
        /// </summary>
        private const string Id = "����TaskCondition���";

        /// <summary>
        /// �ڲ�ID
        /// </summary>
        public override string GetID => Id;

        public NP_TaskConditionNodeData NP_BlackboardConditionNodeData =
                new NP_TaskConditionNodeData { NodeType = NodeType.Decorator, NodeDes = "����TaskCondition���" };

        public override NP_NodeDataBase NP_GetNodeData()
        {
            return NP_BlackboardConditionNodeData;
        }

        public override void NodeGUI()
        {
            NP_BlackboardConditionNodeData.NodeDes = EditorGUILayout.TextField(NP_BlackboardConditionNodeData.NodeDes);
        }
    }
}