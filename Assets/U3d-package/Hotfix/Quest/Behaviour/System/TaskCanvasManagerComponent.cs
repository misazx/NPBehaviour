//���ļ���ʽ�ɹ����Զ�����

using System.Collections.Generic;

namespace ETModel
{
    #region System

    [ObjectSystem]
    public class TaskCanvasManagerComponentComponentAwakeSystem : AwakeSystem<TaskCanvasManagerComponent>
    {
        public override void Awake(TaskCanvasManagerComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class TaskCanvasManagerComponentComponentUpdateSystem : UpdateSystem<TaskCanvasManagerComponent>
    {
        public override void Update(TaskCanvasManagerComponent self)
        {
            self.Update();
        }
    }

    [ObjectSystem]
    public class TaskCanvasManagerComponentComponentFixedUpdateSystem : FixedUpdateSystem<TaskCanvasManagerComponent>
    {
        public override void FixedUpdate(TaskCanvasManagerComponent self)
        {
            self.FixedUpdate();
        }
    }

    [ObjectSystem]
    public class TaskCanvasManagerComponentComponentDestroySystem : DestroySystem<TaskCanvasManagerComponent>
    {
        public override void Destroy(TaskCanvasManagerComponent self)
        {
            self.Destroy();
        }
    }

    #endregion

    /// <summary>
    /// ������Ϊ��������
    /// </summary>
    public class TaskCanvasManagerComponent : Component
    {
        #region ˽�г�Ա

        /// <summary>
        /// ����Id�����Ӧ��Ϊ��ӳ��,��Ϊһ�����ܿ����ɶ����Ϊ����ɣ�����valueʹ����List����ʽ
        /// </summary>
        private Dictionary<long, List<NP_RuntimeTree>> Tasks = new Dictionary<long, List<NP_RuntimeTree>>();


        #endregion

        #region ���г�Ա


        public void AddTaskCanvas(long taskId, NP_RuntimeTree npRuntimeTree)
        {
            if (npRuntimeTree == null)
            {
                Log.Error($"��ͼ��ӵ�idΪ{taskId}��ͼΪ��");
                return;
            }

            if (Tasks.TryGetValue(taskId, out var skillContent))
            {
                skillContent.Add(npRuntimeTree);
            }
            else
            {
                Tasks.Add(taskId, new List<NP_RuntimeTree>() { npRuntimeTree });
            }
        }


        /// <summary>
        /// ��ȡ���м�����Ϊ��
        /// </summary>
        /// <param name="taskId">���ܱ�ʶ</param>
        public Dictionary<long, List<NP_RuntimeTree>> GetAllTaskCanvas()
        {
            return this.Tasks;
        }

        /// <summary>
        /// ��ȡ��Ϊ��
        /// </summary>
        /// <param name="taskId">���ܱ�ʶ</param>
        public List<NP_RuntimeTree> GetTaskCanvas(long taskId)
        {
            if (Tasks.TryGetValue(taskId, out var skillContent))
            {
                return skillContent;
            }
            else
            {
                Log.Error($"�����ID��ʶΪ{taskId}��ͼ������");
                return null;
            }
        }

        /// <summary>
        /// �Ƴ���Ϊ��(�Ƴ�һ�����ܱ�ʶ��Ӧ����ͼ)
        /// </summary>
        /// <param name="taskId">���ܱ�ʶ</param>
        public void RemoveTaskCanvas(long taskId)
        {
            foreach (var TaskCanvas in GetTaskCanvas(taskId))
            {
                RemoveTaskCanvas(taskId, TaskCanvas);
            }
        }

        /// <summary>
        /// �Ƴ���Ϊ��(�Ƴ�һ�����ܱ�ʶ��Ӧ��Ŀ��ͼ)
        /// </summary>
        /// <param name="taskId">���ܱ�ʶ</param>
        /// <param name="npRuntimeTree">��Ӧ��Ϊ��</param>
        public void RemoveTaskCanvas(long taskId, NP_RuntimeTree npRuntimeTree)
        {
            List<NP_RuntimeTree> targetSkillContent = GetTaskCanvas(taskId);
            if (targetSkillContent != null)
            {
                for (int i = targetSkillContent.Count - 1; i >= 0; i--)
                {
                    if (targetSkillContent[i] == npRuntimeTree)
                    {
                        this.Entity.GetComponent<NP_RuntimeTreeManager>().RemoveTree(npRuntimeTree.Id);
                        targetSkillContent.RemoveAt(i);
                    }
                }
            }
        }

        #endregion

        #region �������ں���

        public void Awake()
        {
            //�˴���дAwake�߼�
        }

        public void Update()
        {
            //�˴���дUpdate�߼�
        }

        public void FixedUpdate()
        {
            //�˴���дFixedUpdate�߼�
        }

        public void Destroy()
        {
            //�˴���дDestroy�߼�
        }

        public override void Dispose()
        {
            if (IsDisposed)
                return;
            base.Dispose();
            foreach (var taskContent in Tasks)
            {
                foreach (var TaskCanvas in taskContent.Value)
                {
                    TaskCanvas.Dispose();
                }
            }

            Tasks.Clear();
            //�˴���д�ͷ��߼�,���漰Entity�Ĳ����������Destroy��
        }

        #endregion
    }
}