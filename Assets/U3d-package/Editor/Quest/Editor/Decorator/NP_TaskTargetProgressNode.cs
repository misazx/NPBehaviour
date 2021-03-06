using ETModel;
using NodeEditorFramework;
using Plugins.NodeEditor.Editor.Canvas;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Plugins.NodeEditor.Editor.NPBehaveNodes
{
    [Node(false, "Task行为树/Task Decorator/TaskTargetProgress", typeof(NPBehaveCanvas))]
    public class NP_TaskTargetProgressNode : NP_DecoratorNodeBase
    {
        /// <summary>
        /// 内部ID
        /// </summary>
        private const string Id = "TaskTargetProgress";

        /// <summary>
        /// 内部ID
        /// </summary>
        public override string GetID => Id;

        public NP_TaskTargetProgressNodeData NP_BlackboardConditionNodeData =
                new NP_TaskTargetProgressNodeData { NodeType = NodeType.Decorator, NodeDes = "任务TaskTargetProgress结点" };

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