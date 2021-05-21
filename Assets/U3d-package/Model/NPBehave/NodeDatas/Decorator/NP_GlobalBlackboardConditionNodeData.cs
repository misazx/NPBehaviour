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
    [BoxGroup("�ڰ������ڵ�����"), GUIColor(0.961f, 0.902f, 0.788f, 1f)]
    [HideLabel]
    public class NP_GlobalBlackboardConditionNodeData : NP_NodeDataBase
    {
        [HideInEditorMode]
        private GlobalBlackboardCondition m_BlackboardConditionNode;

        [LabelText("�������")]
        public Operator Ope = Operator.IS_EQUAL;

        [LabelText("��ֹ����")]
        public Stops Stop = Stops.IMMEDIATE_RESTART;

        public NP_BlackBoardRelationData NPBalckBoardRelationData = new NP_BlackBoardRelationData() { WriteOrCompareToBB = true };

        public override Decorator CreateDecoratorNode(long unitId, NP_RuntimeTree runtimeTree, Node node)
        {
            this.m_BlackboardConditionNode = new GlobalBlackboardCondition(this.NPBalckBoardRelationData.BBKey,
                this.Ope,
                this.NPBalckBoardRelationData.NP_BBValue, this.Stop, node);
            //�˴���value������������裬��Ϊ��������Ϸ�����value����Ҫ��̬�ı��
            return this.m_BlackboardConditionNode;
        }

        public override Node NP_GetNode()
        {
            return this.m_BlackboardConditionNode;
        }
    }
}