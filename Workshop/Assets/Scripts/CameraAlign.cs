using UnityEngine;
using System.Collections;

namespace CompletedWorkshop
{
    public class CameraAlign : MonoBehaviour
    {
        private Camera cameraScript;

        public void SetupCamera(int rows, int cols)
        {
            if(cameraScript == null)
                cameraScript = Camera.main;
            rows += 2;
            cols += 2;

            float cameraAspect = cameraScript.aspect;
            float boardAspect =  1f * cols / rows;


            if(boardAspect > cameraAspect)
                cameraScript.orthographicSize = cols / cameraAspect / 2f;
            else
                cameraScript.orthographicSize = rows / 2f;

            cameraScript.transform.position = new Vector3((cols - 3) / 2f, (rows - 3)/ 2f, -1f);

        }
    }
}