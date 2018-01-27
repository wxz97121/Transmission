using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private void Awake()
    {
        m_Image = GetComponent<Image>();
        m_Controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>();
        ID = id;
    }

    void UpdatePic()
    {
        //print(m_Controller);

        if (m_Controller.Pic.Length > id && id >= 0 && m_Controller.Pic[id] != null)
            m_Image.overrideSprite = m_Controller.Pic[id];
    }
}
