using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nest : MonoBehaviour
{
    [SerializeField]
    private GameObject m_enemy;   //�G�̃Q�[���I�u�W�F�N�g

    private GameObject enemyResource;

    [SerializeField]
    private int m_nCntSpringUp;   //�G���o������܂ł̃J�E���g

    [SerializeField]
    private int m_nFrame;         //�G���o�����鎞��

    // Start is called before the first frame update
    void Start()
    {
        //������
        m_nCntSpringUp = 0;

        enemyResource = Resources.Load("Enemy\\enemy") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        m_nCntSpringUp++;  //�J�E���g�A�b�v

        if(m_nCntSpringUp % m_nFrame == 0)
        {//�o������܂ł̎��ԂƏo������t���[���̗]�肪0�̂Ƃ�
            
            //�G�𐶐�
            Instantiate(enemyResource, transform.position, Quaternion.identity);

            //�f�o�b�O���O
            Debug.Log("�G���o�Ă���");
            
            //�J�E���g��0�ɂ���
            m_nCntSpringUp = 0;  
        }
    }
}