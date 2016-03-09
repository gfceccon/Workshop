using UnityEngine;
using System.Collections;

namespace CompletedWorkshop
{
    public class Loader : MonoBehaviour
    {
        public GameObject gameManager;


        void Awake()
        {
            if (GameManager.instance == null)
                Instantiate(gameManager);
        }
    }
}