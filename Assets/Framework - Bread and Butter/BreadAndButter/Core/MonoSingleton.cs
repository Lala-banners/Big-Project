using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NullReferenceException = System.NullReferenceException;

namespace BreadAndButter
{
    //Type of framework component in core 
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> //Any type that is passed into T needs to be MonoSingleton
    {
        public static T Instance
        {
            get 
            {
                //The internal instance isn't set so attempt to find it from the scene
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    //No instance was found so throw new NullReferenceException detailing what singleton caused the error.
                    if (instance == null)
                    {
                        throw new NullReferenceException(string.Format("No object of type: {0} was found.", typeof(T).Name)); //Print out class name
                    }
                }

                return instance;
            }
        }

        //The property wraps this variable
        private static T instance = null;

        /// <summary>
        /// Has the singleton been generated?
        /// </summary>
        public static bool IsSingletonValid() => instance != null;

        /// <summary>
        /// Finds the instance within the scene
        /// </summary>
        protected T CreateInstance() => Instance; //When in doubt LAMBDA AWAY

        /// <summary>
        /// Force the singleton instance to not be destroyed on scene load
        /// </summary>
        public static void FlagAsPersistant() => DontDestroyOnLoad(Instance.gameObject);
    }
}
