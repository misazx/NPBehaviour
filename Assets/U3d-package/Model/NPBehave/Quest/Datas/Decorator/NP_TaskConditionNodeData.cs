//------------------------------------------------------------
// Author: �������������
// Mail: 1778139321@qq.com
// Data: 2019��8��21�� 7:12:13
//------------------------------------------------------------

using System.Collections.Generic;
using NPBehave;
using Sirenix.OdinInspector;

namespace ETModel
{
    [BoxGroup("���������ڵ�����"), GUIColor(0.961f, 0.902f, 0.788f, 1f)]
    [HideLabel]
    public class NP_TaskConditionNodeData : NP_NodeDataBase
    {
        [HideInEditorMode]
        private TaskCondition m_TaskCondition;

        [LabelText("��ֹ����")]
        public Stops Stop = Stops.IMMEDIATE_RESTART;


        public override Decorator CreateDecoratorNode(long unitId, NP_RuntimeTree runtimeTree, Node node)
        {
            var taskId = runtimeTree.BelongNP_DataSupportor.NpDataSupportorBase.NPBehaveTreeDataId;
            this.m_TaskCondition = new TaskCondition(taskId, this.Stop, node);
            return this.m_TaskCondition;
        }

        public override Node NP_GetNode()
        {
            return this.m_TaskCondition;
        }
    }
}