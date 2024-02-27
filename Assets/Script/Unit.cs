using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int idUnit;
    public Slot parentSlot;
    public int slotId;
    public BloomSlot bloomSlot;
    public Vector3 initialPosition;

    public void Init(int slotId,int id, Slot slot)
    {
        this.slotId = slotId;
        this.idUnit = id;
        this.parentSlot = slot;
    }

    public void InitChild(int id, BloomSlot slot)
    {
        this.idUnit = id;
        this.bloomSlot = slot;
    }

    public void ReturnToInitialPosition()
    {
        transform.position = initialPosition;
    }
}
