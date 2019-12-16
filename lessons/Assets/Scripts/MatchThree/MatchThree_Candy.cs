using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchThree_Candy : MonoBehaviour
{
    public MatchThree_CandyData CandyData;
    private Vector3 baseCandyPosition;
    private bool inDrag;
    private Collider2D currentCollider;

    private void OnMouseDown()
    {
        baseCandyPosition = transform.position;
        inDrag = true;
    }


    private void OnMouseDrag()
    {
        if (!inDrag)
        {
            return;
        }

        var position = MatchThree_Controller.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        position.z = baseCandyPosition.z;
        //максимальное смещение конфеты
        float maxDistance = MatchThree_Field.CurrentCellSize;

        float x = position.x;
        float baseX = baseCandyPosition.x;
        float y = position.y;
        float baseY = baseCandyPosition.y;

        //ограничение смещения до одной ячейки
        x = Mathf.Clamp(x, baseX - maxDistance, baseX + maxDistance);
        y = Mathf.Clamp(y, baseY - maxDistance, baseY + maxDistance);
        //ограничение смещения по одной оси
        if (Mathf.Abs(x - baseX) > Mathf.Abs(y - baseY))
        {
            y = baseY;
        }
        else
        {
            x = baseX;
        }


        position.x = x;
        position.y = y;

        transform.position = position;
    }


    private void OnMouseUp()
    {
        inDrag = false;
        if (currentCollider)
        {
            MatchThree_Candy targetCandy = currentCollider.GetComponent<MatchThree_Candy>();
            var targetCell = MatchThree_Field.GetCell(targetCandy);
            var currentCell = MatchThree_Field.GetCell(this);

            //пытаемся поменять ячейки
            currentCell.Candy = targetCandy;
            targetCell.Candy = this;

            //проверить валидность
            bool isFreePlacement = MatchThree_Controller.IsFreeCandyPlacement(targetCell, CandyData.Id);
            if (isFreePlacement)
            {
                isFreePlacement = MatchThree_Controller.IsFreeCandyPlacement(currentCell, targetCandy.CandyData.Id);
            }

            //если смена НЕ приведет к совпадению 3-х
            if (isFreePlacement)
            {
                targetCell.Candy = targetCandy;
                currentCell.Candy = this;
                transform.position = baseCandyPosition;
                return;
            }

            //Если приведет к совпадению 3-х
            transform.position = targetCell.transform.position;
            targetCandy.transform.position = currentCell.transform.position;

            MatchThree_Controller.DeleteLine(this, targetCell, CandyData.Id);
//            MatchThree_Controller.DeleteLine(targetCandy, currentCell, CandyData.Id); // как то удалять если совпала переставленная конфетка =/

//            MatchThree_Controller.DragCandies("OnMouseUp 1");
            return;
        }
        
        transform.position = baseCandyPosition;
//        MatchThree_Controller.DragCandies("OnMouseUp 2");
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        currentCollider = other;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (currentCollider == other)
        {
            currentCollider = null;
        }
    }
}