using System;

namespace ETModel
{
    public abstract class ATaskTargetSystemBase : IReference
    {
        /// <summary>
        /// ����������ʱ��Ϊ��ʵ��
        /// </summary>
        public NP_RuntimeTree BelongtoRuntimeTree;

        public TaskTargetDataBase data;

        public int taskId;

        public abstract void OnInit();

        public abstract void OnFinished();


        public virtual void OnUpdate()
        {
        }

        public void Clear()
        {
            this.BelongtoRuntimeTree = null;
        }
    }
}
