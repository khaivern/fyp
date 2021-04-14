using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWing : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);    
    }

    public void DisplayWing()
    {
        this.gameObject.SetActive(true);
    }

    
}
