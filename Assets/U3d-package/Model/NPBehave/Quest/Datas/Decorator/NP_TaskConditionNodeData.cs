using System.Collections.Generic;
using NPBehave;
using Sirenix.OdinInspector;

namespace ETModel
{
    [BoxGroup("任务目标节点配置"), GUIColor(0.961f, 0.902f, 0.788f, 1f)]
    [HideLabel]
    public class NP_TaskConditionNodeData : NP_NodeDataBase
    {
        [HideInEditorMode]
        private TaskCondition m_TaskCondition;

        [LabelText("终止条件")]
        public Stops Stop = Stops.IMMEDIATE_RESTART;



        public override Decorator CreateDecoratorNode(long unitId, NP_RuntimeTree runtimeTree, Node node)
        {
            var taskId = runtimeTree.BelongNP_DataSupportor.NpDataSupportorBase.NPBehaveTreeConfigId;
            this.m_TaskCondition = new TaskCondition(taskId, this.Stop, node);
            return this.m_TaskCondition;
        }

        public override Node NP_GetNode()
        {
            return this.m_TaskCondition;
        }
    }
}
