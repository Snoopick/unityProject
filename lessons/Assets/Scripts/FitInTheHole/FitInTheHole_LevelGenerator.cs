using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitInTheHole_LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject m_CubePrefab;
    [SerializeField] private float m_BaseSpeed = 2f;
    [SerializeField] private float m_WallDistance = 35f;

    [SerializeField] private FitInTheHole_Template[] m_TemplatePrefabs;
    [SerializeField] private Transform m_FigurePoint;

    private float speed;
    private FitInTheHole_Wall wall;

    private FitInTheHole_Template[] templates;
    private FitInTheHole_Template figure;

    // Start is called before the first frame update
    void Start()
    {
        templates = new FitInTheHole_Template[m_TemplatePrefabs.Length];
        
        for (int i = 0; i < templates.Length; i++)
        {
            templates[i] = Instantiate(m_TemplatePrefabs[i]);
            templates[i].gameObject.SetActive(false);
            templates[i].transform.position = m_FigurePoint.position;
        }

        wall = new FitInTheHole_Wall(5, 5, m_CubePrefab);
        SetupTemplate();
        wall.SetupWall(figure, m_WallDistance);
        speed = m_BaseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        wall.Parent.transform.Translate(speed * Time.deltaTime * Vector3.back);
        if (wall.Parent.transform.position.z > m_WallDistance *-1f)
        {
            return;
        }

        SetupTemplate();
        wall.SetupWall(figure, m_WallDistance);
    }

    private void SetupTemplate ()
    {
        if (figure)
        {
            figure.gameObject.SetActive(false);
        }

        var rand = Random.Range(0, templates.Length);
        figure = templates[rand];
        figure.gameObject.SetActive(true);
        figure.SetupRandomFigure();
    }
}
