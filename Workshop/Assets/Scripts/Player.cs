using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CompletedWorkshop
{

    public class Player : MonoBehaviour
    {
        public float restartLevelDelay = 1f;
        public int pointsPerFood = 10;
        public int pointsPerSoda = 20;
        public LayerMask blockingLayer;
		public int wallDamage = 1;

		public AudioClip[] moveSounds;
		public AudioClip[] eatSounds;
		public AudioClip[] drinkSounds;
        public AudioClip gameOverSound;

        public Text foodText;

        private int food;

        
		private Animator animator;
		private BoxCollider2D boxCollider;
        private Rigidbody2D rigid;

        void Start()
        {
            food = GameManager.instance.playerFoodPoints;

            foodText.text = "Food: " + food;

            rigid = GetComponent<Rigidbody2D>();
			boxCollider = GetComponent<BoxCollider2D>();
			animator = GetComponent<Animator>();
        }

        void OnDisable()
        {
            GameManager.instance.playerFoodPoints = food;
        }

        void Update()
        {
            if (!GameManager.instance.playersTurn)
                return;

            int horizontal = 0;
            int vertical = 0;

            horizontal = (int)Input.GetAxisRaw("Horizontal");
            vertical = (int)Input.GetAxisRaw("Vertical");

            if (horizontal != 0)
                vertical = 0;

            if (horizontal != 0 || vertical != 0)
                Move(horizontal, vertical);
        }

        void Move(int hor, int ver)
        {
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(hor, ver);

			RaycastHit2D hit;

			boxCollider.enabled = false;
			hit = Physics2D.Linecast(start, end, blockingLayer);
			boxCollider.enabled = true;

			if(hit != null && hit.transform != null)
			{
				Wall wall = hit.transform.GetComponent<Wall>();
				if(wall != null)
				{
					wall.DamageWall(wallDamage);
					animator.SetTrigger("playerChop");
					GameManager.instance.playersTurn = false;
				}
			}
			else
            {
                food--;
                foodText.text = "Food: " + food;
				rigid.MovePosition(end);
				SoundManager.instance.RandomizeSfx(moveSounds);
				GameManager.instance.playersTurn = false;
			}

            CheckIfGameOver();
        }

        void CheckIfGameOver()
        {
            if (food <= 0)
            {
                SoundManager.instance.musicSource.Stop();
                SoundManager.instance.PlaySingle(gameOverSound);
                GameManager.instance.GameOver();

            }
        }

		public void DamagePlayer(int dmg)
		{
            food -= dmg;
            foodText.text = "-" + dmg + " food: " + food;
            animator.SetTrigger("playerHit");
            CheckIfGameOver();
		}

        void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.tag.Equals("Exit"))
            {
                Invoke("RestartLevel", restartLevelDelay);

                enabled = false;
            }
            else if(collider.tag.Equals("Food"))
            {
                food += pointsPerFood;
                foodText.text = "+" + pointsPerFood + " food: " + food;
				SoundManager.instance.RandomizeSfx(eatSounds);
                collider.gameObject.SetActive(false);
            }
            else if(collider.tag.Equals("Soda"))
            {
                food += pointsPerSoda;
                foodText.text = "+" + pointsPerSoda + " food: " + food;
				SoundManager.instance.RandomizeSfx(drinkSounds);
                collider.gameObject.SetActive(false);
            }
        }

        void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
    }
}