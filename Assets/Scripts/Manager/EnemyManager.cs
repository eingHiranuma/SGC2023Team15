
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
    /// Start
    /// </summary>
    protected override void Start()
    {
    }


    /// <summary>
    /// 巣配置
    /// </summary>
	public void SetNest(Vector3 position)
    {
        var go = Instantiate(Resources.Load("Nest/nest") as GameObject);
        go.name = "Nest" + nestList.Count.ToString();
        go.transform.position = position;
        nestList.Add(go.GetComponent<nest>());
    }
}

