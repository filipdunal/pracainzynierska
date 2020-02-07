using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
namespace Threeyes.EventPlayer
{
    [Serializable]
    public class EventPlayerBehaviour : PlayableBehaviour
    {
        public EventPlayer eventPlayer;

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            if (eventPlayer)
                eventPlayer.Play();

            UpdateEPInfo(playable);
        }
        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            //Prevent get call on Tineline Start https://forum.unity.com/threads/release-event-player-visual-play-and-organize-unityevent.536984/#post-3605916
            double time = playable.GetGraph().GetRootPlayable(0).GetTime();
            if (time > 0)
            {
                if (eventPlayer)
                    eventPlayer.Stop();

                UpdateEPInfo(playable);
            }
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (!eventPlayer)
                return;

            UpdateEPInfo(playable);
        }

        void UpdateEPInfo(Playable playable)
        {
            if (eventPlayer is TimelineEventPlayer)
            {
                TimelineEventPlayer timelineEventPlayer = eventPlayer as TimelineEventPlayer;
                if (timelineEventPlayer)
                {
                    //Set Data
                    PlayableInfo playableInfo = timelineEventPlayer.playableInfo;
                    playableInfo.time = playable.GetTime();
                    playableInfo.duration = playable.GetDuration();

                    timelineEventPlayer.OnProcessFrame(playableInfo.percent);

#if UNITY_EDITOR
                    EventPlayer.RefreshEditor();
#endif
                }
            }
        }


    }

    [System.Serializable]
    public class PlayableInfo
    {
        public double time;
        public double duration;
        public float percent { get { return duration > 0 ? (float)(time / duration) : 0; } }
    }

}
