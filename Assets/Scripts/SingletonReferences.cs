using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonReferences : MonoBehaviour
{
    [SerializeField] MasterManager _masterManager;
    public MasterManager MasterManager
    {
        get { return _masterManager; }
    }
    public static SingletonReferences instance { get; set; }

    private void Start()
    {
        if(instance == null)
            instance = this;

        _masterManager._gameSettings.setPlayerVotesArray();
    }
}
