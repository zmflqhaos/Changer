using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootNum : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletT = null;
    private Transform numB = null;

    public void OnClick()
    {        
        StartCoroutine(NumShoot());
    }

    private void Update()
    {
       if(Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(NumShoot());
        }
    }

    private IEnumerator NumShoot()
    {
        numB = bulletT.transform.GetChild(0);
        numB.SetParent(null);
        yield return new WaitForSeconds(2f);
    }
}
