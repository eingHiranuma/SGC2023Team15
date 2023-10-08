using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendingMachine : MonoBehaviour
{
    public Button push_button1;
    public Button push_button2;

    public GameObject Insect;
    public GameObject Armor;

    public GameObject YesButtom;
    public GameObject NoButtom;

    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;

    public GameObject NextButton;
    public GameObject ReturnButton;

    private int _aNum; //�A�[�}�[�̏�����
    private int _iNum; //���悯�X�v���[�̏�����
    private int money1;
    private int money2;


    // �����̔��@���I�����ꂽ�Ƃ��̏���
    void Start()�@//���悯�X�v���[�ƃA�[�}�[�̑I��
    {
        _aNum = 0;
        _iNum = 0;

        Text1.SetActive(true);
        Text2.SetActive(false);
        Text3.SetActive(false);
        YesButtom.SetActive(true);
        NoButtom.SetActive(true);
        Insect.SetActive(true);
        Armor.SetActive(true);
        NextButton.SetActive(false);
        ReturnButton.SetActive(true);  
    }
        
    // Update is called once per frame
    void Update() 
    {

    }

    //���悯�X�v���[���w��
    public void InsectPushed()

    {
        if ((_iNum < 1)&&(money1 >= 300))   //���悯�X�v���[��1�܂ŃX�g�b�N�\����300�~�ȏ�
        {
            Insect.SetActive(false);
            Armor.SetActive(false);
            Text1.SetActive(false);
            Text2.SetActive(true);
            YesButtom.SetActive(false);
            NoButtom.SetActive(false);
            NextButton.SetActive(true);
            ReturnButton.SetActive(false);
            _iNum++;
            money2 = money1 - 300;
        }
        else
        {
            Debug.Log("�w���ł��܂���");
        }
    }

    //�A�[�}�[���w��
    public void ArmorPushed()
    {
        if ((_aNum < 1)&&(money1 >= 300))   //�A�[�}�[��1�܂ŃX�g�b�N�\����300�~�ȏ�
        {
            Insect.SetActive(false);
            Armor.SetActive(false);
            Text1.SetActive(false);
            Text3.SetActive(true);
            YesButtom.SetActive(false);
            NoButtom.SetActive(false);
            NextButton.SetActive(true);
            ReturnButton.SetActive(false);
            _aNum++;
            money2 = money1 - 300;
        }
        else
        {
            Debug.Log("�w���ł��܂���");
        }
    }

    public void NextPushed()
    {
        Text2.SetActive(false);
        Text3.SetActive(false);
        NextButton.SetActive(false);
        ReturnButton.SetActive(false);
    }

    public void ReturnPushed()
    {
        Text1.SetActive(false);
        YesButtom.SetActive(false);
        NoButtom.SetActive(false);
        Insect.SetActive(false);
        Armor.SetActive(false);
        NextButton.SetActive(false);
        ReturnButton.SetActive(false);
    }
}

