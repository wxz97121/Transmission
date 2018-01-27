using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class DropMe : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image containerImage;
    private Color normalColor;
    public Color highlightColor = Color.yellow;
    [HideInInspector]
    public Person m_Person;
    private GameObject m_DraggingIcons;
    private RectTransform m_DraggingPlanes;
    private void Awake()
    {
        m_Person = GetComponent<Person>();
    }
    public void OnBeginDrag(PointerEventData data)
    {
        var canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
            return;
        
        m_DraggingIcons = new GameObject("icon");

        m_DraggingIcons.transform.SetParent(canvas.transform, false);
        m_DraggingIcons.transform.SetAsLastSibling();

        var image = m_DraggingIcons.AddComponent<Image>();
        var group = m_DraggingIcons.AddComponent<CanvasGroup>();
        group.blocksRaycasts = false;
        image.sprite = GetComponent<Image>().overrideSprite;
        m_Person.m_Image.overrideSprite = null;
        image.SetNativeSize();

        //if (dragOnSurfaces)
        //    m_DraggingPlanes[eventData.pointerId] = transform as RectTransform;
        //else
        m_DraggingPlanes = canvas.transform as RectTransform;
        SetDraggedPosition(data);
    }
    public void OnEndDrag(PointerEventData data)
    {
        Destroy(m_DraggingIcons);
        StartCoroutine(CheckIfNull(data.pointerDrag.GetComponent<Person>()));
    }
    public void OnDrag(PointerEventData data)
    {
        if (m_DraggingIcons)
            SetDraggedPosition(data);
    }
    IEnumerator CheckIfNull(Person PersonToCheck)
    {
        yield return new WaitForEndOfFrame();
        if (PersonToCheck.m_Image.overrideSprite == null)
            PersonToCheck.ID = PersonToCheck.ID;
    }
    private void SetDraggedPosition(PointerEventData eventData)
    {
        //if (dragOnSurfaces && eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null)
        //    m_DraggingPlanes[eventData.pointerId] = eventData.pointerEnter.transform as RectTransform;

        var rt = m_DraggingIcons.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlanes, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = m_DraggingPlanes.rotation;
        }
    }

    public void OnEnable()
    {
        if (containerImage != null)
            normalColor = containerImage.color;
    }

    public void OnDrop(PointerEventData data)
    {
        containerImage.color = normalColor;
        var originalPerson = data.pointerDrag.GetComponent<Person>();
        //这个地方是不是用m_Person好一点
        if (originalPerson == null) return;
        int Temp = m_Person.ID;
        m_Person.ID = originalPerson.ID;
        //TODO:这张图片是飞过去的
        originalPerson.ID = Temp;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (containerImage == null) return;
        containerImage.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (containerImage == null)
            return;

        containerImage.color = normalColor;
    }

}
