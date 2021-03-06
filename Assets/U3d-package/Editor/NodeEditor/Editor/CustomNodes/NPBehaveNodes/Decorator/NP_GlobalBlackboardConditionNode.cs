//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年8月23日 17:54:50
//------------------------------------------------------------

using ETModel;
using NodeEditorFramework;
using Plugins.NodeEditor.Editor.Canvas;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Plugins.NodeEditor.Editor.NPBehaveNodes
{
    [Node(false, "NPBehave行为树/Decorator/GlobalBlackboardCondition", typeof(NPBehaveCanvas))]
    public class NP_GlobalBlackboardConditionNode : NP_DecoratorNodeBase
    {
        /// <summary>
        /// 内部ID
        /// </summary>
        private const string Id = "共享黑板条件结点";

        /// <summary>
        /// 内部ID
        /// </summary>
        public override string GetID => Id;

        public NP_GlobalBlackboardConditionNodeData NP_BlackboardConditionNodeData =
                new NP_GlobalBlackboardConditionNodeData { NodeType = NodeType.Decorator, NodeDes = "共享黑板条件结点" };

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