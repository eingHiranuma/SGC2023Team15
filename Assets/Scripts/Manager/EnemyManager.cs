
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
{
    /// <summary>
    /// 巣
    /// </summary>
    private List<nest> nestList = new List<nest>();
    public List<nest> NestList => nestList;

    /// <summary>
    /// root
    /// </summary>
    private Transform root;
    public Transform EnemyRoot => root;


    /// <summary>
    /// Start
    /// </summary>
    protected override void Start()
    {
    }


    /// <summary>
    /// Init
    /// </summary>
    public void Init()
    {
        // Root作成
        var rootObject = new GameObject("Enemy");
        root = rootObject.transform;
    }


    /// <summary>
    /// 巣配置
    /// </summary>
	public void SetNest(Vector3 position)
    {
        var go = Instantiate(Resources.Load("Nest/nest") as GameObject);
        go.name = "Nest" + nestList.Count.ToString();
        go.transform.position = position;
        go.transform.SetParent(root, false);
        nestList.Add(go.GetComponent<nest>());
    }


    /// <summary>
    /// 巣破棄
    /// </summary>
	public void Destroy()
    {
        if(root != null)
        {
            GameObject.Destroy(root.gameObject);
        }
      
        nestList.Clear();
    }

    public void ReMoveNest( nest nest)
    {
        nestList.Remove(nest);
    }
}

