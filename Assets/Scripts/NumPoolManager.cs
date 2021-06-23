using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumPoolManager : MonoBehaviour
{
    public void Numspawn(Transform p)
    {
        GameObject number = null;
        int rand = Random.Range(0, 10);
        if(gameObject.transform.childCount==0)
        {
            return;
        }
        number = gameObject.transform.GetChild(rand).gameObject;
        number.SetActive(true);
        number.transform.SetParent(null);
        number.transform.position = p.position;
        
    }
}
