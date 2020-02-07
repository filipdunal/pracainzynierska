using UnityEngine;
using UnityEngine.Events;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Threeyes.Extension;
namespace Threeyes.EventPlayer
{
    /// <summary>
    /// Manage child EventPlayer
    /// </summary>
    [Obsolete("Use EventPlayer + isGroup=true instead")]
    public class EventPlayerGroup : EventPlayer
    {
        private void OnValidate()
        {
            //Default Set IsGroup as true
            if (!IsGroup)
            {
                IsGroup = true;
            }
        }
    }

}