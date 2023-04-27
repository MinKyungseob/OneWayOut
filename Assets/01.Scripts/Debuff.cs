using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff : MonoBehaviour
{
    int debuffnum;
    // Start is called before the first frame update
    void Start()
    {
        randomDebuff();
    }

    void randomDebuff()
    {
        debuffnum = Random.Range(0, 3);

        if (debuffnum == 0)//공격력 스텟 -3 
        {
            EnemyCtrl.damage -= 3f;
        }
        if (debuffnum == 1)// 공속,이동속도 감소
        {
            //PlayerCtrl playerCtrl = GameObject.Find("x").GetComponent<PlayerCtrl>();
            //playerCtrl.x *= 1.3f;
            PlayerCtrl.speed -= 0.4f;
        }
        if (debuffnum == 2)//공격범위 증가
        {
            PlayerCtrl.x /= 1.07f;
            PlayerCtrl.y /= 1.07f;
            PlayerCtrl.instance.windPrefab.transform.localScale /= 1.1f;
        }

    }


}
