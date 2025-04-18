using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePlayerState
{
    Idle,
    Run,
    Jump,
    Jump_End,
    Attack,
    Roll,
    Hit,
    Defend,
    DefendEnter,
    Dead,
    End,
}

public enum eMonsterState
{
    Idle,
    Run,
    Attack,
    Hit,
    End,
}

public struct tAttack
{
    public int iDamage;
    
}

