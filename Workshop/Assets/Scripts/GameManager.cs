using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace CompletedWorkshop
{

	public class GameManager : MonoBehaviour
	{
        public static GameManager instance = null;

		private BoardManager boardScript;
        private CameraAlign cameraScript;

        public int level;


		void Awake()
		{
            if (GameManager.instance == null)
                GameManager.instance = this;
            else if (GameManager.instance != this)
                DestroyObject(gameObject);

            DontDestroyOnLoad(gameObject);

            boardScript = GetComponent<BoardManager>();
            cameraScript = GetComponent<CameraAlign>();
			InitGame();
		}

		void InitGame()
		{
			boardScript.SetupScene(level);
            cameraScript.SetupCamera(boardScript.rows, boardScript.columns);
		}

		void Update()
		{
		}
	}
}

