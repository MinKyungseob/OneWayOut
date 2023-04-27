using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public static float speed = 3f;
    public GameObject windPrefab;

    SpriteRenderer renderer;
    int damage = 1;
    public static float x = 1, y = 1.3f, z = 1;
    Animator animator;
    Rigidbody2D rigidbody;
    public static GameObject wind;
    bool isMove = true;

    public static bool isDie = false;
    bool isCanDamaged;

    Vector2 moveVec;
    Vector2 scaleVec;
    //Transform position;
    static public PlayerCtrl instance;



    private void Awake()
    {
        if (PlayerCtrl.instance == null)
            PlayerCtrl.instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        windPrefab.transform.localScale = new Vector3(1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //moveVec.x = Input.GetAxisRaw("Horizontal");
        //moveVec.y = Input.GetAxisRaw("Vertical");
        transform.Translate(moveVec * speed * Time.deltaTime, Space.World);

        //이동시 애니메이션
        if (isMove == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool("IsUpWalk", true);
                transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World);
            }
            else
            {
                animator.SetBool("IsUpWalk", false);
            }
            if (Input.GetKey(KeyCode.S))
            {
                animator.SetBool("IsDownWalk", true);
                transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
            }
            else
            {
                animator.SetBool("IsDownWalk", false);
            }
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetBool("IsLeftWalk", true);
                transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
            }
            else
            {
                animator.SetBool("IsLeftWalk", false);
            }
            if (Input.GetKey(KeyCode.D))
            {
                animator.SetBool("IsRightWalk", true);
                transform.Translate(Vector2.right * speed * Time.deltaTime, Space.World);
            }
            else
            {
                animator.SetBool("IsRightWalk", false);
            }


            //공격시 애니메이션
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                isMove = false;
                attack(0, y);
                animator.SetTrigger("IsAttack");
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                isMove = false;
                attack(0, -y);
                animator.SetTrigger("IsAttack");
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                isMove = false;
                attack(-x, 0);
                animator.SetTrigger("IsAttack");
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                isMove = false;
                attack(x, 0);
                animator.SetTrigger("IsAttack");
            }
        }

    }

    //IEnumerator ReBirth()
    //{
    //    int count = 0;
    //    int maxCount = 3;

    //    while (count < maxCount)
    //    {
    //        count++;

    //        renderer.color = Color.clear;

    //        yield return new WaitForSeconds(0.1f);

    //        renderer.color = Color.white;

    //        yield return new WaitForSeconds(0.1f);

    //    }

    //    isCanDamaged = true;

    //}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (isDie == false)
            {
                GameManager.playerLife -= damage;
                int count = 0;
                int maxCount = 3;

                while (count < maxCount)
                {
                    count++;

                    renderer.color = Color.red;
                    Invoke("ReturnColor", 0.1f);
                }

                if (GameManager.playerLife <= 0)
                {
                    isDie = true;
                    GameManager.instance.PlayerDie();
                }
            }
        }
    }

    void ReturnColor()
    {
        renderer.color = Color.white;

    }

    void attack(float x, float y)
    {
        wind = Instantiate(windPrefab);
        wind.transform.position = transform.position;
        if (x > 0)
        {
            wind.transform.localRotation = Quaternion.Euler(0, 0, 0);
            wind.transform.position = transform.position + new Vector3(x, 0, 0);
        }
        if (x < 0)
        {
            wind.transform.localRotation = Quaternion.Euler(0, 0, 180);
            wind.transform.position = transform.position + new Vector3(x, 0, 0);
        }
        if (y > 0)
        {
            wind.transform.localRotation = Quaternion.Euler(0, 0, 90);
            wind.transform.position = transform.position + new Vector3(0, y, 0);
        }
        if (y < 0)
        {
            wind.transform.localRotation = Quaternion.Euler(0, 0, 270);
            wind.transform.position = transform.position + new Vector3(0, y, 0);
        }
        Destroy(wind, 0.4f); //destory( object, time) 하면 time 만큼 뒤에 object를 삭제함.
        Invoke("cooltime", 0.4f);
    }
    void cooltime()
    {
        isMove = true;
    }
}