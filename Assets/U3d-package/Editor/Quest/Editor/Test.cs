using MonKey;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETModel;
using ETHotfix;

namespace Task.Test
{
    public class Test
    {
        [Command("Task Accept", "接受任务", Category = "Task")]
        public static void Accept(int taskId)
        {
            ETModel.Game.Scene.GetComponent<ETModel.GameEventSystem>().Run(ETModel.EventIdType.TaskAccept, taskId, new TaskData(taskId, new Dictionary<long, int>()));
        }

        [Command("Task KillMonster", "杀怪", Category = "Task")]
        public static void KillMonster(int monsterId, int count = 1)
        {
            ETHotfix.Game.Scene.GetComponent<ETHotfix.GameEventSystem>().Run(ETHotfix.EventIdType.KillMonster, monsterId, count);
        }


    }
}

