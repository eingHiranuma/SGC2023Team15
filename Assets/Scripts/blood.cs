using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blood : MonoBehaviour
{
    private Vector3 m_pos;  //位置

    private SpriteRenderer m_Color;  //色

    private int m_nCntAlpha;         //透明になるまでのカウント

    [SerializeField]
    private int m_MaxCount;          //血液が透明になる

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        m_Color = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        m_nCntAlpha++;

        if(m_nCntAlpha >= m_MaxCount)
        {
            time += Time.deltaTime;
            if (time < 0.5f)
            {
                float alpha = 1.0f - time / 0.5f;
                Color color = m_Color.color;
                color.a = alpha;
                m_Color.color = color;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
