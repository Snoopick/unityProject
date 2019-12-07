using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopPlatform : MonoBehaviour
{
    [SerializeField] private GameObject m_baseView;
    [SerializeField] private GameObject m_greenView;
    public int type;
    
    // Start is called before the first frame update
    void Start ()
    {
        SetBase();
    }

    public void SetGreen()
    {
        m_baseView.SetActive(false);
        m_greenView.SetActive(true);

        Invoke("SetBase", .3f);
    }

    public void SetBase()
    {
        m_baseView.SetActive(true);
        m_greenView.SetActive(false);
    }
}
