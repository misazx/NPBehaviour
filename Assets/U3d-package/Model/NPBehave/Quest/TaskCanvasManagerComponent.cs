//此文件格式由工具自动生成

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
    /// 任务行为树管理器
    /// </summary>
    public class TaskCanvasManagerComponent : Component
    {
        #region 私有成员

        /// <summary>
        /// 任务Id与其对应行为树映射,因为一个技能可能由多个行为树组成，所以value使用了List的形式
        /// </summary>
        private Dictionary<int, List<NP_RuntimeTree>> Tasks = new Dictionary<int, List<NP_RuntimeTree>>();


        #endregion

        #region 公有成员


        public void AddTaskCanvas(int taskId, NP_RuntimeTree npRuntimeTree)
        {
            if (npRuntimeTree == null)
            {
                Log.Error($"试图添加的id为{taskId}的图为空");
                return;
            }

            if (Tasks.TryGetValue(taskId, out var taskContent))
            {
                taskContent.Add(npRuntimeTree);
            }
            else
            {
                Tasks.Add(taskId, new List<NP_RuntimeTree>() { npRuntimeTree });
            }
            var dataDic = npRuntimeTree.BelongNP_DataSupportor.DataDic;
            foreach (var data in dataDic)
            {
                if(data.Value is TaskTargetData targetData)
                {
                    Game.Scene.GetComponent<TaskComponent>().StartObserving(taskId, targetData.BaseData, npRuntimeTree);
                }
            }
        }


        /// <summary>
        /// 获取所有技能行为树
        /// </summary>
        /// <param name="taskId">技能标识</param>
        public Dictionary<int, List<NP_RuntimeTree>> GetAllTaskCanvas()
        {
            return this.Tasks;
        }

        /// <summary>
        /// 获取行为树
        /// </summary>
        /// <param name="taskId">技能标识</param>
        public List<NP_RuntimeTree> GetTaskCanvas(int taskId)
        {
            if (Tasks.TryGetValue(taskId, out var skillContent))
            {
                return skillContent;
            }
            else
            {
                Log.Error($"请求的ID标识为{taskId}的图不存在");
                return null;
            }
        }

        /// <summary>
        /// 移除行为树(移除一个技能标识对应所有图)
        /// </summary>
        /// <param name="taskId">技能标识</param>
        public void RemoveTaskCanvas(int taskId)
        {
            foreach (var TaskCanvas in GetTaskCanvas(taskId))
            {
                RemoveTaskCanvas(taskId, TaskCanvas);
            }
        }

        /// <summary>
        /// 移除行为树(移除一个技能标识对应的目标图)
        /// </summary>
        /// <param name="taskId">技能标识</param>
        /// <param name="npRuntimeTree">对应行为树</param>
        public void RemoveTaskCanvas(int taskId, NP_RuntimeTree npRuntimeTree)
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

            var dataDic = npRuntimeTree.BelongNP_DataSupportor.DataDic;
            foreach (var data in dataDic)
            {
                if (data.Value is TaskTargetData targetData)
                {
                    Game.Scene.GetComponent<TaskComponent>().StopObserving(targetData.BaseData.TargetId);
                }
            }
        }

        #endregion

        #region 生命周期函数

        public void Awake()
        {
            //此处填写Awake逻辑
        }

        public void Update()
        {
            //此处填写Update逻辑
        }

        public void FixedUpdate()
        {
            //此处填写FixedUpdate逻辑
        }

        public void Destroy()
        {
            //此处填写Destroy逻辑
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
            //此处填写释放逻辑,但涉及Entity的操作，请放在Destroy中
        }

        #endregion
    }
}