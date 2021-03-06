using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework
{
    public class NodeCanvasManager
    {
        /// <summary>
        /// 所有Canvas类型收集
        /// </summary>
        private static Dictionary<Type, NodeCanvasTypeData> CanvasTypes;

        /// <summary>
        /// 能显示在ToolBar的Create Canvas多选框中的Canvas类型
        /// </summary>
        private static Dictionary<Type, NodeCanvasTypeData> OnlyInToolBarCanvasTypes;

        private static Action<Type> _menuCallback;

        /// <summary>
        /// Fetches every CanvasType Declaration in the script assemblies to provide the framework with custom canvas types
        /// </summary>
        public static void FetchCanvasTypes()
        {
            CanvasTypes = new Dictionary<Type, NodeCanvasTypeData>();
            foreach (Type type in ReflectionUtility.getSubTypes(typeof (NodeCanvas), typeof (NodeCanvasTypeAttribute)))
            {
                object[] nodeAttributes = type.GetCustomAttributes(typeof (NodeCanvasTypeAttribute), false);
                NodeCanvasTypeAttribute attr = nodeAttributes[0] as NodeCanvasTypeAttribute;
                CanvasTypes.Add(type, new NodeCanvasTypeData() { CanvasType = type, DisplayString = attr.Name });
            }

            //下面是能显示在ToolBar的Create Canvas多选框中的Canvas类型收集
            OnlyInToolBarCanvasTypes = new Dictionary<Type, NodeCanvasTypeData>();
            foreach (var type in ReflectionUtility.getSubTypes(typeof (NodeCanvas), typeof (NodeCanvasTypeAttribute)))
            {
                object[] OnlyInToolBarCanvasTypesAttributes = type.GetCustomAttributes(typeof (CannotShowInToolBarCanvasTypeAttribute), false);
                object[] nodeAttributes = type.GetCustomAttributes(typeof (NodeCanvasTypeAttribute), false);
                if (OnlyInToolBarCanvasTypesAttributes.Length == 0)
                {
                    NodeCanvasTypeAttribute attr = nodeAttributes[0] as NodeCanvasTypeAttribute;
                    OnlyInToolBarCanvasTypes.Add(type, new NodeCanvasTypeData() { CanvasType = type, DisplayString = attr.Name });
                }
            }
        }

        /// <summary>
        /// Returns all recorded canvas definitions found by the system
        /// </summary>
        public static List<NodeCanvasTypeData> getCanvasDefinitions()
        {
            return CanvasTypes.Values.ToList();
        }

        /// <summary>
        /// Returns the NodeData for the given canvas
        /// </summary>
        public static NodeCanvasTypeData GetCanvasTypeData(NodeCanvas canvas)
        {
            return GetCanvasTypeData(canvas.GetType());
        }

        /// <summary>
        /// Returns the NodeData for the given canvas type
        /// </summary>
        public static NodeCanvasTypeData GetCanvasTypeData(Type canvasType)
        {
            NodeCanvasTypeData data;
            CanvasTypes.TryGetValue(canvasType, out data);
            return data;
        }

        /// <summary>
        /// Returns the NodeData for the given canvas name (type name, display string, etc.)
        /// </summary>
        public static NodeCanvasTypeData GetCanvasTypeData(string name)
        {
            return CanvasTypes.Values.FirstOrDefault((NodeCanvasTypeData data) =>
                    data.CanvasType.FullName.Contains(name) || data.DisplayString.Contains(name) || name.Contains(data.DisplayString));
        }

        /// <summary>
        /// Checks whether the süecified nodeID is compatible with the given canvas type
        /// 检测制定的Node ID是否与当前Canvas相匹配
        /// </summary>
        public static bool CheckCanvasCompability(string nodeID, Type canvasType)
        {
            NodeTypeData data = NodeTypes.GetNodeData(nodeID);
            if (data.limitToCanvasTypes == null || data.limitToCanvasTypes.Length == 0)
                return true;
            foreach (var type in data.limitToCanvasTypes)
            {
                if (type == canvasType || canvasType.IsSubclassOf(type))
                    return true;
            }
            return false;
        }

        #region Canvas Type Menu
        

#if UNITY_EDITOR
        public static void FillCanvasTypeMenu(ref UnityEditor.GenericMenu menu, Action<Type> NodeCanvasSelection, string path = "")
        {
            _menuCallback = NodeCanvasSelection;
            foreach (NodeCanvasTypeData data in OnlyInToolBarCanvasTypes.Values)
                menu.AddItem(new GUIContent(path + data.DisplayString), false, unwrapCanvasTypeCallback, (object) data);
        }
#endif

        private static void unwrapCanvasTypeCallback(object data)
        {
            NodeCanvasTypeData typeData = (NodeCanvasTypeData) data;
            _menuCallback(typeData.CanvasType);
        }

        #endregion
    }

    public struct NodeCanvasTypeData
    {
        public string DisplayString;
        public Type CanvasType;
    }

    public class NodeCanvasTypeAttribute: Attribute
    {
        public string Name;

        public NodeCanvasTypeAttribute(string displayName)
        {
            Name = displayName;
        }
    }

    public class CannotShowInToolBarCanvasTypeAttribute: Attribute
    {
    }
}