#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Threeyes.EventPlayer
{

    [CustomEditor(typeof(EventPlayerClip))]
    public class EventPlayerClipEditor : BaseClipEditor<EventPlayerClip>
    {
        private SerializedProperty mserialProperty = null;
        private static readonly GUIContent tipsLabel = new GUIContent("Event Player", "Event Player / TimelineEventPlayer");

        /// <summary>
        /// 需要排除的物体
        /// </summary>
        /// <returns></returns>
        protected override List<string> GetExcludedPropertiesInInspector()
        {
            List<string> excluded = base.GetExcludedPropertiesInInspector();
            excluded.Add(FieldPath(x => x.eventPlayer));
            return excluded;
        }

        protected virtual void OnEnable()
        {
            if (serializedObject != null)
                mserialProperty = FindProperty(x => x.eventPlayer);
        }

        public override void OnInspectorGUI()
        {
            BeginInspector();
            EditorGUI.indentLevel = 0; // otherwise subeditor layouts get screwed up

            Rect rect;
            EventPlayer ep = mserialProperty.exposedReferenceValue as EventPlayer;
            if (ep != null)
                EditorGUILayout.PropertyField(mserialProperty, tipsLabel);
            else
            {
                GUIContent createLabel = new GUIContent("Create");
                Vector2 createSize = GUI.skin.button.CalcSize(createLabel);

                rect = EditorGUILayout.GetControlRect(true);
                rect.width -= createSize.x;

                EditorGUI.PropertyField(rect, mserialProperty, tipsLabel);
                rect.x += rect.width; rect.width = createSize.x;
                if (GUI.Button(rect, createLabel))
                {
                    ep = EventPlayer.CreateEventPlayer();
                    mserialProperty.exposedReferenceValue = ep;
                }
                serializedObject.ApplyModifiedProperties();
            }

            DrawRemainingPropertiesInInspector();

            if (ep != null)
                DrawSubEditors(ep);
        }
    }
}
#endif