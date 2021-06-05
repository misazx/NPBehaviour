using System.Collections.Generic;
using NPBehave;
using Sirenix.OdinInspector;

namespace ETModel
{
    [BoxGroup("����Ŀ����Ƚڵ�����"), GUIColor(0.961f, 0.902f, 0.788f, 1f)]
    [HideLabel]
    public class NP_TaskTargetProgressNodeData : NP_NodeDataBase
    {
        [HideInEditorMode]
        private TaskTargetProgress m_TaskCondition;

        [BoxGroup("Ŀ��ڵ�Id��Ϣ")]
        [HideLabel]
        public VTD_Id NodeId;

        [LabelText("��ֹ����")]
        public Stops Stop = Stops.IMMEDIATE_RESTART;

        public override Decorator CreateDecoratorNode(long unitId, NP_RuntimeTree runtimeTree, Node node)
        {
            this.m_TaskCondition = new TaskTargetProgress(runtimeTree, NodeId, this.Stop, node);
            return this.m_TaskCondition;
        }

        public override Node NP_GetNode()
        {
            return this.m_TaskCondition;
        }
    }
}
