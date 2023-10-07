﻿
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
                    PlayerManager.Instance.LoadPlayer(Vector3.zero);

                    var go = Instantiate(Resources.Load("Map/Map") as GameObject);
                    go.name = "Map";
                    go.transform.position = Vector3.zero;

                    EnemyManager.Instance.SetNest(new Vector3(10, 0, 0));
                    EnemyManager.Instance.SetNest(new Vector3(10, 10, 0));

                    nowPhase = phase.game;
                }
                break;

			// ゲームメイン
			case phase.game:
				break;

            // リザルト
            case phase.result:
                break;
        }
    }
}

