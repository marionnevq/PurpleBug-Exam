using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour
{
    public enum Type 
    {
        land,
        flying
    }
    public Type type;
    [Range(0,100)] public float chanceToDrop;
}
