﻿using System;
using System.IO;
using UnityEngine;
using Plugins.NodeEditor.Editor.Canvas;
using Plugins.NodeEditor.Node_Editor.Default;
using UnityEditor;

namespace NodeEditorFramework.Standard
{
    public class NodeEditorInterface
    {
        public NodeEditorUserCache canvasCache;
        public Action<GUIContent> ShowNotificationAction;

        // GUI
        public string sceneCanvasName = "";
        public const float toolbarHeight = 20;

        public void ShowNotification(GUIContent message)
        {
            if (ShowNotificationAction != null)
                ShowNotificationAction(message);
        }

        #region GUI

        public void DrawToolbarGUI(Rect rect)
        {
            rect.height = toolbarHeight;
            GUILayout.BeginArea(rect, NodeEditorGUI.toolbar);
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("File", NodeEditorGUI.toolbarDropdown, GUILayout.Width(50)))
            {
                GenericMenu menu = new GenericMenu();
                NodeCanvasManager.FillCanvasTypeMenu(ref menu, NewNodeCanvas, "New Canvas/");
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Load Canvas"), false, LoadCanvas);
                menu.AddItem(new GUIContent("Reload Canvas"), false, ReloadCanvas);
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Save Canvas"), false, SaveCanvas);
                menu.AddItem(new GUIContent("Save Canvas As"), false, SaveCanvasAs);
                menu.ShowAsContext();
            }

            GUILayout.Space(10);
            //GUILayout.FlexibleSpace();
            NodeEditor.curNodeCanvas.DrawToolbar();
            GUILayout.Space(10);
            //重定向到数据资产按钮
            EditorGUILayoutExtension.LinkFileLabelField("Click To go to asset Path",this.canvasCache.openedCanvasPath);
            GUILayout.Label(this.canvasCache.typeData.DisplayString, NodeEditorGUI.toolbarLabel);

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        #endregion

        private void NewNodeCanvas(Type canvasType)
        {
            this.AssertSavaCanvasSuccessfully();
            canvasCache.NewNodeCanvas(canvasType);
        }

#if UNITY_EDITOR
        private void LoadCanvas()
        {
            string path = UnityEditor.EditorUtility.OpenFilePanel("Load Node Canvas", NodeEditor.editorPath + "Resources/Saves/", "asset");
            if (!path.Contains(Application.dataPath))
            {
                if (!string.IsNullOrEmpty(path))
                    ShowNotification(new GUIContent("You should select an asset inside your project folder!"));
            }
            else
            {
                this.AssertSavaCanvasSuccessfully();
                path = path.Replace("/", @"\");
                canvasCache.LoadNodeCanvas(path);
            }
        }

        private void ReloadCanvas()
        {
            string path = canvasCache.nodeCanvas.savePath;
            if (!string.IsNullOrEmpty(path))
            {
                canvasCache.LoadNodeCanvas(path);
                ShowNotification(new GUIContent("Canvas Reloaded!"));
            }
            else
                ShowNotification(new GUIContent("Cannot reload canvas as it has not been saved yet!"));
        }

        public void SaveCanvas()
        {
            string path = canvasCache.nodeCanvas.savePath;
            if (!string.IsNullOrEmpty(path))
            {
                canvasCache.SaveNodeCanvas(path);
                ShowNotification(new GUIContent("Canvas Saved!"));
                Debug.Log($"{path}已保存成功");
            }
            else
            {
                Debug.LogError($"{path}保存失败，请先确保要覆盖的文件已存在！（尝试使用Save As创建目标文件）");
                ShowNotification(new GUIContent("No save location found. Use 'Save As'!"));
            }
        }

        /// <summary>
        /// 供外部调用的保存当前图的接口
        /// </summary>
        /// <returns></returns>
        public bool AssertSavaCanvasSuccessfully()
        {
            if (canvasCache.nodeCanvas is DefaultCanvas || !File.Exists(NodeEditorSaveManager.GetLastCanvasPath()))
            {
                return true;
            }

            string path = canvasCache.nodeCanvas.savePath;
            //清理要删掉的Node
            foreach (var nodeForDelete in canvasCache.nodeCanvas.nodesForDelete)
            {
                //去除Node附带的端口
                foreach (var connectPort in nodeForDelete.connectionPorts)
                {
                    UnityEngine.Object.DestroyImmediate(connectPort, true);
                }
                UnityEngine.Object.DestroyImmediate(nodeForDelete, true);
            }

            canvasCache.nodeCanvas.nodesForDelete.Clear();

            if (!string.IsNullOrEmpty(path))
            {
                canvasCache.SaveNodeCanvas(path);
                ShowNotification(new GUIContent("Canvas Saved!"));
                Debug.Log($"{path}已保存成功");
                return true;
            }
            else
            {
                Debug.LogError($"{path}保存失败，请先确保要覆盖的文件已存在！（尝试使用Save As创建目标文件）");
                ShowNotification(new GUIContent("No save location found. Use 'Save As'!"));
                return false;
            }
        }

        public void SaveCanvasAs()
        {
            string panelPath = NodeEditor.editorPath + "Resources/Saves/";
            string panelFileName = "Node Canvas";
            if (canvasCache.nodeCanvas != null && !string.IsNullOrEmpty(canvasCache.nodeCanvas.savePath))
            {
                panelPath = canvasCache.nodeCanvas.savePath;
                string savedFileName = System.IO.Path.GetFileNameWithoutExtension(panelPath);
                if (!string.IsNullOrEmpty(savedFileName))
                {
                    panelPath = panelPath.Substring(0, panelPath.LastIndexOf(savedFileName));
                    panelFileName = savedFileName;
                }
            }

            string path = UnityEditor.EditorUtility.SaveFilePanelInProject("Save Node Canvas", panelFileName, "asset", "", panelPath);
            if (!string.IsNullOrEmpty(path))
                canvasCache.SaveNodeCanvas(path);
        }
#endif
    }
}