using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlphabetDroppbleSlot : MonoBehaviour, IDropHandler
{
    private Transform alphabetBlur;
    private GameObject item;
    private void Start()
    {
        alphabetBlur = transform.Find("Alphabet_blur");
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.Find("Alphabet"))
            return;
        Debug.Log("Put!");
        item = eventData.pointerDrag;
        if (item.GetComponent<DraggableItem>().hasPut || item.GetComponent<DraggableItem>().backToHolder)
            return;
        item.transform.localScale *= (9f / 8f);
        item.GetComponent<DraggableItem>().currentParent = transform;
        item.GetComponent<DraggableItem>().hasPut = true;
        alphabetBlur.gameObject.SetActive(false);
        CheckAlphabet();
    }
    private void Update()
    {
        CheckDragOnSlot();
    }

    private void CheckDragOnSlot()
    {
        if (transform.Find("Alphabet"))
            return;
        if (DraggableItem.draggingItem != null)
        {
            if (Vector2.Distance(DraggableItem.draggingItem.position, transform.position) < 45f)
            {
                alphabetBlur.GetComponent<Image>().sprite = DraggableItem.draggingItemSprite;
                alphabetBlur.gameObject.SetActive(true);
            }
            else
                alphabetBlur.gameObject.SetActive(false);
        }
        if(DraggableItem.draggingItem == null)
            alphabetBlur.gameObject.SetActive(false);
    }
    private int GetHolderLocation()
    {
        return transform.parent.GetComponent<PerfectWordHolder>().slotLocation;
    }
    private void CheckAlphabet()
    {
        char[] c = item.GetComponent<Image>().sprite.name.ToCharArray();
        if(!GameManager_SXChuCai.instance.CheckAlphabet(c[0], transform.GetSiblingIndex(), GetHolderLocation()))
        {
            Debug.Log("Wrong Alphabet on " + transform.GetSiblingIndex());
            item.transform.Find("RedFx").gameObject.SetActive(true);
            AudioManager.instance.PlaySfx(6);
        }
        GameManager_SXChuCai.instance.currentAlphabetNumOnSlot++;
        Invoke("CheckEndLv", .2f);
        if(!item.transform.Find("RedFx").gameObject.activeInHierarchy)
            AudioManager.instance.PlaySfx(0);
    }
    private void CheckEndLv() => GameManager_SXChuCai.instance.CheckEndLv();
}
