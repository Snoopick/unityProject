using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAG {
    public class Spawner : MonoBehaviour {
        public GameObject[] groups;
        public GameObject currentFigure;
        int i = -1;
        int nexFigureNum = -1;

        void Start() {
            spawnNext();
        }

        void Update()
        {
            if (currentFigure.transform.position.y >= 1)
            {
                currentFigure.transform.Translate(Vector3.down * Time.deltaTime);
            } 
            else
            {
                spawnNext();
            }
        }

        public void spawnNext() {
            if (nexFigureNum != -1)
            {
                i = nexFigureNum;
                nexFigureNum = Random.Range(0, groups.Length);
            }
            else
            {
                nexFigureNum = Random.Range(0, groups.Length);
            }

            if (i == -1)
            {
                i = Random.Range(0, groups.Length);
            }

            currentFigure = Instantiate(groups[i], transform.position, Quaternion.identity);
            FindObjectOfType<NextFigure>().ShowFigure(groups[nexFigureNum]);
        }
    }
}