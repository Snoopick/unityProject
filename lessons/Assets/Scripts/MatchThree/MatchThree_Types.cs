using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchThree_Types : MonoBehaviour
{
    [SerializeField] private MatchThree_CandyData[] m_CandiesData;
    public MatchThree_Candy GetRandomCandy()
    {
        MatchThree_CandyData candyData = m_CandiesData[Random.Range(0, m_CandiesData.Length)];
        GameObject obj = Instantiate(candyData.Prefab);
        obj.name = $"Candy";
        MatchThree_Candy candy = obj.AddComponent<MatchThree_Candy>();
        candy.CandyData = candyData;
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        spriteRenderer.color = candyData.Color;
        
        return candy;
    }
}
