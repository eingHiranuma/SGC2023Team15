using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;   //プレイヤーのゲームオブジェクト取得
   
    [SerializeField]
    private float m_Speed;         //速さ

    [SerializeField]
    private int m_nLife;           //体力

    private SpriteRenderer m_Color;

    private GameObject moneyResource;
    private GameObject BloodResource;

    private int m_nCntSound;        //音のなるまでのカウント

    private bool m_bFlag;           //ボスのフラグ

    public bool GetbFlag(){ return m_bFlag; }

    public void SetbFlag(bool flag) {  m_bFlag = flag; }

//敵の状態の列挙
private enum STATE
    {
        NONE,                      //なんもない
        DAMEGE,                    //ダメージ
    }

    STATE m_state;                 //状態   

    private int m_nDamegeCounter;  //ダメージ状態になっている時間

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        m_Player = GameObject.Find("Player");
        m_Color = transform.Find("enemy").GetComponent<SpriteRenderer>();

        m_state = STATE.NONE;
        m_nDamegeCounter = 0;

        moneyResource = null;

        m_bFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (null == m_Player)
        {
            return;
        }

        switch (m_state)
        {
            case STATE.NONE:  //通常状態

                if(m_bFlag == false)
                {
                    //色がもとに戻るよ
                    m_Color.material.color = Color.white;
                }
                else
                {
                    //色がもとに戻るよ
                    m_Color.material.color = Color.red;
                }
                
                m_nDamegeCounter = 0;  //カウントをリセット
                break;

            case STATE.DAMEGE:  //ダメージ状態

                //ダメージをくらうと赤くなるよ
                Damege();
                break;
        }

        if (Vector2.Distance(transform.position, m_Player.transform.position) > 0.1f)
        {//プレイヤーとの距離が0.1未満になるまで

            //敵をプレイヤーの方に向かせる
            Quaternion rot = Quaternion.LookRotation(m_Player.transform.position - transform.position, Vector2.up);

            rot.y = 0;
            rot.x = 0;

            transform.rotation = Quaternion.Lerp(transform.rotation, rot, 0.1f);

            //敵のプレイヤーの方に移動させる
            transform.position = Vector2.MoveTowards(transform.position, m_Player.transform.position, m_Speed * Time.deltaTime);

            if(m_nCntSound >= 120)
            {//カウントが120以上になったら

                //サウンドを鳴らす
                SoundManager.Instance.Play("enemy01");

                //カウントをリセットする
                m_nCntSound = 0;

            }

            //カウントアップ
            m_nCntSound++;
        }
    }

    //ゲームオブジェクトが何かに当たった時に呼び出される
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ////敵同士の当たり判定
        //if (collision.collider.tag == ("Enemy"))
        //{
            
        //}
    }

    //ヒット処理
    public void Hit()
    {//攻撃を受けたときに呼び出す

        //体力を減らす
        m_nLife--;

        //状態をダメージにする
        m_state = STATE.DAMEGE;

        if (m_nLife <= 0)
        {//体力が0以下

            if(moneyResource == null)
            {
                moneyResource = Resources.Load("Money\\Money") as GameObject;

                //敵を生成
                Instantiate(moneyResource, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("nullだよ〜ン");
            }

            if(BloodResource ==  null)
            {
                BloodResource = Resources.Load("Blood\\blood_00") as GameObject;

                //血液を生成
                Instantiate(BloodResource, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("nullです");
            }
           

            //自分を破棄
            Destroy(gameObject);

            return;
        }
    }

    //色が変わる処理
    public void Damege()
    {
        m_Color.material.color = Color.red;  //色を赤に変える
        
        m_nDamegeCounter++;  //カウントアップ

        if (m_nDamegeCounter >= 60)
        {//カウントが60以上になったら

            //状態を戻す
            m_state = STATE.NONE;
        }
    }
}
