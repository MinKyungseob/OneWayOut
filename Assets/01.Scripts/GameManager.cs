using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] stagePrefabs; //스테이지 프리팹
    [SerializeField] GameObject player;
    [SerializeField] Transform DownSpawn;
    [SerializeField] Transform UpSpawn;
    GameObject curStage; //현재 스테이지 오브젝트
    public static int curStageIndex;


    static GameManager m_instance;
    static public GameManager instance; //공용적으로 접근 가능한 GameManager 객체
    public static GameManager Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindObjectOfType<GameManager>();

            return m_instance;
        }

    }

    public bool isGameOver;

    public static int playerLife;
    public int score;

    //UI
    [SerializeField] Texture2D[] numTextures;
    [SerializeField] Image lifeNumImage;
    [SerializeField] Text scoreNumText;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject gameClearUI;
    [SerializeField] Text timeUI;
    float time = 0f;

    private void Awake()//게임 시작 후 Start 이전에 호출이 되는 함수
    {
        time = 0f;
        isGameOver = false;
        gameOverUI.SetActive(false);
        if (GameManager.instance == null)
            GameManager.instance = this;
        playerLife = 3;
        UpdateLifeUI();
        score = 0;
        scoreNumText.text = this.score.ToString();
        //Time.timeScale = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateLifeUI();
        curStage = Instantiate(stagePrefabs[curStageIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerLife > 6)
            playerLife = 6;
        UpdateLifeUI();
        
        //if (Input.anyKey)
        //    Time.timeScale = 1f;

        time += Time.deltaTime;
        timeUI.text = string.Format("{0:N2}", time);

        restart();
    }

    public void NextStage() //NextRoom에서 호출해줄 함수
    {
        curStageIndex++;

        if (curStageIndex < stagePrefabs.Length)
        {
            Destroy(curStage);
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject item in enemies)
                Destroy(item);

            curStage = Instantiate(stagePrefabs[curStageIndex]);
            player.transform.position = DownSpawn.position;

        }

    }
    public void BeforeStage() //BeforeRoom에서 호출해줄 함수
    {
        curStageIndex--;

        if (curStageIndex < stagePrefabs.Length)
        {
            Destroy(curStage);
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject item in enemies)
                Destroy(item);

            curStage = Instantiate(stagePrefabs[curStageIndex]);
            player.transform.position = UpSpawn.position;

        }

    }

    public void PlayerDie() //플레이어가 죽었을때 호출될 함수
    {
        if (playerLife <= 0)
        {
            GameOver();
        }
        else
        {
            //플레이어 부활
            Instantiate(player);

            UpdateLifeUI();

        }

    }

    void GameOver() //몫이 다 닳아서 게임오버가 됐을때 호출될 함수
    {
        isGameOver = true;
        gameOverUI.SetActive(true);

        Time.timeScale = 0f;

    }

    public void GameClear()
    {
        gameClearUI.SetActive(true);
        Time.timeScale = 0f;
    }

    void UpdateLifeUI()
    {
        Sprite sprite = Sprite.Create(numTextures[playerLife], lifeNumImage.sprite.rect, lifeNumImage.sprite.pivot);

        lifeNumImage.sprite = sprite;

    }

    public void AddScore(int score) //적이 죽을때 호출될 함수
    {
        this.score += score;

        //UI에 표시하기
        scoreNumText.text = this.score.ToString();
    }

    void restart()
    {
        if (isGameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {   //모든 내용 리셋
                curStageIndex = 0;
                Destroy(curStage);
                Awake();
                Start();    
                //Destroy는 하나의 object만 삭제하므로, "Enemy"를 가진 모든 object를 삭제하기 위해 변형한, 응용한 문장.★
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject item in enemies)
                    Destroy(item);

                //Time.timeScale=0f이면 멈추고, 0.5f면 0.5배속, 1f면 1배속으로 게임이 진행됨.
                Time.timeScale = 1f;
                //적 리셋
                EnemyCtrl.damage = 12f;
                //플레이어 리셋
                PlayerCtrl.speed = 3f;
                PlayerCtrl.x = 1; PlayerCtrl.y = 1.3f; PlayerCtrl.z = 1;
                PlayerCtrl.isDie = false;
                GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 0, 0);
            }
        }
    }
}
