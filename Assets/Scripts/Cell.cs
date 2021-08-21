using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    bool m_alive = false;
    public bool Alive
    {
        get { return m_alive; }
        set 
        { 
            m_alive = value;
            GetComponent<Image>().color = Alive ? Color.black : Color.white; 
        }
    }
    public bool NextAlive { get; set; }

    bool m_downMouse = false;

    private void Update()
    {
        m_downMouse = Input.GetButton("Fire1") ? true : false;
    }
    
    public void NextStep()
    {
        Alive = NextAlive;
    }

    public void MouseAlive()
    {
        if (m_downMouse) Alive = true;
    }
}
