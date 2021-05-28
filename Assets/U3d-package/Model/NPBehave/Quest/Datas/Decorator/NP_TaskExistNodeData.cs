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
    [BoxGroup("�������״̬�ڵ�����"), GUIColor(0.961f, 0.902f, 0.788f, 1f)]
    [HideLabel]
    public class NP_TaskExistNodeData : NP_NodeDataBase
    {
        [HideInEditorMode]
        private TaskExist m_TaskCondition;

        [LabelText("��ֹ����")]
        public Stops Stop = Stops.IMMEDIATE_RESTART;


        public override Decorator CreateDecoratorNode(long unitId, NP_RuntimeTree runtimeTree, Node node)
        {
            var taskId = runtimeTree.BelongNP_DataSupportor.NpDataSupportorBase.NPBehaveTreeConfigId;
            this.m_TaskCondition = new TaskExist(taskId, this.Stop, node);
            return this.m_TaskCondition;
        }

        public override Node NP_GetNode()
        {
            return this.m_TaskCondition;
        }
    }
}