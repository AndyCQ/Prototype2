using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railgun : MonoBehaviour
{
    public float lifeTime = 1;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

}
