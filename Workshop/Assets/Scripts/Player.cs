﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CompletedWorkshop
{

    public class Player : MonoBehaviour
    {
        public float restartLevelDelay = 1f;
        public int pointsPerFood = 10;
        public int pointsPerSoda = 20;
        public LayerMask blockingLayer;

        private int food;

        private Rigidbody2D rigid;

        void Start()
        {
            food = GameManager.instance.playerFoodPoints;

            rigid = GetComponent<Rigidbody2D>();
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
            food--;

            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(hor, ver);

            rigid.MovePosition(end);
            GameManager.instance.playersTurn = false;
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
                collider.gameObject.SetActive(false);
            }
            else if(collider.tag.Equals("Soda"))
            {
                food += pointsPerSoda;
                collider.gameObject.SetActive(false);
            }
        }

        void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
    }
}