using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

namespace CompletedWorkshop
{
    public class Enemy : MonoBehaviour
    {
		public string targetTag = "Player";
		public int playerDamage;
		public LayerMask blockingLayer;
		public AudioClip[] enemySounds;

		private Animator animator;
        private Transform target;
		private BoxCollider2D boxCollider;
        private Rigidbody2D rigid;

        void Start()
        {
            GameManager.instance.AddEnemyToList(this);

            target = GameObject.FindGameObjectWithTag(targetTag).transform;

            rigid = GetComponent<Rigidbody2D>();
			boxCollider = GetComponent<BoxCollider2D>();
			animator = GetComponent<Animator>();
        }

        public void MoveEnemy()
        {
            int hor = 0;
            int ver = 0;

            if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
                ver = target.position.y > transform.position.y ? 1 : -1;
            else
                hor = target.position.x > transform.position.x ? 1 : -1;


            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(hor, ver);



			RaycastHit2D hit;

			boxCollider.enabled = false;
			hit = Physics2D.Linecast(start, end, blockingLayer);
			boxCollider.enabled = true;

			if(hit != null && hit.transform != null)
			{
				Player player = hit.transform.GetComponent<Player>();
				if(player != null)
				{
					player.DamagePlayer(playerDamage);
					animator.SetTrigger("enemyAttack");
					SoundManager.instance.RandomizeSfx(enemySounds);
					GameManager.instance.playersTurn = false;
				}
			}
			else
			{
				rigid.MovePosition(end);
				GameManager.instance.playersTurn = false;
			}
        }
    }
}