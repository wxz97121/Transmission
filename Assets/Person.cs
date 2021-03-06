﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Text;


public class Person : MonoBehaviour
{
    [SerializeField]
    private int id;
    public int ID
    {
        get { return id; }
        set { id = value; UpdatePic(); }
    }
    [HideInInspector]
    public Image m_Image;
    private Controller m_Controller;
    private Image PopupImage;
    private Text PopupText;
    private Text m_title;
    static private ShrinkText m_CalcText;
    private Vector3 oriPos;
    [HideInInspector]
    public bool Done = true;
    private void Awake()
    {
        m_title = transform.parent.Find("Title").GetComponent<Text>();
        PopupImage = transform.parent.Find("PopUp").GetComponent<Image>();
        PopupText = PopupImage.GetComponentInChildren<Text>();
        m_Image = GetComponent<Image>();
        m_Controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>();
        if (m_CalcText==null)
            m_CalcText = GameObject.Find("CalcTextBox").GetComponent<ShrinkText>();
        ID = id;
    }

    private void Start()
    {
        oriPos = transform.position;
    }

    void UpdatePic()
    {
        //print(m_Controller);

        if (m_Controller.Pic.Length > id && id >= 0 && m_Controller.Pic[id] != null)
        {
            m_Image.overrideSprite = m_Controller.Pic[id];
            m_title.text = m_Controller.Title[id];
        }
    }
    public IEnumerator Show(string s)
    {
        Done = false;
        Random.InitState(0124);
        PopupText.text = "";

        m_CalcText.text = s;
        yield return new WaitForFixedUpdate();
        PopupText.fontSize = m_CalcText.Bestsize;
   
        //print(PopupText.fontSize);
        StringBuilder sb = new StringBuilder();
        PopupText.color = FullColor(PopupText.color);
        PopupImage.DOFade(1, 0.3f);
        yield return new WaitForSeconds(0.32f);

        StartCoroutine("JumpProcess");

        int now = 0;
        while (now < s.Length)
        {
            float p = Random.value;
            if (p > 1.2 && now + 1 < s.Length)
            {

                sb.Append(s[now++]);
                sb.Append(s[now++]);
            }
            else
            {
                sb.Append(s[now++]);
            }
            PopupText.text = sb.ToString();
            yield return new WaitForSeconds(0.05f);
        }
        StopCoroutine("JumpProcess");
        yield return new WaitForSeconds(1.8f);
        PopupImage.DOFade(0, 0.3f);
        PopupText.DOFade(0, 0.3f);
        Done = true;
    }
    Color ClearColor(Color m_Color)
    {
        Color tmp = m_Color;
        tmp.a = 0;
        return tmp;
    }
    Color FullColor(Color m_Color)
    {
        Color tmp = m_Color;
        tmp.a = 1;
        return tmp;
    }
    public void Rewind()
    {
        DOTween.Clear();
        StopAllCoroutines();
        Done = true;
        PopupText.text = "";
        PopupImage.color = ClearColor(PopupImage.color);
        transform.position = oriPos;
    }

    IEnumerator JumpProcess()
    {
        //Debug.Log(GetComponent<RectTransform>().rect.position);
        float jumptime = 0.15f;
        while (!Done)
        {
            transform.DOJump(transform.position, 3.0f, 1, jumptime,true);
            yield return new WaitForSeconds(jumptime+0.02f);
        }
        yield return 0;
    }
    //float Calcsize(string s)
    //{
    //    m_CalcText.text = s;
        
    //    print(textGenerator.fontSizeUsedForBestFit);
    //    return textGenerator.fontSizeUsedForBestFit;
    //}
}
