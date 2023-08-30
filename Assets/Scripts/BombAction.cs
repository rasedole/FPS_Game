using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목적1 : 폭탄이 물체에 부딫히면 파괴된다.
//속성1 : 폭발 이펙트
//순서1-1. 폭탄이 물체와 닿으면 폭발 이펙트를 실행한다.
//순서1-2. 폭탄을 파괴한다.

//목적2 : 폭발 효과 반경 내에서 레이어가 'Enemy'인 모든 게임오브젝트의 Collider를 저장하여, 해당 적 게임오브젝트에게 수류탄 데미지를 준다.
//속성2 : 폭발 효과 반경
//순서2-1. 폭탄이 폭발한다.
//순서2-2. 폭발 효과 반경 내에 있는 레이어가 'Enemy'인 게임오브젝트의 Collider를 저장한다.
//순서2-3. 해당 오브젝트에게 데미지를 준다.

public class BombAction : MonoBehaviour
{
    //속성1 : 폭발 이펙트
    public GameObject bombEffect;

    //속성2 : 폭발 효과 반경
    public float explosionRadius = 5;

    //순서2-1. 폭탄이 폭발한다.
    private void OnCollisionEnter(Collision collision)
    {
        //순서2-2. 폭발 효과 반경 내에 있는 레이어가 'Enemy'인 게임오브젝트의 Collider를 저장한다.
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, 1 << 8);

        //순서1-1. 폭탄이 물체와 닿으면 폭발 이펙트를 실행한다.
        GameObject bombGO = Instantiate(bombEffect);
        bombGO.transform.position = transform.position;

        //순서2-3. 해당 오브젝트에게 데미지를 준다.
        foreach(Collider collider in colliders)
        {
            collider.transform.gameObject.GetComponent<EnemyFSM>().GetDamaged(25);
        }


        //순서1-2. 폭탄을 파괴한다.
        Destroy(gameObject);
    }
}
