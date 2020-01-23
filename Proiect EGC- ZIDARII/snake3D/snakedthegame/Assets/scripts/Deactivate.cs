using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    void Start()
    {
        Invoke("dezactivare", Random.Range(3f, 6f));//dezactivez fructele de pe mapa
    }

    void dezactivare()
    {
        gameObject.SetActive(false);
    }
}
