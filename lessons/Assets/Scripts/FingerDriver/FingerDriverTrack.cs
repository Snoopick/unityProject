using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerDriverTrack : MonoBehaviour
{
    private class TrackSegment
    {
        public Vector3[] Points;
        public bool IsPointSegment(Vector3 point)
        {
            return MathfTriangles.IsPointInTriangleXY(point, Points[0], Points[1], Points[2]);
        }
    }

    private class Checkpoints
    {
        private Vector3[] ChPoints = new Vector3[20];

        public bool IsPointInCheckpoint(Vector3 point)
        {
            return MathfTriangles.IsPointInTriangleXY(point, ChPoints[0], ChPoints[1], ChPoints[2]);
        }

        public void CreateCheckpoint(TrackSegment Segment, GameObject CheckpointPref)
        {
            Array.Resize(ref ChPoints, ChPoints.Length + 1);
            ChPoints[0] = Segment.Points[0];
            ChPoints[1] = Segment.Points[1];
            ChPoints[2] = Segment.Points[2];

            //GameObject chp = Instantiate(CheckpointPref);
            //chp.transform.position = new Vector3(ChPoints[0].x, ChPoints[1].y, 0);
        }
    }

    [SerializeField] private LineRenderer m_lineRenderer;
    [SerializeField] private bool  m_debug;

    private Vector3[] corners;
    private TrackSegment[] segments;

    private int CheckpointsCounter = 1;
    private Checkpoints CheckpointTrack = new Checkpoints();
    private int Score = 0;
    public GameObject ScoreText;
    public GameObject CheckpointPref;

    // Start is called before the first frame update
    private void Start()
    {
        // fill corners array
        corners = new Vector3[transform.childCount];
        for (int i = 0; i < corners.Length; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            corners[i] = obj.transform.position;
            obj.GetComponent<MeshRenderer>().enabled = false;
        }

        //Line Renderer settings
        m_lineRenderer.positionCount = corners.Length;
        m_lineRenderer.SetPositions(corners);

        // Line Renderer mesh
        Mesh mesh = new Mesh();
        m_lineRenderer.BakeMesh(mesh, true);

        //array of track segments
        segments = new TrackSegment[mesh.triangles.Length / 3];
        int segmentCounter = 0;

        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            segments[segmentCounter] = new TrackSegment();
            segments[segmentCounter].Points = new Vector3[3];
            segments[segmentCounter].Points[0] = mesh.vertices[mesh.triangles[i]];
            segments[segmentCounter].Points[1] = mesh.vertices[mesh.triangles[i + 1]];
            segments[segmentCounter].Points[2] = mesh.vertices[mesh.triangles[i + 2]];

            segmentCounter++;
        }

        CreateNewCheckpoint();

        //segments debug
        if (!m_debug)
        {
            return;
        }

        foreach (var segment in segments)
        {
            foreach (var point in segment.Points)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = point;
                sphere.transform.localScale = Vector3.one * 0.1f;

            }
        }
    }

    public bool IsPointInTrack(Vector3 point)
    {

        if (IsPointReachedCheckpoint(point))
        {
            Score++;
            ScoreText.GetComponent<Text>().text = "Score: " + Score;
            CreateNewCheckpoint();
        }

        foreach (var segment in segments)
        {
            if (segment.IsPointSegment(point))
            {
                return true;
            }
        }

        return false;
    }

    public bool IsPointReachedCheckpoint(Vector3 point)
    {
        return CheckpointTrack.IsPointInCheckpoint(point);
    }

    public void CreateNewCheckpoint()
    {
        CheckpointTrack.CreateCheckpoint(segments[CheckpointsCounter], CheckpointPref);
        CheckpointsCounter++;
    }
}
