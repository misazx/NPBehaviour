using ETModel;

namespace ETHotfix
{
    [TaskTarget(ETaskTargetType.KillMonster)]
    public class KillMonsterTaskTargetSystem : ATaskTargetSystemBase
    {
        private IEvent eve;

        public override void OnInit()
        {
            Log.Debug("KillMonsterTaskTargetSystem OnInit...");

            this.eve = new ListenKillMonsterEvent(this);
            Game.Scene.GetComponent<GameEventSystem>().RegisterEvent(EventIdType.KillMonster, eve);
        }

        public override void OnFinished()
        {
            Log.Debug("KillMonsterTaskTargetSystem OnFinished...");

            Game.Scene.GetComponent<GameEventSystem>().UnRegisterEvent(EventIdType.KillMonster, eve);
        }


        public override void OnUpdate()
        {
            Log.Debug("KillMonsterTaskTargetSystem OnUpdate...");
        }


    }

    public class ListenKillMonsterEvent : AEvent<int, int>
    {
        private ATaskTargetSystemBase sys;

        public ListenKillMonsterEvent(ATaskTargetSystemBase sys)
        {
            this.sys = sys;
        }

        public override void Run(int monsterId, int count)
        {
            int id = int.Parse(sys.data.parameters[0]);
            if (id != monsterId)
                return;

            var key = sys.data.key;
            var blackboard = sys.BelongtoRuntimeTree.GetBlackboard();
            int nowCount = blackboard.Get<int>(key);
            int targetCount = nowCount + count;
            blackboard.Set<int>(key, targetCount);
        }
    }
}
