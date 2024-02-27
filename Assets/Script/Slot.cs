using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class Slot : MonoBehaviour
{   
    public int slotId;
    public Unit currentUnit;
    public SlotState slotState = SlotState.Empty;

    private void Update()
    {
        if (currentUnit == null)
        {
            ChangeStateTo(SlotState.Empty);
        }
        if (slotState == SlotState.Empty)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 1.21f);
        }
    }
    public void CreateUnit(int id)
    {
        GameObject bloom = UnitUtils.GetUnitById(id);

        if (bloom != null)
        {
            GameObject prefab = Instantiate(
                bloom,transform.position, bloom.transform.rotation);
            prefab.transform.SetParent(this.transform);
            prefab.transform.localPosition = new Vector3(0, 1.37f, 0);
            
            currentUnit = prefab.GetComponent<Unit>();
            if (currentUnit != null)
            {
                currentUnit.Init(slotId,id,this);
            }

            ChangeStateTo(SlotState.Full);
        }
    }

    public void ChangeStateTo(SlotState targetState)
    {
        slotState = targetState;
    }


    public void ItemPlaced()
    {
        currentUnit = null;
        ChangeStateTo(SlotState.Empty);
    }
}

public enum SlotState
{
    Empty,
    Full
}
