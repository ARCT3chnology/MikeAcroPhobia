using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ScriptableObjectsSingleton<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance = null;

    public static T Instance
    {
        get 
        {
            if (_instance == null)
            {
                T[] results = Resources.FindObjectsOfTypeAll<T>();
                if(results.Length == 0) 
                {
                    Debug.LogError("ScriptableSingletonObject -> Instance -> results lenght is 9 for type " + typeof(T).ToString() + " .");
                    return null;
                }
                if (results.Length > 1)
                {
                    Debug.LogError("ScriptableSingletonObject -> Instance -> results lenght is greater than one for type " + typeof(T).ToString() + " .");
                    return null;
                }
                _instance = results[0];
            }
            return _instance; }
    }

}
