using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSnake_Snake : MonoBehaviour
{
    [SerializeField] private ColorSnake_GameController m_GameController;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    private Vector3 position;
    private int currentType;

    void Start()
    {
        position = transform.position;
        var colorType = m_GameController.Types.GetRandomColorType();
        currentType = colorType.Id;
        m_SpriteRenderer.color = colorType.Color;
    }

    private void SetColor(int id)
    {
        var colorType = m_GameController.Types.GetColorType(id);
        currentType = colorType.Id;
        m_SpriteRenderer.color = colorType.Color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var obstacle = other.gameObject.GetComponent<ColorSnake_Obstacle>();
        if (!obstacle)
        {
            return;
        }

        SetColor(obstacle.ColorId);
        Destroy(obstacle.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;

        if (!Input.GetMouseButton(0))
        {
            return;
        }

        position.x = m_GameController.MainCamera.ScreenToWorldPoint(Input.mousePosition).x;

        float min = m_GameController.Bounds.Left;
        float max = m_GameController.Bounds.Right;

        position.x = Mathf.Clamp(position.x, min, max);
        transform.position = position;
    }
}
