using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    [SerializeField]private GameObject info;
    // Start is called before the first frame update
    public void ShowInfo()
    {
        info.SetActive(true);
    }
}
