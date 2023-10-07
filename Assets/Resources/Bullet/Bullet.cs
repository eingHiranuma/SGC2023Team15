using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject m_enemy;   //�Q�[���I�u�W�F�N�g�擾

    public float speed;
    public Vector3 direction;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("EnemyHit");

            //�G�̃C���X�^���X���擾
            enemy enemy = collision.gameObject.GetComponent<enemy>();

            enemy.Hit();

            Destroy(gameObject);
        }
    }
   

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        Vector3 destination = direction * speed * Time.deltaTime;
        //Debug.Log("dest" + destination);
        transform.Translate(destination,Space.World);
    }
}
