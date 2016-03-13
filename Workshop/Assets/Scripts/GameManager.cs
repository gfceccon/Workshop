using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace CompletedWorkshop
{

	public class GameManager : MonoBehaviour
	{
        public static GameManager instance = null;

        public int level = 1;
        public int playerFoodPoints = 100;
        public float levelStartDelay = 2f;
        public float turnDelay = 0.1f;
        [HideInInspector]
        public bool playersTurn = false;

        private Text levelText;
        private GameObject levelImage;
        private BoardManager boardScript;
        private CameraAlign cameraScript;
        private List<Enemy> enemies;
        private bool enemiesMoving = false;
        private bool doingSetup = true;



		void Awake()
        {
            if (GameManager.instance == null)
                GameManager.instance = this;
            else if (GameManager.instance != this)
                DestroyObject(gameObject);

            DontDestroyOnLoad(gameObject);

            boardScript = GetComponent<BoardManager>();
            cameraScript = GetComponent<CameraAlign>();

            enemies = new List<Enemy>();

			InitGame();
		}

        void OnLevelWasLoaded(int index)
        {
            level++;
            InitGame();
        }

        void Update()
        {
            if (playersTurn || enemiesMoving || doingSetup)
                return;

            StartCoroutine(MoveEnemies());
        }

		void InitGame()
		{
            doingSetup = true;
            levelImage = GameObject.Find("LevelImage");
            levelText = GameObject.Find("LevelText").GetComponent<Text>();

            levelText.text = "Day " + level;

            levelImage.SetActive(true);

            Invoke("HideLevelImage", levelStartDelay);

            enemies.Clear();

			boardScript.SetupScene(level);
            cameraScript.SetupCamera(boardScript.rows, boardScript.columns);
		}

        void HideLevelImage()
        {
            levelImage.SetActive(false);

            doingSetup = false;
        }

        public void AddEnemyToList(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        public void GameOver()
        {
            levelText.text = "After " + level + " days, you starved.";
            levelImage.SetActive(true);
            enabled = false;
        }

        IEnumerator MoveEnemies()
        {
            enemiesMoving = true;

            yield return new WaitForSeconds(turnDelay);

            if(enemies.Count == 0)
                yield return new WaitForSeconds(turnDelay);

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].MoveEnemy();
                yield return new WaitForSeconds(turnDelay);
            }

            playersTurn = true;
            enemiesMoving = false;
        }
	}
}

