using System;
using ETModel;

namespace ETModel
{ 
	[AttributeUsage(AttributeTargets.Class)]
	public class TaskTargetAttribute : BaseAttribute
	{
		public ETaskTargetType type;

		public TaskTargetAttribute(ETaskTargetType type)
		{
			this.type = type;
		}
	}
}