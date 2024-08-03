
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Item item;
    public Transform parentAfterDrag;
    private int closestSlot = -1;
    private int itemIndex;
    private void Start()
    {
        item = gameObject.GetComponent<InventoryItemController>().item;
    }
    public void SetItem(Item newItem)
    {
        item = newItem;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemIndex = FindItemIndex(item);
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        GetComponent<Image>().raycastTarget = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        closestSlot = GetClosestSlot();

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (closestSlot >= 0 && closestSlot < InventoryManager.Instance.itemSlots.Length)
        {
            if (InventoryManager.Instance.itemsArray[closestSlot] == null)
            {
                transform.SetParent(InventoryManager.Instance.itemSlots[closestSlot].transform);
                transform.localPosition = Vector3.zero;
                InventoryManager.Instance.itemsArray[itemIndex] = null;
                InventoryManager.Instance.itemsArray[closestSlot] = item;
                InventoryManager.Instance.ListItems();
            }
            else
            {

                {
                    transform.SetParent(InventoryManager.Instance.itemSlots[closestSlot].transform);
                    transform.localPosition = Vector3.zero;
                    var holder = InventoryManager.Instance.itemsArray[closestSlot];
                    InventoryManager.Instance.itemsArray[itemIndex] = holder;
                    InventoryManager.Instance.itemsArray[closestSlot] = item;
                    InventoryManager.Instance.ListItems();


                }
            }
        }
        else
        {
            Destroy(transform.gameObject);
            transform.localPosition = Vector3.zero;
            transform.SetParent(parentAfterDrag);
        }

        GetComponent<Image>().raycastTarget = true;
    }


    private int GetClosestSlot()
    {
        int closestSlotIndex = -1;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < InventoryManager.Instance.itemSlots.Length; i++)
        {
            float distance = Vector2.Distance(InventoryManager.Instance.itemSlots[i].transform.position, Input.mousePosition);
            if (distance <= 30 && distance < closestDistance)
            {
                closestDistance = distance;
                closestSlotIndex = i;
            }
        }

        return closestSlotIndex;
    }

    private int FindItemIndex(Item item)
    {
        for (int i = 0; i < InventoryManager.Instance.itemsArray.Length; i++)
        {
            if (item == InventoryManager.Instance.itemsArray[i])
                return i;
        }
        return -1;
    }
}
