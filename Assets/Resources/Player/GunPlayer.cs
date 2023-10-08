using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunPlayer : MonoBehaviour
{
    //移動速度
    [SerializeField]
    private float _speed = 3.0f;

    //x軸方向の入力を保存
    private float _input_x;
    //z軸方向の入力を保存
    private float _input_y;

    [SerializeField]
    Gun gun;

    [SerializeField]
    GameObject dbgObj;

    Vector3 direction;

    Vector3 prevPos;
    Vector3 newPos;
    float movingAreaSpent = 0;

    [SerializeField]
    private int maxHP;
    private int currentHP;

    //[SerializeField]
    //TMP_Text  gameOverText;
    //[SerializeField]
    //TMP_Text hpText;

    public bool isMuteki;
    [SerializeField]
    float maxMutekiTime;
    private float damageIntervalSpent = 0;

    public bool canMoveArea;
    SpriteRenderer sRenderer;

    private int m_nCntSound;        //音のなるまでのカウント

    private GameObject ResultResource;

    private void Start()
    {
        currentHP = maxHP;
        //hpText.SetText(maxHP.ToString());
        GameStat.stat = GameStat.Status.OnArea;
        sRenderer = GetComponent<SpriteRenderer>();
        ResultResource = Resources.Load("Result\\Result") as GameObject;
    }

    void Update()
    {
        if(GameStat.stat == GameStat.Status.OnArea)
        {
            //x軸方向、z軸方向の入力を取得
            //Horizontal、水平、横方向のイメージ
            _input_x = Input.GetAxis("Horizontal");
            //Vertical、垂直、縦方向のイメージ
            _input_y = Input.GetAxis("Vertical");


            Vector3 velocity = new Vector3(_input_x, _input_y, 0);
            //ベクトルの向きを取得
            direction = velocity.normalized;

            Vector3 mousePos = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                mousePos.z = 1;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                Debug.Log("mPos:"+mousePos);
                Vector3 dir = new Vector3 (mousePos.x,mousePos.y,0) - new Vector3(transform.position.x, transform.position.y,0);
                gun.Shot(dir);

                //gun.Shot(direction);

                


                SoundManager.Instance.Play("shot");
            }

            //移動距離を計算
            float distance = _speed * Time.deltaTime;
            //移動先を計算
            Vector3 destination = transform.position + direction * distance;

            float rad = Mathf.Atan2(direction.y, direction.x);
            float deg = Mathf.Rad2Deg * rad;

            //移動先に向けて回転
            //transform.rotation = Quaternion.Euler(0, 0, deg);
            Vector3 localScale = transform.localScale;
            if(_input_x > 0)
            {
                localScale.x = -1;
            }
            else
            {
                localScale.x = 1;
            }
            transform.localScale = localScale;

            //移動先の座標を設定
            transform.position = destination;

            //無敵時間中なら経過時間をカウント
            if (isMuteki)
            {
                damageIntervalSpent += Time.deltaTime;
                if(damageIntervalSpent > maxMutekiTime)
                {
                    isMuteki = false;
                    sRenderer.color = Color.red;//元の色に戻す
                }
            }

            //Debug.Log("isMuteki" + isMuteki);
        }

        if(GameStat.stat == GameStat.Status.movingNextArea)
        {
            MoveArea();
        }

        if(_input_x != 0.0f || _input_y != 0.0f)
        {//移動している

            if(m_nCntSound >= 60)
            {//カウントが60以上の時

                //足音
                SoundManager.Instance.Play("footstep");

                //カウントをリセット
                m_nCntSound = 0;
            }

            //カウントアップ
            m_nCntSound++;
        }
    }

    public void SetMoveDiff(Vector3 moveDiff)
    {
        if (canMoveArea)
        {
            GameStat.stat = GameStat.Status.movingNextArea;
            prevPos = transform.position;
            newPos = prevPos + moveDiff;
            movingAreaSpent = 0;
        }
        else {
            GameStat.stat = GameStat.Status.movingNextArea;
            prevPos = transform.position;
            moveDiff = -moveDiff / 8.0f;
            newPos = prevPos + moveDiff;
            movingAreaSpent = 0;
        }
        

    }

    public void MoveArea()
    {
        movingAreaSpent += Time.deltaTime;
        transform.position = Vector3.Lerp(prevPos,newPos,movingAreaSpent);
        if(movingAreaSpent > 1f)
        {
            GameStat.stat = GameStat.Status.OnArea;
            movingAreaSpent = 0;
        }
    }

    public void TakeDamage()
    {
        isMuteki = true; damageIntervalSpent = 0;
        sRenderer.color = Color.blue; //無敵時間中は色を変更

        currentHP--;

        Debug.Log("Player Damaged");
        if(currentHP == 2)
        {
            //兜を脱ぐ
        }
        else if(currentHP == 1)
        {
            //胴体を脱ぐ
        }
        else if(currentHP == 0)
        {
            //ゲームオーバー
            Die();
            //hpText.enabled = false;
        }
        //hpText.SetText(currentHP.ToString());
    }
    public void Die()
    {
        GameStat.stat = GameStat.Status.Die;
        Destroy(gameObject);
        Instantiate(ResultResource, transform.position, Quaternion.identity);
        //gameOverText.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && !isMuteki)
        {
            TakeDamage();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !isMuteki)
        {
            TakeDamage();
        }
    }
}
