
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
	private enum phase
	{
		title_init,
		title,
		game_init,
		game,
		result,
	}

	private phase nowPhase = phase.title_init;

    private GameObject map = null;

    private GameObject shop = null;

    /// <summary>
    /// Start
    /// </summary>
    protected override void Start()
	{
		// サウンド初期化
		SoundMasterData.Setup();
		SoundManager.Instance.Initialize();
    }

    /// <summary>
    /// Update
    /// </summary>
    protected override void Update()
	{
		switch (nowPhase)
		{
			// タイトル初期化
			case phase.title_init:
				{
                    var go = Instantiate(Resources.Load("UI/Title/Title") as GameObject);
                    go.name = "Title";
					var title = go.GetComponent<Title>();
					title.ExitCallBack = () =>
					{
                        nowPhase = phase.game_init;
                    };
                    go.transform.position = Vector3.zero;
                }
				nowPhase = phase.title;
                break;

			// タイトル
			case phase.title:
				break;

			// ゲーム初期化
			case phase.game_init:
                {
//                    var go = Instantiate(Resources.Load("Canvas") as GameObject);
//                    go.name = "Shop";
/*
//                    var title = go.GetComponent<Title>();
                    title.ExitCallBack = () =>
                    {
                        nowPhase = phase.game_init;
                    };
                    go.transform.position = Vector3.zero;
*/
                }
                {
                    PlayerManager.Instance.Init();
                    PlayerManager.Instance.LoadPlayer(Vector3.zero);

                    ShopManager.Instance.Init();

                    map = Instantiate(Resources.Load("Map/Map") as GameObject);
                    map.name = "Map";
                    map.transform.position = Vector3.zero;

                    EnemyManager.Instance.Init();
                    EnemyManager.Instance.SetNest(new Vector3(10, 0, 0));
                    EnemyManager.Instance.SetNest(new Vector3(10, 10, 0));

                    nowPhase = phase.game;
                }
                break;


            // ゲームメイン
            case phase.game:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    UninitAll();

                    nowPhase = phase.title_init;

                    

                }
                else if (Input.GetKeyDown(KeyCode.V))
                {
                    ShopManager.Instance.Disp();
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    ShopManager.Instance.Hide();
                }
                break;

            // リザルト
            case phase.result:
                break;
        }
    }

    public void ReturnTitle()
    {
        UninitAll();

        nowPhase = phase.title_init;
    }

    private void UninitAll()
    {
        EnemyManager.Instance.Destroy();
        PlayerManager.Instance.Destroy();
        ShopManager.Instance.Destroy();

        GameObject.Destroy(map);
        map = null;
    }
}

