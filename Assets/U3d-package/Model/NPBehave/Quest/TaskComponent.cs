using NPBehave;
using System;
using System.Collections.Generic;

namespace ETModel
{
	public class TaskData
    {
        public int taskId;

		public Dictionary<long, int> progress;

        public TaskData(int taskId, Dictionary<long, int> progress)
        {
            this.taskId = taskId;
            this.progress = progress;
        }
    }

	public class TaskComponent : Component
	{
		public Dictionary<ETaskTargetType, Type> targetSystems = new Dictionary<ETaskTargetType, Type>();

        private LinkedList<ATaskTargetSystemBase> m_Systems = new LinkedList<ATaskTargetSystemBase>();
        private Dictionary<long,ATaskTargetSystemBase> m_DicSystems = new Dictionary<long, ATaskTargetSystemBase>();

        private LinkedListNode<ATaskTargetSystemBase> m_Current, m_Next;

        private Dictionary<int, TaskData> m_Data = new Dictionary<int, TaskData>();

        private Blackboard globalBlackboard
        {
            get
            {
                return SyncContext.GetSharedBlackboard("Task");
            }
        }

        private ListenTaskAcceptEvent acceptEvent;

        public TaskComponent():base()
        {
            acceptEvent = new ListenTaskAcceptEvent();
            Game.Scene.GetComponent<GameEventSystem>().RegisterEvent(EventIdType.TaskAccept, acceptEvent);
        }

        public override void Dispose()
        {
            base.Dispose();

            Game.Scene.GetComponent<GameEventSystem>().UnRegisterEvent(EventIdType.TaskAccept, acceptEvent);
        }

        public void Update()
        {
            this.m_Current = m_Systems.First;
            //ÂÖÑ¯Á´±í
            while (this.m_Current != null)
            {
                ATaskTargetSystemBase aTarget = this.m_Current.Value;
                aTarget.OnUpdate();
                this.m_Current = this.m_Current.Next;
            }
        }

		public void FixedUpdate()
        {
			

		}

		public ATaskTargetSystemBase StartObserving(int taskId, TaskTargetDataBase data, NP_RuntimeTree runtimeTree)
        {
			var type = data.type;
			Type system;
			if(!targetSystems.TryGetValue(type, out system))
            {
				Log.Error("The task target system has not been found.",type.ToString());
				return null;
            }
			ATaskTargetSystemBase targetSystem = ReferencePool.Acquire(system) as ATaskTargetSystemBase;
			targetSystem.BelongtoRuntimeTree = runtimeTree;
			targetSystem.data = data;
			targetSystem.taskId = taskId;

			targetSystem.OnInit();

			m_Systems.AddLast(targetSystem);
            m_DicSystems.Add(data.TargetId, targetSystem);
            return targetSystem;
		}

		public void StopObserving(ATaskTargetSystemBase targetSystem)
		{
			targetSystem.OnFinished();
            m_Systems.Remove(targetSystem);
            m_DicSystems.Remove(targetSystem.data.TargetId);
		}

        public void StopObserving(long targetId)
        {
            var targetSystem = m_DicSystems[targetId];
            if (targetSystem == null)
                return;
            m_Systems.Remove(targetSystem);
            m_DicSystems.Remove(targetId);
        }

        public void UpdateData(int taskId, TaskData data = null)
        {
            if (data == null)
            {
                Log.Debug("TaskComponent UpdateData Remove...");

            }
            else if (m_Data.ContainsKey(taskId))
            {
                Log.Debug("TaskComponent UpdateData Update...");

            }
            else
            {
                Log.Debug("TaskComponent UpdateData Add...");

            }
            m_Data[taskId] = data;
            bool isTaskExist = data != null;
            globalBlackboard.Set<bool>("Exist_" + taskId.ToString(), isTaskExist);

            if (!isTaskExist)
            {
                //NP_TaskRuntimeTreeFactory.RemoveTaskNpRuntimeTree(taskId);
            }
        }
    }

    public class ListenTaskAcceptEvent : AEvent<int, TaskData>
    {
        public override void Run(int taskId, TaskData data)
        {
            if (data == null)
                data = new TaskData(taskId, new Dictionary<long, int>());

            Game.Scene.GetComponent<TaskComponent>().UpdateData(taskId, data);
        }
    }

    public class ListenTaskAbandonEvent : AEvent<int>
    {
        public override void Run(int taskId)
        {
            Game.Scene.GetComponent<TaskComponent>().UpdateData(taskId);
        }
    }
}