using System;
using System.Collections.Generic;
using ETModel.BBValues;
using NPBehave;
using Exception = NPBehave.Exception;

namespace ETModel
{
    public class NP_TaskRuntimeTreeFactory
    {
        public static NP_RuntimeTree CreateTaskNpRuntimeTree(Unit unit, long nPDataId, long belongToTaskId)
        {
            NP_RuntimeTree result = NP_RuntimeTreeFactory.CreateNpRuntimeTree(unit, nPDataId);
            unit.GetComponent<TaskCanvasManagerComponent>()
                    .AddTaskCanvas(belongToTaskId, result);
            return result;
        }

        public static void TestCreateTaskNPRuntimeTree()
        {
            Unit unit = ComponentFactory.Create<Unit>();
            unit.AddComponent<NP_RuntimeTreeManager>();
            unit.AddComponent<TaskCanvasManagerComponent>();
            unit.AddComponent<UnitPathComponent>();
            unit.AddComponent<MoveComponent>();

            UnitComponent.Instance.Add(unit);
            Log.Debug("------------------->>>>.");
            ConfigComponent configComponent = Game.Scene.GetComponent<ConfigComponent>();
            int taskId = 1;
            NP_TaskRuntimeTreeFactory.CreateTaskNpRuntimeTree(unit, configComponent.Get<Client_TaskCanvasConfig>(taskId).NPBehaveId, taskId).Start();
        }
    }

}