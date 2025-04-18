using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager GBattleManager;

    private void Awake()
    {
        if (GBattleManager != null && GBattleManager != this)
            Destroy(GBattleManager);
        else
            GBattleManager = this;

      
        DontDestroyOnLoad(GBattleManager);
    }

    public void ActiveDamage(Action<Vector2, int> ActionHandler, Vector2 _vHitPoint, int _iDamage)
    {
        ActionHandler?.Invoke(_vHitPoint, _iDamage);
    }
}
