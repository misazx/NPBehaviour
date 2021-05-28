//------------------------------------------------------------
// Author: �������������
// Mail: 1778139321@qq.com
// Data: 2019��8��22�� 21:13:00
//------------------------------------------------------------

using System;
using Sirenix.OdinInspector;

namespace ETModel
{
    [Title("��ӡ��Ϣ", TitleAlignment = TitleAlignments.Centered)]
    public class NP_LogAction : NP_ClassForStoreAction
    {
        [LabelText("��Ϣ")]
        public string LogInfo;

        public override Action GetActionToBeDone()
        {
            this.Action = this.TestLog;
            return this.Action;
        }

        public void TestLog()
        {
            Log.Info(LogInfo);
        }
    }
}