using System.Diagnostics;
using ETModel;
using ETModel.BBValues;
using Log = NPBehave_Core.Log;

namespace NPBehave
{
    public class TaskDecorator : ObservingDecorator
    {
        protected int taskId;

        public virtual string key
        {
            get
            {
                return string.Empty + taskId;
            }
        }

        private Blackboard globalBlackboard
        {
            get
            {
                return SyncContext.GetSharedBlackboard("Task");
            }
        }


        public TaskDecorator(int taskId, Stops stopsOnChange, Node decoratee, string name) : base(name, stopsOnChange,
            decoratee)
        {
            this.taskId = taskId;
            this.stopsOnChange = stopsOnChange;
        }

        override protected void StartObserving()
        {
            this.globalBlackboard.AddObserver(key, onValueChanged);
        }

        override protected void StopObserving()
        {
            this.globalBlackboard.RemoveObserver(key, onValueChanged);
        }

        private void onValueChanged(Blackboard.Type type, ANP_BBValue newValue)
        {
            Evaluate();
        }

        override protected bool IsConditionMet()
        {
            ANP_BBValue bbValue = this.globalBlackboard.Get(key);

            return bbValue!=null?(bbValue as NP_BBValue_Bool).Value:false;

        }

        override public string ToString()
        {
            return this.key;
        }
    }

    public class TaskContainerDecorator : TaskDecorator
    {
        public override string key
        {
            get
            {
                return _key + "_" + taskId;
            }
        }

        public virtual string _key
        {
            get
            {
                return string.Empty;
            }
        }

        public TaskContainerDecorator(int taskId, Stops stopsOnChange, Node decoratee, string name) : base(taskId, stopsOnChange,
    decoratee, name)
        {
       
        }
    }

    public class TaskTargetDecorator : ObservingDecorator
    {
        protected int taskId;
        protected TaskTargetDataBase data;
        protected NP_RuntimeTree runtimeTree;

        protected ATaskTargetSystemBase targetSystem;
        private string key
        {
            get { return this.data.key; }
        }

        public TaskTargetDecorator(NP_RuntimeTree runtimeTree, VTD_Id NodeId, Stops stopsOnChange, Node decoratee):base("TaskTargetDecorator", stopsOnChange,decoratee)
        {
            this.data = (runtimeTree.BelongNP_DataSupportor.DataDic[NodeId.Value] as TaskTargetData).BaseData;
            var taskId = runtimeTree.BelongNP_DataSupportor.NpDataSupportorBase.NPBehaveTreeConfigId;
            this.taskId = taskId;
            this.runtimeTree = runtimeTree;
        }

        protected override void StartObserving()
        {
            this.RootNode.Blackboard.AddObserver(key, onValueChanged);
        }

        protected override void StopObserving()
        {
            this.RootNode.Blackboard.RemoveObserver(key, onValueChanged);
        }

        private void onValueChanged(Blackboard.Type type, ANP_BBValue newValue)
        {
            Evaluate();
        }

        override protected bool IsConditionMet()
        {
            NP_BBValue_Int bbValue = this.RootNode.Blackboard.Get(key) as NP_BBValue_Int;
            var val = bbValue!=null?bbValue.GetValue():0;
            return val >= this.data.count;
        }
    }
}