using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TransferChild : MonoBehaviour
{
    public BloomSlot[] slots;
    public GameObject detectedObject;
    public float detectionRadius = 1.5f;
    private bool hasSuccess = false;

    private Dictionary<int, BloomSlot> slotsBySlot;
    private void Start()
    {
        BloomSlot[] childSlots = GetComponentsInChildren<BloomSlot>();

        // Thêm các con vào mảng slots
        slots = slots.Concat(childSlots).ToArray();

        slotsBySlot = new Dictionary<int, BloomSlot>();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].id = i;
            slotsBySlot.Add(i, slots[i]);
        }
        if (gameObject.transform.parent.name== "SlotStart")
            TransferChildBegin();
        else
                TransferChildTo();

        }
        
    private void Update()
    {
        if (AllSlotsOccupied() && BloomSuccess() && !hasSuccess)
        {
            hasSuccess = true;
            Debug.Log(BloomSuccess());
            Destroy(gameObject, 1f);
            StartCoroutine(IncreaseCoinsOverTime());
            ExpLevelScore.Score += 100;
            ProgressBar.Instance.TickScore();
        }
        if (AllSlotsEmpty())
        {
            Destroy(gameObject,1f);
        }
        CallFunctionIfParentNotInSlots();
    }

    IEnumerator IncreaseCoinsOverTime()
    {
        for (int i = 0;i < 10; i++)
        {
            DataManager.Coins += 1;
            GameSharedUI.Instance.UpdateCoinUiText();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void CallFunctionIfParentNotInSlots()
    {
        if (IsParentInSlots())
        {
            DetectGameObjectsAround();
        }
    }

    public bool IsParentInSlots()
    {
        if (gameObject.transform.parent != null)
        {
            if (gameObject.transform.parent.name != "Realdy")
            {
                return true;
            }
        }
        return false;
    }

    public bool BloomSuccess()
    {
        string firstChildName = slots[0].transform.GetChild(0).name;

        for (int i = 1; i < slots.Length; i++)
        {
            string childName = slots[i].transform.GetChild(0).name;

            if (childName != firstChildName)
            {
                return false;
            }
        }

        return true;
    }


    bool AllSlotsEmpty()
    {
        foreach (var slot in slots)
        {
            if (slot.slotState == State.Full)
            {
                return false;
            }
        }
        return true;
    }

    public void TransferChildTo()
    {
        List<int> index = new List<int>();
        int ran = Random.Range(1, slots.Length);

        for (int i = 0;i < ran; i++)
        {
            
            int indexValue = Random.Range(0, UnitUtils.InitChildResources().items.Count);
            
            index.Add(indexValue);
        }

        index.Sort();

        for (int i = 0; i < index.Count; i++)
        {
            slots[i].CreateChild(index[i]);
        }
    }

    private void TransferChildBegin()
    {
        int indexValue1= Random.Range(0, UnitUtils.InitChildResources().items.Count);
        for (int i = 0;i < slots.Length-3;i++)
        {
            slots[i].CreateChild(indexValue1);
        }
        int indexValue2 = Random.Range(0, UnitUtils.InitChildResources().items.Count);
    while(indexValue1 == indexValue2)
            indexValue2 = Random.Range(0, UnitUtils.InitChildResources().items.Count);

        for (int i = 3; i < slots.Length; i++)
        {
            slots[i].CreateChild(indexValue2);
        }

    }

    public bool AllSlotsOccupied()
    {
        foreach (var slot in slots)
        {
            if (slot.slotState == State.Empty)
            {
                return false;
            }
        }
        return true;
    }

    BloomSlot GetSlotById(int index)
    {
        return slotsBySlot[index];
    }


    void DetectGameObjectsAround()
    {
       
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Bloom" && collider.gameObject.transform.position != gameObject.transform.position)
            {
                detectedObject = collider.gameObject;

                if (detectedObject != gameObject)
                {
                    if (gameObject.transform.parent != null && detectedObject.transform.parent != null)
                    {
                        TransferChild child = gameObject.GetComponent<TransferChild>();
                        TransferChild detectChild = detectedObject.GetComponent<TransferChild>();

                        if (child != null && detectChild != null)
                        {
                            child.CompareChild(detectChild);
                        }
                    }
                }
            }
            else
            {
                detectedObject = null;
            }
        }
    }

    void CompareChild(TransferChild child)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            for (int j = 0; j < child.slots.Length; j++)
            {
                if (slots[i].transform.childCount > 0 && child.slots[j].transform.childCount > 0)
                {
                    if (slots[i].transform.GetChild(0).name == child.slots[j].transform.GetChild(0).name)
                    {
                        BloomSlot targetSlot = GetEmptyAdjacentSlot(i); 

                        if (targetSlot != null && targetSlot.slotState == State.Empty)
                        {
                            Transform prefabTransform = child.slots[j].transform.GetChild(0);

                            if (prefabTransform != null)
                            {
                                prefabTransform.SetParent(targetSlot.transform);
                                prefabTransform.localRotation = Quaternion.Euler(-89.98f,0,0);
                                prefabTransform.localPosition = Vector3.zero;

                                targetSlot.slotState = State.Full;
                                child.slots[j].slotState = State.Empty;

                            }
                        }
                        else
                        {
                            Debug.Log("No empty slot available or target slot is full!");
                        }
                    }
                }
            }
        }
    }


    BloomSlot GetEmptyAdjacentSlot(int currentIndex)
    {
        // Tìm kiếm BloomSlot trống kế cạnh
        for (int k = currentIndex + 1; k < slots.Length; k++)
        {
            if (slots[k].slotState == State.Empty)
            {
                return slots[k];
                
            }
        }

        if (currentIndex != 0)
        {
            for (int l = currentIndex - 1; l >= 0; l--)
            {
                if (slots[l].slotState == State.Empty)
                {
                    return slots[l];
                }
            }
        }

        return null; // Trả về null nếu không tìm thấy BloomSlot trống
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
