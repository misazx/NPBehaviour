//------------------------------------------------------------
// Author: �������������
// Mail: 1778139321@qq.com
// Data: 2019��8��23�� 17:54:50
//------------------------------------------------------------

using ETModel;
using NodeEditorFramework;
using Plugins.NodeEditor.Editor.Canvas;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Plugins.NodeEditor.Editor.NPBehaveNodes
{
    [Node(false, "Task��Ϊ��/Task Decorator/TaskExist", typeof(NPBehaveCanvas))]
    public class NP_TaskExistNode : NP_DecoratorNodeBase
    {
        /// <summary>
        /// �ڲ�ID
        /// </summary>
        private const string Id = "�������״̬���";

        /// <summary>
        /// �ڲ�ID
        /// </summary>
        public override string GetID => Id;

        public NP_TaskExistNodeData NP_BlackboardConditionNodeData =
                new NP_TaskExistNodeData { NodeType = NodeType.Decorator, NodeDes = "�������״̬���" };

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