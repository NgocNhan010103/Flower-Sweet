using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildInfo : MonoBehaviour
{
    public int slotId;
    public int childId;

    public void InitDumy(int id, int childId)
    {
        this.slotId = id;
        this.childId = childId;

        GameObject child = Instantiate(
            UnitUtils.GetChildById(childId), transform.position, UnitUtils.GetChildById(childId).transform.rotation);
        child.transform.SetParent(this.transform);
    }
}
