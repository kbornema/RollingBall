using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SingletonDestroyMode { GameObject, Script, Nothing }

/// <summary>Helper class that defines an abstract singleton inheriting from MonoBehaviour </summary>
/// <typeparam name="T">The inheriting class.</typeparam>
public abstract class AManager<T> : MonoBehaviour where T : class
{
    public static T Instance { get; private set; }
    
    [SerializeField]
    private SingletonDestroyMode destroyMode;

    protected abstract void OnAwake();

    public void Awake()
    {
        if (Instance != null)
        {
            if (destroyMode == SingletonDestroyMode.GameObject)
            {
                Destroy(gameObject);
                return;
            }

            else if (destroyMode == SingletonDestroyMode.Script)
            {
                Destroy(this);
                return;
            }

            else
            {
                //Debug.Log("Multiple instances of " + gameObject.name + " detected. Intended?!");
            }
        }

        else
        {
            Instance = this as T;
            OnAwake();
        }
    }
}
