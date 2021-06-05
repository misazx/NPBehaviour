using System;

namespace ETModel
{
    public abstract class ATaskTargetSystemBase : IReference
    {
        /// <summary>
        /// 归属的运行时行为树实例
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
