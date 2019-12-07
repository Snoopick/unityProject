using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HopPlayer : MonoBehaviour
{
    [SerializeField] private AnimationCurve m_JumpCurve;
    [SerializeField] public float m_JumpHeight = 1f;
    [SerializeField] public float m_JumpDistance = 2f;
    [SerializeField] public float m_BallSpeed = 1f;
    [SerializeField] private HopInput m_Input;
    [SerializeField] private HopTrack m_Track;

    private float iteration;
    private float startZ;

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        // strafe
        pos.x = Mathf.Lerp(pos.x, m_Input.Strafe, Time.deltaTime * 5f);
        // jump
        pos.y = m_JumpCurve.Evaluate(iteration) * m_JumpHeight;
        // forward
        pos.z = startZ + iteration * m_JumpDistance;
        transform.position = pos;

        iteration += Time.deltaTime * m_BallSpeed;

        if (iteration < 1f)
        {
            return;
        }

        iteration = 0;
        startZ += m_JumpDistance;

        if (m_Track.IsBallOnPlatform(transform.position))
        {
            //int platformType = m_Track.GetPlatformType();
            return;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
