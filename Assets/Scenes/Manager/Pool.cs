using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    public static Pool GPool; 

    private Dictionary<string, ObjectPool<GameObject>> GObjectPool;
    [SerializeField] private int m_iInstCount;
    //[SerializeField] private List<GameObject> listPrefab;
    

    private void Awake()
    {
        if (GPool != null && GPool != this)
        {
            Destroy(gameObject);
            return;
        }

        GPool = this;
        DontDestroyOnLoad(gameObject);

        GObjectPool = new Dictionary<string, ObjectPool<GameObject>>();
    }

    void Start()
    {
        //�⺻ 10 �� max 50��
        m_iInstCount = 10;

        GameObject[] arrPreFab = Resources.LoadAll<GameObject>("Prefabs");
        for(int i = 0; i<arrPreFab.Length; ++i)
        {
            AddObject(arrPreFab[i]);
        }
    }


    private void AddObject(GameObject _pGameObject)
    {
        //Clone ������
        string strKey = _pGameObject.name.Replace("(Clone)", "").Trim();
        
        if (ContainKey(strKey))
            return;

        //�����ϸ� Destroy��� X (���δ�)
        ObjectPool<GameObject> pPool = new ObjectPool<GameObject>
        (() =>
            Instantiate(_pGameObject),  
            obj => obj.SetActive(true),       
            obj => obj.SetActive(false),      
            obj => Destroy(obj),              
            true,                             //�ߺ�����
            m_iInstCount,                     
            m_iInstCount * 5                  
        );

        GObjectPool[strKey] = pPool;

        for (int i = 0; i < m_iInstCount; ++i)
        {
            GameObject obj = pPool.Get();
            pPool.Release(obj); // ��Ȱ��ȭ
        }
    }

    private bool ContainKey(string _strName)
    {
        return GObjectPool.ContainsKey(_strName);
    }

    public GameObject GetObject(string _strName)
    {
        if (!ContainKey(_strName))
            return null;

        return GObjectPool[_strName].Get();
    }

    public GameObject GetObject(string _strName, Vector3 _vPosition)
    {
        if (!ContainKey(_strName))
            return null;

        GameObject pTargetObj = GObjectPool[_strName].Get();
        pTargetObj.transform.position = _vPosition;

        return pTargetObj;
    }


    public void ReleaseObject(string _strName, GameObject _pObject)
    {
        string strName = _strName.Replace("(Clone)", "").Trim();
        if (GObjectPool.TryGetValue(strName, out ObjectPool<GameObject> pPool))
        {
            pPool.Release(_pObject);

#if UNITY_EDITOR
            _pObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.NotEditable;
            EditorApplication.RepaintHierarchyWindow();
#endif
        }
    }


}
