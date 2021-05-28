//------------------------------------------------------------
// Author: �������������
// Mail: 1778139321@qq.com
// Data: 2019��8��21�� 8:18:19
//------------------------------------------------------------

using System.Collections.Generic;
using ETModel;
using NodeEditorFramework;
using Plugins.NodeEditor.Editor.Canvas;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Node = NPBehave.Node;

namespace Plugins.NodeEditor.Editor.NPBehaveNodes
{
    [Node(false, "NPBehave��Ϊ��/Task/Log", typeof(NPBehaveCanvas))]
    public class NP_LogActionNode : NP_TaskNodeBase
    {
        /// <summary>
        /// �ڲ�ID
        /// </summary>
        private const string Id = "��Ϊ�ڵ�";

        /// <summary>
        /// �ڲ�ID
        /// </summary>
        public override string GetID => Id;

        public NP_ActionNodeData NP_ActionNodeData =
                new NP_ActionNodeData() { NodeType = NodeType.Task, NpClassForStoreAction = new NP_LogAction() };

        public override NP_NodeDataBase NP_GetNodeData()
        {
            return NP_ActionNodeData;
        }

        public override void NodeGUI()
        {
            NP_ActionNodeData.NodeDes = EditorGUILayout.TextField(NP_ActionNodeData.NodeDes);
        }
    }
}