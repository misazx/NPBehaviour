using System.Diagnostics;
using ETModel;
using ETModel.BBValues;
using Log = NPBehave_Core.Log;

namespace NPBehave
{
    /// <summary>
    /// 任务是否接受
    /// </summary>
    public class TaskCondition : ObservingDecorator
    {
        private const string _key = "List";

        private long taskId;

        private string key { get
            {
                return _key + taskId;
            }
        }

        private Blackboard globalBlackboard
        {
            get
            {
                return SyncContext.GetSharedBlackboard("Task");
            }
        }


        public TaskCondition(long taskId, Stops stopsOnChange, Node decoratee) : base("TaskCondition", stopsOnChange,
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

            return (bbValue as NP_BBValue_Bool).Value;

        }

        override public string ToString()
        {
            return this.key;
        }
    }
}
