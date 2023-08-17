using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목표1 : 폭탄이 물체에 부딫히면 파괴된다.
//속성1 : 폭발 이펙트
//순서1-1. 폭탄이 물체와 닿으면 폭발 이펙트를 실행한다.
//순서1-2. 폭탄을 파괴한다.

public class BombAction : MonoBehaviour
{
    //속성1 : 폭발 이펙트
    public GameObject bombEffect;

    private void OnCollisionEnter(Collision collision)
    {
        //순서1-1. 폭탄이 물체와 닿으면 폭발 이펙트를 실행한다.
        GameObject bombGO = Instantiate(bombEffect);
        bombGO.transform.position = transform.position;

        //순서1-2. 폭탄을 파괴한다.
        Destroy(gameObject);
    }
}
