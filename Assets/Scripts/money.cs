using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class money : MonoBehaviour
{
    private GameObject m_Player;   //�v���C���[�̃Q�[���I�u�W�F�N�g�擾

    private int m_Lottery;  //���I�ԍ��ۑ�
    static private int m_Money;    //����

    [SerializeField]
    private float m_Speed;         //����

    // Start is called before the first frame update
    void Start()
    {
        //1�`100�͈̔͂Œl���ς���
        m_Lottery = Random.Range(1, 101);

        m_Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, m_Player.transform.position) >= 0.1f)
        {//�v���C���[�Ƃ̋�����0.1�����ɂȂ�܂�

            //�G�̃v���C���[�̕��Ɉړ�������
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

            //�f�o�b�O���O
            Debug.Log(m_Money);
            
            Destroy(gameObject);
        }
    }

    public void Add(int money)
    {
        m_Money += money;
    } 
}
