using MongoDB.Bson.Serialization.Attributes;
using Sirenix.OdinInspector;

namespace ETModel
{
    [Title("����Ŀ��ڵ����ݿ�", TitleAlignment = TitleAlignments.Centered)]
    [HideLabel]
    public class TaskTargetData : NodeDataBase
    {
        [LabelText("����")]
        [BsonIgnore]
        public string Des;

        /// <summary>
        /// Ŀ������
        /// </summary>
        public TaskTargetDataBase BaseData;
    }
}