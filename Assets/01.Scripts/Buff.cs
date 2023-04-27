using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    int buffnum;
    int countNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        randomBuff();
    }
    void roomEnter()
    {
        if (GameManager.curStageIndex == 1)
            countNum++;
    }

    void randomBuff()
    {
        buffnum = Random.Range(0, 4);
        if(buffnum==0)//기본체력 1 증가
        {
            if (GameManager.playerLife < 6)
                GameManager.playerLife += 1;
            else
            {
                GameManager.playerLife = 6;
                randomBuff();
            }
        }
        if (buffnum==1)//공격력 스텟 +3 추가
        {
            EnemyCtrl.damage += 3f;
        }
        if (buffnum == 2)// 공속,이동속도 증가
        {
            //PlayerCtrl playerCtrl = GameObject.Find("x").GetComponent<PlayerCtrl>();
            //playerCtrl.x *= 1.3f;
            if (PlayerCtrl.speed < 4.2f)
                PlayerCtrl.speed += 0.4f;
            else
            {
                PlayerCtrl.speed = 4.2f;
                randomBuff();
            }
        }
        if (buffnum == 3)//공격범위 증가
        {
            PlayerCtrl.x *= 1.07f;
            PlayerCtrl.y *= 1.07f;
            //PlayerCtrl.instance.windPrefab.transform.localScale+= new Vector3(0.2f, 0.2f, 0);
            PlayerCtrl.instance.windPrefab.transform.localScale *= 1.1f;
        }

    }

 
}
