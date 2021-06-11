﻿using System;
using ETModel;
using NETCoreTest.Framework;
using UnityEngine;

namespace ETHotfix
{
    public static class Init
    {
        public static async void Start()
        {
#if ILRuntime
			if (!Define.IsILRuntime)
			{
				Log.Error("mono层是mono模式, 但是Hotfix层是ILRuntime模式");
			}
#else
            if (Define.IsILRuntime)
            {
                Log.Error("mono层是ILRuntime模式, Hotfix层是mono模式");
            }
#endif

            try
            {
                //显示转圈圈（因为后期要加很多界面，所以要等好几秒）
                ETModel.Game.EventSystem.Run(ETModel.EventIdType.ShowLoadingUI);

                // 注册热更层回调
                ETModel.Game.Hotfix.Update = () => { Update(); };
                ETModel.Game.Hotfix.FixedUpdate = () => { FixedUpdate(); };
                ETModel.Game.Hotfix.LateUpdate = () => { LateUpdate(); };
                ETModel.Game.Hotfix.OnApplicationQuit = () => { OnApplicationQuit(); };

                Game.Scene.AddComponent<NumericWatcherComponent>();

                Game.Scene.AddComponent<ConfigComponent>();
                Game.Scene.AddComponent<GameEventSystem>();
                Game.Scene.AddComponent<TaskComponent>();

                //显示登录UI
                Game.EventSystem.Run(EventIdType.ShowLoginUI);

                //至此，检查更新界面使命正式结束
                ETModel.Game.EventSystem.Run(ETModel.EventIdType.CheckForUpdateFinish);
                ETModel.Log.Info("关闭了资源热更新界面");
                //关闭转圈圈
                ETModel.Game.EventSystem.Run(ETModel.EventIdType.CloseLoadingUI);

                Game.EventSystem.Run(EventIdType.GameInitFinished);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void Update()
        {
            try
            {
                Game.EventSystem.Update();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void FixedUpdate()
        {
            try
            {
                Game.EventSystem.FixedUpdate();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void LateUpdate()
        {
            try
            {
                Game.EventSystem.LateUpdate();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void OnApplicationQuit()
        {
            Game.Close();
        }
    }

    [Event(EventIdType.GameInitFinished)]
    public class Test : AEvent
    {
        public override void Run()
        {
            NP_TaskRuntimeTreeFactory.TestCreateTaskNPRuntimeTree();
        }
    }
}