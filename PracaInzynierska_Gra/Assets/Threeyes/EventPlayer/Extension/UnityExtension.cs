using UnityEngine;
using UnityEngine.Events;

namespace Threeyes.Extension
{
    public static class UnityExtension
    {

        #region String

        public static bool NotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        #endregion

        #region Transform

        /// <summary>
        /// ForAll Child
        /// </summary>
        /// <param name="tf"></param>
        /// <param name="action"></param>
        /// <param name="includeSelf"></param>
        public static void ForEachChild(this Transform tf, UnityAction<Transform> action, bool includeSelf = true)
        {
            if (includeSelf)
                action(tf);

            foreach (Transform tfChild in tf.transform)
            {
                action(tfChild);
            }
        }

        public static void ForEachParent(this Transform tf, UnityAction<Transform> action, bool includeSelf = true)
        {
            Transform target = tf;
            if (includeSelf)
                action(tf);

            while (target.parent)
            {
                action(target.parent);
                target = target.parent;
            }
        }

        /// <summary>
        /// Recursive
        /// </summary>
        /// <param name="tf"></param>
        /// <param name="action"></param>
        /// <param name="includeSelf"></param>
        public static void Recursive(this Transform tf, UnityAction<Transform> action, bool includeSelf = true)
        {
            if (includeSelf)
                action(tf);

            foreach (Transform tfChild in tf.transform)
            {
                Recursive(tfChild, action);
            }
        }

        #endregion

        #region Component

        public static TReturn FindFirstComponentInParent<TReturn>(this Component comp, bool includeSelf = true)
    where TReturn : Component
        {
            TReturn tReturn = null;
            comp.transform.ForEachParent(
               (tf) =>
               {
                   if (!tReturn)
                       tReturn = tf.GetComponent<TReturn>();
               },
               includeSelf
               );
            return tReturn;
        }

        /// <summary>
        /// Clone Component 
        /// Ref: https://answers.unity.com/questions/458207/copy-a-component-at-runtime.html
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public static T CopyComponent<T>(this T original, T dest) where T : Component
        {
            System.Type type = original.GetType();

            var fields = type.GetFields();
            foreach (var field in fields)
            {
                if (field.IsStatic) continue;
                field.SetValue(dest, field.GetValue(original));
            }
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name") continue;
                prop.SetValue(dest, prop.GetValue(original, null), null);
            }
            return dest as T;
        }


        #endregion
    }
}