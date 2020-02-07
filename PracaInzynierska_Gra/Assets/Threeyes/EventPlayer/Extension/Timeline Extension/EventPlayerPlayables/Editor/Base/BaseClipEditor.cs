#if UNITY_EDITOR

namespace Threeyes.EventPlayer
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    /// <summary>
    /// 绘制Clip所引用的其他剩余组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseClipEditor<T> : BaseEditor<T> where T : class
    {

        protected virtual void OnDisable()
        {
            DestroyComponentEditors();
        }

        protected void DrawSubEditors(Component comp)
        {
            // Create an editor for each of the cinemachine virtual cam and its components
            GUIStyle foldoutStyle;
            foldoutStyle = EditorStyles.foldout;
            foldoutStyle.fontStyle = FontStyle.Bold;
            UpdateComponentEditors(comp);

            if (m_editors != null)
            {
                foreach (Editor e in m_editors)
                {
                    if (e == null || e.target == null)
                        continue;

                    // Separator line - how do you make a thinner one?
                    GUILayout.Box("", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(1) });

                    bool expanded = true;
                    if (!s_EditorExpanded.TryGetValue(e.target.GetType(), out expanded))
                        expanded = true;
                    expanded = EditorGUILayout.Foldout(expanded, e.target.GetType().Name, true, foldoutStyle);
                    if (expanded)
                        e.OnInspectorGUI();
                    s_EditorExpanded[e.target.GetType()] = expanded;
                }
            }
        }

        Component m_cachedReferenceObject;
        UnityEditor.Editor[] m_editors = null;
        static Dictionary<System.Type, bool> s_EditorExpanded = new Dictionary<System.Type, bool>();

        void UpdateComponentEditors(Component comp)
        {
            MonoBehaviour[] components = null;
            if (comp != null)
                components = comp.gameObject.GetComponents<MonoBehaviour>();

            int numComponents = (components == null) ? 0 : components.Length;
            int numEditors = (m_editors == null) ? 0 : m_editors.Length;

            if (m_cachedReferenceObject != comp || (numComponents + 1) != numEditors)
            {
                DestroyComponentEditors();
                m_cachedReferenceObject = comp;
                if (comp != null)
                {
                    m_editors = new Editor[components.Length + 1];
                    //CreateCachedEditor(obj.gameObject.GetComponent<Transform>(), null, ref m_editors[0]);
                    for (int i = 0; i < components.Length; ++i)
                        CreateCachedEditor(components[i], null, ref m_editors[i + 1]);
                }
            }
        }

        protected void DestroyComponentEditors()
        {
            m_cachedReferenceObject = null;
            if (m_editors != null)
            {
                for (int i = 0; i < m_editors.Length; ++i)
                {
                    if (m_editors[i] != null)
                        UnityEngine.Object.DestroyImmediate(m_editors[i]);
                    m_editors[i] = null;
                }
                m_editors = null;
            }
        }

    }

}
#endif