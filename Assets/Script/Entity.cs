using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] public TypePetal typePetal;
    

    public void ChangeTypePetal(TypePetal type)
    {
        typePetal = type;
    }
}

public enum TypePetal { 
    Orange,
    Red,
    Yelow,
    Purple,
    Blue,
    Green

}
