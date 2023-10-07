using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunPlayer : MonoBehaviour
{
    //�ړ����x
    [SerializeField]
    private float _speed = 3.0f;

    //x�������̓��͂�ۑ�
    private float _input_x;
    //z�������̓��͂�ۑ�
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


    private void Start()
    {
        currentHP = maxHP;
        //hpText.SetText(maxHP.ToString());
        GameStat.stat = GameStat.Status.OnArea;
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(GameStat.stat == GameStat.Status.OnArea)
        {
            //x�������Az�������̓��͂��擾
            //Horizontal�A�����A�������̃C���[�W
            _input_x = Input.GetAxis("Horizontal");
            //Vertical�A�����A�c�����̃C���[�W
            _input_y = Input.GetAxis("Vertical");


            //�ړ��̌����ȂǍ��W�֘A��Vector3�ň���
            Vector3 velocity = new Vector3(_input_x, _input_y, 0);
            //�x�N�g���̌������擾
            direction = velocity.normalized;

            if (Input.GetMouseButtonDown(0))
            {
                gun.Shot(direction);
            }

            //�ړ��������v�Z
            float distance = _speed * Time.deltaTime;
            //�ړ�����v�Z
            Vector3 destination = transform.position + direction * distance;

            float rad = Mathf.Atan2(direction.y, direction.x);
            float deg = Mathf.Rad2Deg * rad;

            //�ړ���Ɍ����ĉ�]
            transform.rotation = Quaternion.Euler(0, 0, deg);
            //�ړ���̍��W��ݒ�
            transform.position = destination;

            //���G���Ԓ��Ȃ�o�ߎ��Ԃ��J�E���g
            if (isMuteki)
            {
                damageIntervalSpent += Time.deltaTime;
                if(damageIntervalSpent > maxMutekiTime)
                {
                    isMuteki = false;
                    sRenderer.color = Color.red;//���̐F�ɖ߂�
                }
            }

            Debug.Log("isMuteki" + isMuteki);
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
        isMuteki = true; damageIntervalSpent = 0;
        sRenderer.color = Color.blue; //���G���Ԓ��͐F��ύX

        currentHP--;

        Debug.Log("Player Damaged");
        if(currentHP == 2)
        {
            //����E��
        }
        else if(currentHP == 1)
        {
            //���̂�E��
        }
        else if(currentHP == 0)
        {
            //�Q�[���I�[�o�[
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
