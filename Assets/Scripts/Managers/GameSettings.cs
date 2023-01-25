using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "manager/GameSetting")]
public class GameSettings : ScriptableObject
{
    [SerializeField] string _gameVersion;
    public string GameVersion { get { return _gameVersion; } }
    [SerializeField] string _nickName;
    public string NickName
    {
        get 
        {
            int val = Random.Range(0, 999);
            return _nickName+val.ToString(); 
        }
    }
}
