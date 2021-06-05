using System;
using System.Collections.Generic;
using ETModel;

namespace ETModel
{
	[ObjectSystem]
	public class TaskComponentAwakeSystem : AwakeSystem<TaskComponent>
	{
		public override void Awake(TaskComponent self)
		{
			self.Awake();
		}
	}

    [ObjectSystem]
    public class TaskComponentLoadSystem : LoadSystem<TaskComponent>
    {
        public override void Load(TaskComponent self)
        {
            self.Load();
        }
    }

    [ObjectSystem]
    public class TaskComponentUpdateSystem : UpdateSystem<TaskComponent>
    {
        public override void Update(TaskComponent self)
        {
            self.Update();
        }
    }

    [ObjectSystem]
    public class TaskComponentFixedUpdateSystem : FixedUpdateSystem<TaskComponent>
    {
        public override void FixedUpdate(TaskComponent self)
        {
            self.FixedUpdate();
        }
    }

    public static class TaskComponentHelper
	{

		public static void Awake(this TaskComponent self)
		{
			self.Load();
		}

		public static void Load(this TaskComponent self)
		{
            self.targetSystems.Clear();
            List<Type> types = Game.EventSystem.GetTypes();

            foreach (Type type in types)
            {
                object[] attrs = type.GetCustomAttributes(typeof(TaskTargetAttribute), false);
                if (attrs.Length == 0)
                {
                    continue;
                }

				TaskTargetAttribute attribute = attrs[0] as TaskTargetAttribute;

                var targetType = attribute.type;
                self.targetSystems.Add(targetType, type);
            }
        }
	

	}
}