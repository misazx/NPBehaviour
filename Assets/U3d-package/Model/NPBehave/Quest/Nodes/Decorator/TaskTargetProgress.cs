using System.Diagnostics;
using ETModel;
using ETModel.BBValues;
using Log = NPBehave_Core.Log;

namespace NPBehave
{
    /// <summary>
    /// ����Ŀ�����
    /// </summary>
    public class TaskTargetProgress : TaskTargetDecorator
    {

        public TaskTargetProgress(NP_RuntimeTree runtimeTree, VTD_Id NodeId, Stops stopsOnChange, Node decoratee) : base(runtimeTree, NodeId, stopsOnChange, decoratee)
        {
        }
       
    }
}
