using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class UnitUtils 
{
    public static GameResources prefabResources;
    public static GameResources childResources;
    public static GameResources unitResources;
    public static GameResources InitResources()
    {
        prefabResources = Resources.Load<GameResources>("Bloom Resources/Bloom");

        if (prefabResources == null)
        {
            Debug.LogError("Failed to load Resources.");
        }

        return prefabResources; 
    }

    public static GameResources InitChildResources()
    {
        childResources = Resources.Load<GameResources>("Bloom Resources/Child");

        if (childResources == null)
        {
            Debug.LogError("Failed to load Resources.");
        }
        return childResources;
    }


    public static void SetResources(GameObject prefab)
    {
        unitResources = Resources.Load<GameResources>("Bloom Resources/CurentUnit");
      
        unitResources.items.Add(prefab);

    }

    public static void Remove(int id)
    {
        unitResources = Resources.Load<GameResources>("Bloom Resources/CurentUnit");

        unitResources.items.Remove(unitResources.items[id]);

    }

    public static GameObject GetCurrentUnit(int id)
    {
        if (unitResources == null)
        {
            return null;
        }
        return unitResources.items[id];
    } 

    public static GameObject GetUnitById(int id)    
    {
        if (prefabResources == null)
        {
            return null;
        }
        return prefabResources.items[id];
    }

    public static GameObject GetChildById(int id)
    {
        if (childResources == null)
        {
            return null;
        }
        return childResources.items[id];
    }

}
