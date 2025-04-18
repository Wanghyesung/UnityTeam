using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public float iLifeTime = 3.0f;
    void Start()
    {
        StartLifeTime(iLifeTime);
    }

 
    public void StartLifeTime(float _fTime)
    {
        StartCoroutine(EndLifeTime(_fTime));
    }

    private IEnumerator EndLifeTime(float _fTime)
    {
        yield return new WaitForSeconds(_fTime);
        Pool.GPool.ReleaseObject(name, gameObject);
    }
}
