using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BossCtrl : MonoBehaviour
{
    public float speed;
    SpriteRenderer renderer;
    private Transform target;

    Image hpBarImage;
    int score = 7;
    bool isDie;

    float hp = 400f;
    float fullhp = 400f;
    float damage;
    [SerializeField] GameObject nextFloor;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        hpBarImage = GetComponentInChildren<Image>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameOver == false)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        damage = EnemyCtrl.damage;
    }

    void ReturnColor()
    {
        renderer.color = Color.white;

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
                    nextFloor.SetActive(true);
                }
                else
                {
                    hpBarImage.fillAmount = hp / fullhp;
                }

            }
        }
    }

}
