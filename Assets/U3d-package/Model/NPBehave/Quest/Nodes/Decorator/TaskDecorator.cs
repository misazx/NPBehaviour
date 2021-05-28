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

    public class TaskTargetDecorator : TaskDecorator
    {
        public override string key => base.key;

        public TaskTargetDecorator(int taskId, Stops stopsOnChange, Node decoratee, string name) : base(taskId, stopsOnChange,
decoratee, name)
        {

        }
    }
}