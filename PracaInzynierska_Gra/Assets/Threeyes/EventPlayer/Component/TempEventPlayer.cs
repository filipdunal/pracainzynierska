using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Threeyes.EventPlayer
{
    /// <summary>
    /// Invoke Play Event for a while, then Invoke Stop Event 
    /// </summary>
    public class TempEventPlayer : CoroutineEventPlayerBase
    {

        #region Property & Field

        [Header("Temp Setting")]
        [SerializeField]
        [Tooltip("Is Invoke Play Event on every frame, just like Update")]
        protected bool isContinuous = false;
        [SerializeField]
        [Tooltip("Total play time, if set it to less than 0, it will never stop")]
        protected float defaultDuration = -1;

        public bool IsContinuous { get { return isContinuous; } set { isContinuous = value; } }
        public float Duration { get { return defaultDuration; } set { defaultDuration = value; } }

        #endregion


        #region Method

        protected override void PlayFunc()
        {
            TempPlay(Duration);
        }

        public void TempPlay(float duration)
        {
            TryStopCoroutine();
            cacheEnum = CoroutineManager.StartCoroutineEx(IsContinuous ? IETempContinuousPlay(duration) : IETempPlay(duration));
        }


        IEnumerator IETempContinuousPlay(float duration)
        {
            float leftTime = duration;
            if (leftTime > 0)
            {
                while (leftTime >= 0)
                {
                    base.PlayFunc();
                    leftTime -= Time.deltaTime;
                    yield return null;
                }
                base.StopFunc();
            }
            else//Infinite
            {
                while (true)
                {
                    base.PlayFunc();
                    yield return null;
                }
            }
        }

        IEnumerator IETempPlay(float duration)
        {
            base.PlayFunc();

            if (Duration > 0)
            {
                yield return new WaitForSeconds(duration);
                base.StopFunc();
            }
        }

        #endregion

        #region Helper Method

#if UNITY_EDITOR

        static string instName = "TempEP ";

        [MenuItem("GameObject/EventPlayers/TempEventPlayer", false, 6)]
        public static void CreateDelayEventPlayer()
        {
            CreateObj<TempEventPlayer>(instName);
        }

        [MenuItem("GameObject/EventPlayers/TempEventPlayer Child", false, 7)]
        public static void CreateDelayEventPlayerChild()
        {
            CreateObjChild<TempEventPlayer>(instName);
        }
#endif

        #endregion

    }

}