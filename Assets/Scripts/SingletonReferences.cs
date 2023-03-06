using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonReferences : MonoBehaviour
{
    [SerializeField] MasterManager _masterManager;
    public static SingletonReferences instance;

}
