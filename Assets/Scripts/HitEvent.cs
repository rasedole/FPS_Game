using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����1 : �÷��̾�� �������� ������.

public class HitEvent : MonoBehaviour
{
    public EnemyFSM eFSM;

    public void HitPlayer()
    {
        eFSM.AttackAction();
    }
}
