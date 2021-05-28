using System.Diagnostics;
using ETModel;
using ETModel.BBValues;
using Log = NPBehave_Core.Log;

namespace NPBehave
{
    /// <summary>
    /// ����Ŀ���Ƿ���
    /// </summary>
    public class TaskCondition : TaskDecorator
    {
        public override string _key
        {
            get
            {
                return "List";
            }
        }

        public TaskCondition(int taskId, Stops stopsOnChange, Node decoratee) : base( taskId, stopsOnChange,
            decoratee, "TaskCondition")
        {
        }
       
    }
}
