using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ETModel
{
    [Title("任务目标数据块", TitleAlignment = TitleAlignments.Centered)]
    [HideLabel]
    public class TaskTargetDataBase
    {
        /// <summary>
        /// 任务目标归属的数据块的Id(也就是NP_DataSupportor的Id)
        /// </summary>
        [HideInInspector]
        [BoxGroup("必填项")]
        [LabelText("任务目标归属的数据块Id")]
        public long BelongToBuffDataSupportorId;

        [HideInInspector]
        [LabelText("目标的Id")]
        [BoxGroup("必填项")]
        public long TargetId = IdGenerater.GenerateId();

        [BoxGroup("必填项")]
        [LabelText("任务目标类型")]
        public ETaskTargetType type;

        [BoxGroup("必填项")]
        [LabelText("任务目标需要达成次数,默认1")]
        [Min(1)]
        public int count = 1;

        [BoxGroup("必填项")]
        [LabelText("任务目标参数列表(如怪物/npc Id)")]
        public List<string> parameters = new List<string>();

        public string key
        {
            get { return "target_" + this.TargetId; }
        }
    }
}
