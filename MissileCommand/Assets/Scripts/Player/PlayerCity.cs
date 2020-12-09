using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCity : MonoBehaviour
{
    public void ResetCity()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
    }
}
