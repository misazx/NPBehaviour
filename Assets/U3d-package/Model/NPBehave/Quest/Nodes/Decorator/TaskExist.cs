using System.Diagnostics;
using ETModel;
using ETModel.BBValues;
using Log = NPBehave_Core.Log;

namespace NPBehave
{
    /// <summary>
    /// �����Ƿ����
    /// </summary>
    public class TaskExist : TaskContainerDecorator
    {
        public override string _key
        {
            get
            {
                return "Exist";
            }
        }
       
        public TaskExist(int taskId, Stops stopsOnChange, Node decoratee) : base(taskId, stopsOnChange,
            decoratee, "TaskExist")
        {
        }

    }
}
