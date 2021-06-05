using MongoDB.Bson.Serialization.Attributes;
using Sirenix.OdinInspector;

namespace ETModel
{
    [Title("任务目标节点数据块", TitleAlignment = TitleAlignments.Centered)]
    [HideLabel]
    public class TaskTargetData : NodeDataBase
    {
        [LabelText("描述")]
        [BsonIgnore]
        public string Des;

        /// <summary>
        /// 目标数据
        /// </summary>
        public TaskTargetDataBase BaseData;
    }
}