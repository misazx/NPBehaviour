using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using NodeEditorFramework.Utilities;
using UnityEditor.Callbacks;

namespace NodeEditorFramework
{
    /// <summary>
    /// NodeTypeData相关操作
    /// </summary>
    public static partial class NodeTypes
    {
        /// <summary>
        /// 所有的NodeTypeData信息
        /// </summary>
        private static Dictionary<string, NodeTypeData> s_AllNodeTypeData;
        private static HashSet<string> s_Assembles = new HashSet<string>();
        public static HashSet<string> S_Assembles { get { return s_Assembles; } }

        [DidReloadScripts]
        public static void Listen()
        {
            OnInit();
        }

        private static void OnInit()
        {
            s_Assembles.Clear();
            var assmebles = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assemble in assmebles)
            {
                var types = assemble.GetTypes();
                foreach (var T in types)
                {
                    if((T.IsClass && !T.IsAbstract)
                        && T.GetCustomAttributes(typeof(NodeAssembleAttribute), false).Length > 0)
                    {
                        if (!s_Assembles.Contains(assemble.FullName))
                            s_Assembles.Add(assemble.FullName);
                    }
                }
            } 
             
            if (s_Assembles.Count.Equals(0))
            {
                s_Assembles.Add("Assembly");
            }
        }
         
        /// <summary>
        /// 获取所有自定义Node描述
        /// </summary> 
        public static void FetchNodeTypes()
        {
            s_AllNodeTypeData = new Dictionary<string, NodeTypeData>();
       
            foreach (Type type in ReflectionUtility.getSubTypes(typeof (Node)))
            {
                object[] nodeAttributes = type.GetCustomAttributes(typeof (NodeAttribute), false);
                NodeAttribute attr = nodeAttributes[0] as NodeAttribute;
                if (attr == null || !attr.hide)
                {
                    // Only regard if it is not marked as hidden
                    // Fetch node information
                    string ID, Title = "None";
                    FieldInfo IDField = type.GetField("ID");
                    if (IDField == null || attr == null)
                    {
                        // Cannot read ID from const field or need to read Title because of missing attribute -> Create sample to read from properties
                        Node sample = (Node) ScriptableObject.CreateInstance(type);
                        ID = sample.GetID;
                        Title = sample.Title;
                        UnityEngine.Object.DestroyImmediate(sample);
                    }
                    else // Can read ID directly from const field
                        ID = (string) IDField.GetValue(null);

                    // Create Data from information
                    NodeTypeData data = attr == null? // Switch between explicit information by the attribute or node information
                            new NodeTypeData(ID, Title, type, new Type[0]) :
                            new NodeTypeData(ID, attr.contextText, type, attr.limitToCanvasTypes);
                    s_AllNodeTypeData.Add(ID, data);
                }
            }
        }

        /// <summary>
        /// 获取所有的NodeTypeData
        /// </summary>
        public static List<NodeTypeData> GetAllNodeTypeData()
        {
            return s_AllNodeTypeData.Values.ToList();
        }

        /// <summary>
        /// 根据Node的ID返回NodeTypeData
        /// </summary>
        public static NodeTypeData GetNodeData(string typeID)
        {
            NodeTypeData data;
            s_AllNodeTypeData.TryGetValue(typeID, out data);
            return data;
        }

        /// <summary>
        /// 返回所有可以连接至选中port的Node ID
        /// 如果port为空，则返回所有Node ID
        /// </summary>
        public static List<string> GetCompatibleNodes(ConnectionPort port)
        {
            if (port == null)
                return NodeTypes.s_AllNodeTypeData.Keys.ToList();

            List<string> compatibleNodes = new List<string>();

            foreach (NodeTypeData nodeData in NodeTypes.s_AllNodeTypeData.Values)
            {
                // Iterate over all nodes to check compability of any of their connection ports
                if (ConnectionPortManager.GetPortDeclarations(nodeData.typeID).Any(
                    (ConnectionPortDeclaration portDecl) => portDecl.portInfo.IsCompatibleWith(port)))
                    compatibleNodes.Add(nodeData.typeID);
            }

            return compatibleNodes;
        }
    }

    /// <summary>
    /// 包含对于NodeEditor来说的Node的额外信息的数据类
    /// </summary>
    public struct NodeTypeData
    {
        /// <summary>
        /// TypeID（我们给定的类型ID）
        /// </summary>
        public string typeID;

        /// <summary>
        /// 我们限定的路径
        /// </summary>
        public string adress;

        /// <summary>
        /// Node的逻辑类型
        /// </summary>
        public Type type;

        /// <summary>
        /// 限定Canvas类型，只在这些Canvas中可用
        /// </summary>
        public Type[] limitToCanvasTypes;

        public NodeTypeData(string ID, string adress, Type nodeType, Type[] limitedCanvasTypes)
        {
            typeID = ID;
            this.adress = adress;
            type = nodeType;
            limitToCanvasTypes = limitedCanvasTypes;
        }
    }

    /// <summary>
    /// The NodeAttribute is used to specify editor specific data for a node type, later stored using a NodeData
    /// Node的attribute，可限定Canvas类型，后面会被NodeTypeData存储下来
    /// </summary>
    public class NodeAttribute: Attribute
    {
        public bool hide { get; private set; }
        public string contextText { get; private set; }
        public Type[] limitToCanvasTypes { get; private set; }

        public NodeAttribute(bool HideNode, string ReplacedContextText, params Type[] limitedCanvasTypes)
        {
            hide = HideNode;
            contextText = ReplacedContextText;
            limitToCanvasTypes = limitedCanvasTypes;
        }
    }

    public class NodeAssembleAttribute : Attribute
    {
    }

    [NodeAssemble()]
    public class NodeTypeAssembleDefine
    {
    }
}