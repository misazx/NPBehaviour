using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ETModel
{
    [Title("����Ŀ�����ݿ�", TitleAlignment = TitleAlignments.Centered)]
    [HideLabel]
    public class TaskTargetDataBase
    {
        /// <summary>
        /// ����Ŀ����������ݿ��Id(Ҳ����NP_DataSupportor��Id)
        /// </summary>
        [HideInInspector]
        [BoxGroup("������")]
        [LabelText("����Ŀ����������ݿ�Id")]
        public long BelongToBuffDataSupportorId;

        [HideInInspector]
        [LabelText("Ŀ���Id")]
        [BoxGroup("������")]
        public long TargetId = IdGenerater.GenerateId();

        [BoxGroup("������")]
        [LabelText("����Ŀ������")]
        public ETaskTargetType type;

        [BoxGroup("������")]
        [LabelText("����Ŀ����Ҫ��ɴ���,Ĭ��1")]
        [Min(1)]
        public int count = 1;

        [BoxGroup("������")]
        [LabelText("����Ŀ������б�(�����/npc Id)")]
        public List<string> parameters = new List<string>();

        public string key
        {
            get { return "target_" + this.TargetId; }
        }
    }
}
