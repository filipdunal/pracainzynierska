#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using Threeyes.Extension;

namespace Threeyes.EventPlayer
{
    /// <summary>
    /// Ref:
    /// http://ilkinulas.github.io/unity/2016/07/20/customize-unity-hierarchy-window.html
    /// https://forum.unity.com/threads/colors-colors-and-more-colors-please.499150/
    /// </summary>
    [InitializeOnLoad]
    public static partial class EventPlayerHierarchyView
    {

        private static StringBuilder sb = new StringBuilder();

        static EventPlayerHierarchyView()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
        }

        private static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (!go)
                return;

            EditorDrawerTool.RecordGUIColors();

            DrawEventPlayer(selectionRect, go);

            EditorDrawerTool.ResetGUIColors();

        }

        #region EventPlayer

        static Color colorSelfActive = Color.green;
        static Color colorSelfDeActive = Color.red;
        static Color colorGroupActive = colorSelfActive * 0.5f;
        static Color colorGroupDeActive = colorSelfDeActive * 0.5f;



        static bool isMouse0Down = false;

        private static void DrawEventPlayer(Rect selectionRect, GameObject go)
        {
            EventPlayer ep = go.GetComponent<EventPlayer>();
            if (!ep)
                return;


            #region 快捷按键操作

            //左键 + Alt 切换IsActive.
            if (EditorDrawerTool.CheckSelect(ref isMouse0Down, selectionRect, 0, () => Event.current.alt))
            {
                //只切换Hierarchy当前选中的单个物体
                ep.IsActive = !ep.IsActive;

                ////切换所有选中的物体
                //foreach (GameObject go in Selection.gameObjects)
                //{
                //    EventPlayer epTemp = go.GetComponent<EventPlayer>();
                //    if (epTemp)
                //        epTemp.IsActive = !epTemp.IsActive;
                //}
            }

            //中键调用TogglePlay函数
            if (EditorDrawerTool.CheckSelect(ref isMouse0Down, selectionRect, 2))
            {
                ep.TogglePlay();
            }

            #endregion

            //Check Active State
            bool isEPActive = ep.IsActive;
            bool isGroupActive = true;

            //Group
            //基于组的激活状态
            EventPlayer epg = ep.transform.FindFirstComponentInParent<EventPlayer>(false);
            if (epg && epg.IsGroup)//进一步的筛选
            {
                bool isManager = epg.IsRecursive || (!epg.IsRecursive && epg.transform == ep.transform.parent);//所获得的EPG是否为该EP的管理员
                if (isManager)
                {
                    if (epg.IsActive)
                    {
                        if (!epg.IsIncludeHide && !ep.gameObject.activeInHierarchy)
                        {
                            isGroupActive = false;
                        }
                    }
                    else
                    {
                        isGroupActive = false;
                    }
                }
            }

            Color colorBGActive = isGroupActive ? colorSelfActive : colorGroupActive;
            Color colorBGDeActive = isGroupActive ? colorSelfDeActive : colorGroupDeActive;
            GUI.backgroundColor = isEPActive ? colorBGActive : colorBGDeActive;//Toggle 背景颜色代表是否已激活

            Rect beginRect = EditorDrawerTool.GetRectAlignOnRight(selectionRect, EditorDrawerTool.toggleSize);
            //Toggle的Tick 代表是否已经Play
            bool isPlay = GUI.Toggle(beginRect, ep.IsPlayed, default(Texture));
            if (isPlay != ep.IsPlayed)
            {
                ep.Play(isPlay);
            }

            float usedSpace = EditorDrawerTool.CalculateLabelSize(ep.name).x + EditorDrawerTool.spaceSize.x;//已用空间：Label+间隔
            Rect remainRect = selectionRect;
            remainRect.x += usedSpace;
            remainRect.width -= usedSpace;
            DrawLabel(ep, beginRect, remainRect);
        }

        /// <summary>
        /// 显示EP的属性
        /// </summary>
        /// <param name="ep"></param>
        /// <param name="beginRect"></param>
        static void DrawLabel(EventPlayer ep, Rect beginRect, Rect remainRect)
        {
            //Format： [Property]EventPlayer_Type
            sb.Length = 0;
            string texProperty = ""; //Property Setting ， Suppressible
            string texEPType = "";//EventPlayer Type


            if (ep.IsPlayOnAwake)
                texProperty = "☆";
            if (ep.IsPlayOnce)
                texProperty += "①";
            if (ep.IsReverse)
                texProperty += "↓";

            TempEventPlayer tempEP = ep as TempEventPlayer;
            if (tempEP)
            {
                if (tempEP.IsContinuous)
                    texProperty += "©";

                texEPType += "T";
                AddSplit(ref texProperty, tempEP.Duration.ToString() + "s");
            }

            RepeatEventPlayer repeatEP = ep as RepeatEventPlayer;
            if (repeatEP)
            {
                texEPType += "R";
                AddSplit(ref texProperty, repeatEP.DeltaTime + "s" + ":" + repeatEP.Duration + "s");
            }
            DelayEventPlayer delayEP = ep as DelayEventPlayer;
            if (delayEP)
            {
                texEPType += "D";
                AddSplit(ref texProperty, delayEP.DelayTime.ToString() + "s");
            }

            DrawTimelineEventPlayer(ep, ref texProperty, ref texEPType);

            //Draw Group
            EventPlayer epg = ep as EventPlayer;
            if (epg && epg.IsGroup)
            {
                texEPType += "G";
                string cacheText = "";
                if (epg.IsIncludeHide)
                    cacheText += "H";
                if (epg.IsRecursive)
                    cacheText += "R";
                AddSplit(ref texProperty, cacheText);
            }


            //组合字符串
            //如果没有越界，则显示
            if (texProperty.NotNullOrEmpty())
                sb.Append("[").Append(texProperty).Append("]");

            if (texEPType.NotNullOrEmpty())
                sb.Append(" " + texEPType);

            if (sb.Length > 0)
            {
                GUIStyle gUIStyle = new GUIStyle();

                gUIStyle.fontSize = EditorDrawerTool.fontSize;
                gUIStyle.alignment = TextAnchor.MiddleRight;
                gUIStyle.normal.textColor = Color.blue;

                Rect labelRect = new Rect(beginRect);
                labelRect.width = EditorDrawerTool.CalculateLabelSize(sb.ToString()).x;

                //Overflow： Show texEPType only
                if (labelRect.width > remainRect.width)
                {
                    sb.Length = 0;
                    sb.Append(texEPType);
                }
                labelRect.x -= labelRect.width;
                labelRect.height = EditorDrawerTool.spaceSize.y;

                GUI.backgroundColor = Color.gray * 0.5f;
                labelRect.x -= 2;
                GUI.Label(labelRect, sb.ToString(), gUIStyle);
            }
        }

        static partial void DrawTimelineEventPlayer(EventPlayer ep, ref string texProperty, ref string texEPType);

        static void AddSplit(ref string souStr, string addStr)
        {
            if (souStr.NotNullOrEmpty())
                souStr += "|";
            souStr += addStr;
        }

        #endregion

    }

}
#endif

