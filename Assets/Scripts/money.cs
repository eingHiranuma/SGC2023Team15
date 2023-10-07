using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class money : MonoBehaviour
{
    private GameObject m_Player;   //プレイヤーのゲームオブジェクト取得

    private int m_Lottery;  //抽選番号保存
    static private int m_Money;    //お金

    [SerializeField]
    private float m_Speed;         //速さ

    // Start is called before the first frame update
    void Start()
    {
        //1～100の範囲で値が変える
        m_Lottery = Random.Range(1, 101);

        m_Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, m_Player.transform.position) >= 0.1f)
        {//プレイヤーとの距離が0.1未満になるまで

            //敵のプレイヤーの方に移動させる
            transform.position = Vector2.MoveTowards(transform.position, m_Player.transform.position, m_Speed);
        }
        else
        {
            if (m_Lottery >= 1 && m_Lottery <= 10)
            {
                Add(0);
            }
            else if (m_Lottery >= 11 && m_Lottery <= 90)
            {
                Add(1);
            }
            else if (m_Lottery >= 91 && m_Lottery <= 100)
            {
                Add(2);
            }

            //デバッグログ
            Debug.Log(m_Money);
            
            Destroy(gameObject);
        }
    }

    public void Add(int money)
    {
        m_Money += money;
    } 
}
