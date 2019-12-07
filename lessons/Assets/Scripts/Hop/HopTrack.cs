using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopTrack : MonoBehaviour
{
    [SerializeField] private GameObject m_Platform;
    [SerializeField] private bool m_userRandomSeed;
    [SerializeField] private int m_seed = 123456;
    [SerializeField] private List<GameObject> m_Platforms;
    [SerializeField] private GameObject m_Player;

    private List<GameObject> platforms = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

        //platforms.Add(m_Platform);
        platforms.Add(m_Platforms[1]); //первая обычная

        if (m_userRandomSeed)
        {
            Random.InitState(m_seed);
        }

        for (int i = 0; i < 25; i++)
        {
            m_Platform = m_Platforms[Random.Range(0, m_Platforms.Count)];
            GameObject obj = Instantiate(m_Platform, transform);
            Vector3 pos = Vector3.zero;

            pos.z = 2 * (i + 1);
            pos.x = Random.Range(-1, 2);

            obj.transform.position = pos;
            obj.name = $"Platform_{i + 1}";
            platforms.Add(obj);
        }
    }

    public bool IsBallOnPlatform(Vector3 position)
    {

        var nearestPlatform = GetPlatform(position);
        float minX = nearestPlatform.transform.position.x - .5f;
        float maxX = nearestPlatform.transform.position.x + .5f;

        bool inPlatform = position.x > minX && position.x < maxX;

        if (inPlatform)
        {
            var platform = nearestPlatform.GetComponent<HopPlatform>();
            var player = m_Player.GetComponent<HopPlayer>();

            switch (platform.type)
            {
                case 1:
                    player.m_BallSpeed += .5f;
                    break;
                case 2:
                    player.m_JumpHeight += 1f;
                    break;
                case 3:
                    player.m_JumpDistance += 2f;
                    break;
                case 0:
                default:
                    break;
            }

            platform.SetGreen();
        }

        return inPlatform;
    }

    private GameObject GetPlatform (Vector3 position)
    {
        position.y = 0f;

        var nearestPlatform = platforms[0];
        var nearestPlatform_green = platforms[0];

        for (int i = 1; i < platforms.Count; i++)
        {
            var platformZ = platforms[i].transform.position.z;
            if (platformZ + .5f < position.z)
            {
                continue;
            }

            if (platformZ - position.z > .5f)
            {
                continue;
            }

            nearestPlatform = platforms[i];
            break;
        }

        return nearestPlatform;
    }
}
