using System.Collections.Generic;
using ETModel;
using NodeEditorFramework;
using NPBehave;
using Plugins.NodeEditor.Editor.Canvas;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Node = NPBehave.Node;

namespace Plugins.NodeEditor.Editor.NPBehaveNodes
{
    [Node(false, "Task行为树/Task根结点", typeof(NPBehaveCanvas))]
    public class NP_Task_RootNode : NP_NodeBase
    {
        /// <summary>
        /// 内部ID
        /// </summary>
        private const string Id = "Task行为树根节点";

        /// <summary>
        /// 内部ID
        /// </summary>
        public override string GetID => Id;

        [ValueConnectionKnob("NPBehave_NextNode", Direction.Out, "NPBehave_NextNodeDatas", NodeSide.Bottom, 75)]
        public ValueConnectionKnob NextNode;

        [BoxGroup("根结点数据")]
        [HideReferenceObjectPicker]
        [HideLabel]
        public NP_RootNodeData MRootNodeData;

        private void OnEnable()
        {
            if (MRootNodeData == null)
            {
                this.MRootNodeData = new NP_RootNodeData { NodeType = NodeType.Decorator };
                backgroundColor = new Color(0, 191 / 255f, 1);
            }
        }

        public override NP_NodeDataBase NP_GetNodeData()
        {
            return this.MRootNodeData;
        }

        public override ValueConnectionKnob GetNextNodes()
        {
            return NextNode;
        }

        public override void ApplyNodeSize()
        {
            NextNode.sidePosition = NodeSize.x / 2;
        }

        public override void NodeGUI()
        {
            EditorGUILayout.TextField("根结点");
        }
    }
}
