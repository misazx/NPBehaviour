using NPBehave;
using System;
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
	public class TaskComponent : Component
	{
	}

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

    public static class TaskComponentHelper
    {
        public static void Awake(this TaskComponent self)
        {
            self.Load();
        }

        public static void Load(this TaskComponent self)
        {
            var sys = ETModel.Game.Scene.GetComponent<ETModel.TaskComponent>().targetSystems;
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
                sys.Add(targetType, type);
            }
        }


    }
}