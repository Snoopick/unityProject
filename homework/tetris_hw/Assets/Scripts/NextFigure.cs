using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAG
{
    internal class NextFigure : MonoBehaviour
    {

        public GameObject nextFigure = null;

        public void ShowFigure(GameObject element)
        {
            if (nextFigure != null)
            {
                Destroy(nextFigure);
            }

            nextFigure = Instantiate(element, transform.position, Quaternion.identity);
        }
    }
}