using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class EnemyCtrl : MonoBehaviour
{
    public float speed;
    SpriteRenderer renderer;
    private Transform target;

    Image hpBarImage;
    int score = 1;
    bool isDie;

    float hp = 40f;
    float fullhp = 40f;
    float plusHp;
    public static float damage = 12f;

    void Start()
    {
        PlusHp();
        hp = hp + plusHp;
        fullhp = fullhp + plusHp;
        renderer = GetComponent<SpriteRenderer>();
        hpBarImage = GetComponentInChildren<Image>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isGameOver==false)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void ReturnColor()
    {
        renderer.color = Color.white;

    }

    void PlusHp()
    {
        plusHp = 2 * GameManager.curStageIndex;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            if (isDie == false)
            {
                hp -= damage;
                renderer.color = Color.red;

                Invoke("ReturnColor", 0.1f);

                if (hp <= 0)
                {
                    isDie = true;

                    //스코어 처리
                    GameManager.instance.AddScore(score);

                    Destroy(gameObject);
                }
                else
                {
                    hpBarImage.fillAmount = hp / fullhp;
                }

            }
        }
    }

}
