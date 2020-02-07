using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Threeyes.EventPlayer
{
    /// <summary>
    /// Repeat Invoke Play Event
    /// </summary>
    public class RepeatEventPlayer : CoroutineEventPlayerBase
    {

        #region Property & Field

        [SerializeField]
        [Header("Repeat Setting")]
        [Tooltip("replay deltaTime, Only work when larger than 0")]
        protected float replayDeltaTime = -1;
        [SerializeField]
        [Tooltip("Total repeat time, if set it to less than 0, it will never stop")]
        protected float defaultDuration = -1;

        public float DeltaTime { get { return replayDeltaTime; } set { replayDeltaTime = value; } }
        public float Duration { get { return defaultDuration; } set { defaultDuration = value; } }


        #endregion

        #region Method

        protected override void PlayFunc()
        {
            if (DeltaTime > 0)
                RepeatPlay(DeltaTime, Duration);
            else
                base.PlayFunc();
        }

        public void RepeatPlay(float deltaTime, float duration)
        {
            TryStopCoroutine();
            cacheEnum = CoroutineManager.StartCoroutineEx(IERepeatPlay(DeltaTime, Duration));
        }

        IEnumerator IERepeatPlay(float deltaTime, float duration)
        {
            float startTime = Time.time;
            while (true)
            {
                if (Duration > 0)
                {
                    if (Time.time - startTime > duration)
                        base.StopFunc();
                }

                base.PlayFunc();
                yield return new WaitForSeconds(deltaTime);
            }
        }

        #endregion

        #region Helper Method

#if UNITY_EDITOR

        static string instName = "RepeatEP ";

        [MenuItem("GameObject/EventPlayers/RepeatEventPlayer", false, 4)]
        public static void CreateDelayEventPlayer()
        {
            CreateObj<RepeatEventPlayer>(instName);
        }

        [MenuItem("GameObject/EventPlayers/RepeatEventPlayer Child", false, 5)]
        public static void CreateDelayEventPlayerChild()
        {
            CreateObjChild<RepeatEventPlayer>(instName);
        }
#endif

        #endregion

    }

}