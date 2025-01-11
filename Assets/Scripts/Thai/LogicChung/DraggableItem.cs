using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static PointerEventData draggingItem;
    public static Sprite draggingItemSprite;
    public Transform currentParent;
    public bool hasPut = false;
    public bool backToHolder = false;
    private Transform originalParent;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (draggingItem != null || hasPut)
            return;
        Debug.Log("Begin Drag");
        backToHolder = false;
        currentParent = transform.parent;
        originalParent = transform.parent;
        transform.SetParent(transform.root, true);
        transform.SetAsLastSibling();
        GetComponent<Image>().raycastTarget = false;
        draggingItem = eventData;
        draggingItemSprite = GetComponent<Image>().sprite;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if ((draggingItem != null && draggingItem != eventData) || hasPut || backToHolder)
            return;
        Debug.Log("Dragging");
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if ((draggingItem != null && draggingItem != eventData) || backToHolder)
            return;
        Debug.Log("End Drag");
        transform.SetParent(currentParent, true);
        GetComponent<Image>().raycastTarget = true;
        draggingItem = null;
    }
    public void OnClickBackToAlphabetHolder()
    {
        if(!hasPut || (hasPut && !transform.Find("RedFx").gameObject.activeInHierarchy) || backToHolder)
            return;
        backToHolder = true;
        GameManager_SXChuCai.instance.currentAlphabetNumOnSlot--;
        //GetComponent<Image>().raycastTarget = false;
        transform.SetParent(transform.root, true);
        StartCoroutine(StartMoveBackToHolder());
    }
    private IEnumerator StartMoveBackToHolder()
    {
        while(Vector2.Distance(transform.position, originalParent.position) > .1f)
        {
            yield return new WaitForFixedUpdate();
            transform.position = Vector2.MoveTowards(transform.position, originalParent.position, 20f);
        }
        transform.SetParent(originalParent, true);
        //GetComponent<Image>().raycastTarget = true;
        transform.localScale /= (9f / 8f);
        transform.Find("RedFx").gameObject.SetActive(false);
        hasPut = false;
    }
    public void ReturnHolderForNextLv()
    {
        transform.SetParent(originalParent, true);
        transform.localScale /= (9f / 8f);
        transform.Find("RedFx").gameObject.SetActive(false);
        hasPut = false;
    }
}
