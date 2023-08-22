using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목적1 : HP 슬라이더가 타겟의 앞 방향으로 향하게 한다.
//속성1 : 타켓
public class HPSliderTarget : MonoBehaviour
{
    //속성1 : 타켓
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.forward = target.transform.forward;
    }
}
