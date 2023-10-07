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

    public bool canMoveArea;

    private void Start()
    {
        currentHP = maxHP;
        //hpText.SetText(maxHP.ToString());
        GameStat.stat = GameStat.Status.OnArea;
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

            //Vector3 mousePos = Input.mousePosition;
            //mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            //mousePos = new Vector3(mousePos.x,mousePos.y,0);
            //dbgObj.transform.position = mousePos;


            //移動の向きなど座標関連はVector3で扱う
            Vector3 velocity = new Vector3(_input_x, _input_y, 0);
            //ベクトルの向きを取得
            direction = velocity.normalized;

            if (Input.GetMouseButtonDown(0))
            {
                //Vector3 diff = mousePos - transform.position;
                gun.Shot(direction);
            }

            //移動距離を計算
            float distance = _speed * Time.deltaTime;
            //移動先を計算
            Vector3 destination = transform.position + direction * distance;

            float rad = Mathf.Atan2(direction.y, direction.x);
            float deg = Mathf.Rad2Deg * rad;

            //移動先に向けて回転
            transform.rotation = Quaternion.Euler(0, 0, deg);
            //移動先の座標を設定
            transform.position = destination;
        }

        if(GameStat.stat == GameStat.Status.movingNextArea)
        {
            MoveArea();
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
        currentHP--;

        if(currentHP == 2)
        {
            //兜を脱ぐ
        }
        else if(currentHP == 1)
        {
            //胴体を脱ぐ
        }
        else
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
        //gameOverText.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            TakeDamage();
        }
    }
}
