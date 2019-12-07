using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitInTheHole_Template : MonoBehaviour
{
    [SerializeField] private Transform[] m_Cubes;
    [SerializeField] private Transform m_PlayerPosition;
    [SerializeField] private Transform[] m_PositionVariants;
    private int currentPosition;
    private FitInTheHole_FigureTweener tweener; //экземпляр класса, отвечающий за повороты кубика

    public Transform CurrentTarget { get; private set; }

    /// <summary>
    /// Получение массива со всеми блоками фигуры
    /// </summary>
    /// <returns></returns>
    public Transform[] GetFigure()
    {
        var figure = new Transform[m_Cubes.Length + 1];
        m_Cubes.CopyTo(figure, 0);
        figure[figure.Length - 1] = CurrentTarget;
        return figure;
    }

    /// <summary>
    /// Постройка случайной фигуры
    /// </summary>
    public void SetupRandomFigure()
    {
        int rand = Random.Range(0, m_PositionVariants.Length);
        for (int i = 0; i < m_PositionVariants.Length; i++)
        {
            if (i == rand)
            {
                m_PositionVariants[i].gameObject.SetActive(true);
                CurrentTarget = m_PositionVariants[i].transform;
                continue;
            }

            m_PositionVariants[i].gameObject.SetActive(false);
        }
    }


    private void Update()
    {
        if (tweener)
            return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveLeft();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveRight();
    }

    //TODO работает только в случае расположеия стартовой точки справа от фигур!!!
    private void MoveLeft()
    {
        if (!IsMovementPossible(1))
            return;

        currentPosition += 1;
        tweener = m_PlayerPosition.gameObject.AddComponent<FitInTheHole_FigureTweener>();
        tweener.Tween(m_PlayerPosition.position, m_PositionVariants[currentPosition].position);
    }


    private void MoveRight()
    {
        if (!IsMovementPossible(-1))
            return;

        currentPosition -= 1;
        tweener = m_PlayerPosition.gameObject.AddComponent<FitInTheHole_FigureTweener>();
        tweener.Tween(m_PlayerPosition.position, m_PositionVariants[currentPosition].position);
    }

    private bool IsMovementPossible(int dir)
    {
        return currentPosition + dir >= 0 && currentPosition + dir < m_PositionVariants.Length;
    }
}