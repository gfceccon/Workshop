using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

namespace CompletedWorkshop
{
    public class Enemy : MonoBehaviour
    {
        public string targetTag = "Player";

        private Transform target;
        private Rigidbody2D rigid;

        void Start()
        {
            GameManager.instance.AddEnemyToList(this);

            target = GameObject.FindGameObjectWithTag(targetTag).transform;

            rigid = GetComponent<Rigidbody2D>();
        }

        void Update()
        {

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

            rigid.MovePosition(end);
        }
    }
}