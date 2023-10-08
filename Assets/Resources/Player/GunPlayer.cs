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

    private int m_nCntSound;        //���̂Ȃ�܂ł̃J�E���g

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
            //x�������Az�������̓��͂��擾
            //Horizontal�A�����A�������̃C���[�W
            _input_x = Input.GetAxis("Horizontal");
            //Vertical�A�����A�c�����̃C���[�W
            _input_y = Input.GetAxis("Vertical");


            Vector3 velocity = new Vector3(_input_x, _input_y, 0);
            //�x�N�g���̌������擾
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

            //�ړ��������v�Z
            float distance = _speed * Time.deltaTime;
            //�ړ�����v�Z
            Vector3 destination = transform.position + direction * distance;

            float rad = Mathf.Atan2(direction.y, direction.x);
            float deg = Mathf.Rad2Deg * rad;

            //�ړ���Ɍ����ĉ�]
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

            //Debug.Log("isMuteki" + isMuteki);
        }

        if(GameStat.stat == GameStat.Status.movingNextArea)
        {
            MoveArea();
        }

        if(_input_x != 0.0f || _input_y != 0.0f)
        {//�ړ����Ă���

            if(m_nCntSound >= 60)
            {//�J�E���g��60�ȏ�̎�

                //����
                SoundManager.Instance.Play("footstep");

                //�J�E���g�����Z�b�g
                m_nCntSound = 0;
            }

            //�J�E���g�A�b�v
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
