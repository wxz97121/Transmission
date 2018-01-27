using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropPanel : MonoBehaviour,IDropHandler
{
    public void OnDrop(PointerEventData data)
    {
        var originalPerson = data.pointerDrag.GetComponent<Person>();
        if (originalPerson == null) return;
        originalPerson.ID = originalPerson.ID;
    }
}
