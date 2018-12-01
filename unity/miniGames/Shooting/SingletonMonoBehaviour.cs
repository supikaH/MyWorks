using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DefaultExecutionOrder(-1)]
public class SingletonMonoBehaviour<T> : MonoBehaviour where T:MonoBehaviour{

    private static T instance;

    public static T Instance {
        get {
            if (instance == null) {
                Type t = typeof(T);
                instance = (T)FindObjectOfType(t);
                if (instance == null) {
                    Debug.LogError(typeof(T) + "IS NULL");
                }
            }
            return instance;
        }
    }

    void Awake() {
        
    }

    protected bool CreateInstance(T ins) {
        if (instance == null) {
            instance = ins;
            return true;
        }
        else {
            Destroy(this);
            return false;
        }
    }
}
