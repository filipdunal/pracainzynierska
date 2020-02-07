using UnityEngine;
using UnityEngine.Events;
using Threeyes.Extension;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Threeyes.EventPlayer
{

    /// <summary>
    /// Base Component that support some basic event
    /// </summary>
    public class EventPlayer : MonoBehaviour
    {

        #region Property & Field

        #region Common

        //[Header("Debug Log")]
        //public bool isLogOnPlay = false;
        //public bool isLogOnStop = false;

        /// <summary>
        /// Emitted when Play/Stop Method is Invoked
        /// </summary>
        public BoolEvent onPlayStop;

        /// <summary>
        /// Emitted when Play Method is Invoked
        /// </summary>
        public UnityEvent onPlay;
        /// <summary>
        /// Emitted when Stop Method is Invoked
        /// </summary>
        public UnityEvent onStop;

        [SerializeField]
        [Tooltip("Is this EventPlayer avaliable")]
        protected bool isActive = true;
        [SerializeField]
        [Tooltip(" Play Method on Awake")]
        protected bool isPlayOnAwake = false;
        [SerializeField]
        [Tooltip("The Play Method can only be Invoked once")]
        protected bool isPlayOnce = false;
        [SerializeField]
        [Tooltip("Reverse the Play/Stop behaviour")]
        protected bool isReverse = false;

        /// <summary>
        /// Cache the play state
        /// </summary>
        protected bool isPlayed = false;

        public bool IsActive { get { return isActive; } set { isActive = value; } }
        public bool IsPlayOnAwake { get { return isPlayOnAwake; } set { isPlayOnAwake = value; } }
        public bool IsPlayOnce { get { return isPlayOnce; } set { isPlayOnce = value; } }
        public bool IsReverse { get { return isReverse; } set { isReverse = value; } }
        public bool IsPlayed { get { return isPlayed; } }

        #endregion

        #region Group

        [Header("Group Setting")]
        [SerializeField]
        protected bool isGroup = false;

        [SerializeField]
        [Tooltip("Is Invoke the child component evenif the GameObject is deActive in Hierarchy")]
        protected bool isIncludeHide = true;
        [SerializeField]
        [Tooltip("Is recursive find the child component")]
        protected bool isRecursive = false;

        public bool IsGroup { get { return isGroup; } set { isGroup = value; } }
        public bool IsIncludeHide { get { return isIncludeHide; } set { isIncludeHide = value; } }
        public bool IsRecursive { get { return isRecursive; } set { isRecursive = value; } }

        #endregion

        #endregion

        #region Method

        [ContextMenu("Play")]
        public void Play()
        {
            Play(true);
        }

        [ContextMenu("Stop")]
        public void Stop()
        {
            Play(false);
        }

        [ContextMenu("TogglePlay")]
        public void TogglePlay()
        {
            Play(!isPlayed);
        }

        [ContextMenu("ResetState")]
        public virtual void ResetState()
        {
            isPlayed = false;
        }

        public virtual void Play(bool isPlay)
        {
            if (!IsActive)
                return;

            if (isPlay && !IsReverse || !isPlay && IsReverse)//Play
            {
                if (IsPlayOnce)
                {
                    if (isPlayed)
                        return;
                }
                PlayFunc();
            }
            else if (!isPlay && !IsReverse || isPlay && IsReverse)//Stop
            {
                StopFunc();
            }
        }

        /// <summary>
        /// Execute the related play event
        /// </summary>
        protected virtual void PlayFunc()
        {
            onPlayStop.Invoke(true);
            onPlay.Invoke();

            if (IsGroup)
                ForEachChildComponent<EventPlayer>((ep) =>
                {
                    ep.Play();
                });

            SetState(true);

#if UNITY_EDITOR
            //if (isLogOnPlay)
            //    print(name + " Play!");
            RefreshEditor();
#endif

        }

        /// <summary>
        /// Execute the related stop event
        /// </summary>
        protected virtual void StopFunc()
        {
            onPlayStop.Invoke(false);
            onStop.Invoke();
            if (IsGroup)
                ForEachChildComponent<EventPlayer>((ep) =>
                {
                    ep.Stop();
                });

            SetState(false);

#if UNITY_EDITOR
            //if (isLogOnStop)
            //    print(name + " Stop!");
            RefreshEditor();
#endif

        }

        protected virtual void SetState(bool isPlayed)
        {
            this.isPlayed = IsReverse ? !isPlayed : isPlayed;
        }


        protected virtual void ForEachChildComponent<T>(UnityAction<T> func) where T : Component
        {
            UnityAction<Transform> sonFunc = (tf) =>
            {
                T[] arrT = tf.GetComponents<T>();//In case some GameObject contains multi Component
                foreach (T t in arrT)
                {
                    if (IsIncludeHide)
                    {
                        func(t);
                    }
                    else
                    {
                        if (t.gameObject.activeInHierarchy)
                            func(t);
                    }
                }
            };

            if (IsRecursive)
                transform.Recursive(sonFunc, false);
            else
                transform.ForEachChild(sonFunc, false);
        }

        #endregion

        #region Unity Method

        private void Awake()
        {
            if (IsPlayOnAwake)
            {
                Play();
            }
        }

        #endregion

        #region Helper Method

#if UNITY_EDITOR

        static BoolEvent cacheOnPlayStop;
        static UnityEvent cacheOnPlay;
        static UnityEvent cacheOnStop;
        [ContextMenu("CopyEvent")]
        public void CopyEvent()
        {
            cacheOnPlayStop = onPlayStop;
            cacheOnPlay = onPlay;
            cacheOnStop = onStop;
        }


        [ContextMenu("ParseEvent")]
        public void ParseEvent()
        {
            onPlayStop = cacheOnPlayStop;
            onPlay = cacheOnPlay;
            onStop = cacheOnStop;
        }

        [ContextMenu("ConvertToEventPlayer")]
        public void ConverToEventPlayer()
        {
            EventPlayer cur = this;

            EventPlayer newEP = gameObject.GetComponent<EventPlayer>();
            if (!newEP || newEP.GetType().IsSubclassOf(typeof(EventPlayer)))//检查当前物体中是否已有EventPlayer的子类
                newEP = Undo.AddComponent<EventPlayer>(gameObject);
            this.CopyComponent(newEP);

            Undo.DestroyObjectImmediate(cur);

        }


        /// <summary>
        /// RefreshEditorGUI
        /// </summary>
        public static void RefreshEditor()
        {
            EditorApplication.RepaintHierarchyWindow();
        }

        #region Experimental

        //[ContextMenu("ConvertToEventPlayerGroup")]
        //public void ConvertToEventPlayerGroup()
        //{
        //    Undo.RegisterCompleteObjectUndo(gameObject, "ConvertToEventPlayerGroup");
        //    EventPlayer cur = this;

        //    //可以参考UnityCsReference\Editor\Mono\Inspector\UnityEventDrawer
        //    EventPlayerGroup eventPlayerGroup = gameObject.AddComponentOnce<EventPlayerGroup>();
        //    for (int i = 0; i != cur.onPlayStop.GetPersistentEventCount(); i++)
        //    {
        //        EventPlayer epChild = CreateObj<EventPlayer>(EventPlayer.instName, transform);

        //        //研究：赋值 再逐一删除
        //        epChild.onPlayStop = eventPlayerGroup.onPlayStop;
        //        for (int j = i + 1; j != cur.onPlayStop.GetPersistentEventCount(); j++)
        //        {
        //            UnityEventTools.RemovePersistentListener(epChild.onPlayStop, i + 1);//移除后面的事件
        //        }
        //        for (int j = 0; j != i; j++)
        //        {
        //            UnityEventTools.RemovePersistentListener(epChild.onPlayStop, 0);//移除后面的事件
        //        }
        //    }

        //for (int i = 0; i != cur.onPlay.GetPersistentEventCount(); i++)
        //{
        //    EventPlayer epChild = CreateObj<EventPlayer>(EventPlayer.instName, transform);

        //    UnityEvent unityEvent = epChild.onPlay;
        //    CustomEvent cacheEvent = new CustomEvent();
        //    Object objTarget = unityEvent.GetPersistentTarget(i);
        //    cacheEvent.AddListenerEx(objTarget, UnityEventBase.GetValidMethodInfo(objTarget, unityEvent.GetPersistentMethodName(i), new System.Type[] { }));
        //    epChild.onPlay = cacheEvent;
        //}

        //}

        #endregion

        //Order: EventPlayer DelayEventPlayer RepeatEventPlayer TempEventPlayer TimelineEventPlayer
        static string instName = "EP ";
        [MenuItem("GameObject/EventPlayers/EventPlayer  %#e", false, 0)]
        public static EventPlayer CreateEventPlayer()
        {
            return CreateObj<EventPlayer>(instName);
        }

        [MenuItem("GameObject/EventPlayers/EventPlayer Child  &#e", false, 1)]
        public static EventPlayer CreateEventPlayerChild()
        {
            return CreateObjChild<EventPlayer>(instName);
        }

        static string instGroupName = "EPG ";

        [MenuItem("GameObject/EventPlayers/EventPlayerGroup %#g", false, 100)]
        public static void CreateEventPlayerGroup()
        {
            EventPlayer eventPlayer = CreateObj<EventPlayer>(instGroupName);
            eventPlayer.IsGroup = true;
        }

        [MenuItem("GameObject/EventPlayers/EventPlayerGroup Child &#g", false, 101)]
        public static void CreateEventPlayerGroupChild()
        {
            EventPlayer eventPlayer = CreateObjChild<EventPlayer>(instGroupName);
            eventPlayer.IsGroup = true;
        }


        public static T CreateObj<T>(string name, Transform tfParent = null) where T : Component
        {
            GameObject go = new GameObject(name);
            T com = go.AddComponent<T>();

            if (!tfParent)//Same parent as Selection
                tfParent = Selection.activeGameObject ? Selection.activeGameObject.transform.parent : null;

            go.transform.parent = tfParent;
            if (Selection.activeGameObject)
                go.transform.SetSiblingIndex(Selection.activeGameObject.transform.GetSiblingIndex() + 1);

            Undo.RegisterCreatedObjectUndo(go, "Create object");
            Selection.activeGameObject = go;

            return com;
        }

        public static T CreateObjChild<T>(string name) where T : Component
        {
            return CreateObj<T>(name, Selection.activeGameObject ? Selection.activeGameObject.transform : null);
        }

#endif

        #endregion

    }

    #region Definition

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool>
    {

    }

    #endregion

}