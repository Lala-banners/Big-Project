using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A class that is going to make runnables easier
namespace BreadAndButter
{
    public class RunnableHelper
    {
        /// <summary>
        /// Attempts to retreive the runnable behaviour from the passed game object or its children.
        /// </summary>
        /// <param name="_runnable">The reference to runnable wukk be set to.</param>
        /// <param name="_from">The gameObject we are attempting to get a runnable from.</param>
        /// <returns></returns>
        public static bool Validate<T>(ref T _runnable, GameObject _from) where T : IRunnable
        {
            //If the passed runnable is already set, return true
            if (_runnable != null)
            {
                return true;   
            }

            //If the passed runnable isn't set, attempt to get it from the gameObject
            if (_runnable == null)
            {
                _runnable = _from.GetComponent<T>();

                //We successfully retreived the component, so return true
                if (_runnable != null)
                {
                    return true;
                }
            }

            //If this is not the case, attempt to get it from the gameObject children
            if (_runnable == null)
            {
                _runnable = _from.GetComponentInChildren<T>();

                //We successfully retreived the component, so return true
                if (_runnable != null)
                {
                    return true;
                }
            }

            //We failed to get any instance of the runnable, so return false
            return false;
        }

        /// <summary>
        /// Attempts to validate then set up IRunnable, returning whether or not it succeeded
        /// </summary>
        /// <param name="_runnable">The runnable being set up</param>
        /// <param name="_from">The gameObject the runnable is attached to</param>
        /// <param name="_params">Any additional info from Runnable's setup function needs</param>
        public static bool Setup<T>(ref T _runnable, GameObject _from, params object[] _params) where T : IRunnable
        {
            //Validate the component, if we can, set it up and return true
            if (Validate(ref _runnable, _from))
            {
                _runnable.Setup(_params);
                return true;
            }

            //We failed to validate the component, so return false
            return false;
        }

        /// <summary>
        /// Attempts to validate the runnable, returning whether or not it succeeded
        /// </summary>
        /// <param name="_runnable">The runnable being set up</param>
        /// <param name="_from">The gameObject the runnable is attached to</param>
        /// <param name="_params">Any additional info from Runnable's Run function needs</param>
        public static void Run<T>(ref T _runnable, GameObject _from, params object[] _params) where T : IRunnable
        {
            if (Validate(ref _runnable, _from))
            {
                _runnable.Run(_params);
            }
        }
    }
}
