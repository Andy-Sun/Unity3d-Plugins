/*
    Author:     AndySun
    Date:       2015-08-11
    Description:二次B样曲线
*/
using UnityEngine;
using Vectrosity;
using System.Collections.Generic;

public class B : MonoBehaviour
{
    public List<Vector2> controlPos;
    public Material mat;
    int PointIndex = 0;
    public float speed = 0.01f;
    float t = 0;
    VectorLine line;

    void Start()
    {
        line = new VectorLine("B", new List<Vector2>(), mat, 4, LineType.Continuous);
    }

    void Update()
    {
        if (controlPos.Count > PointIndex + 2)
        {
            Vector2 t1 = controlPos[PointIndex] + (controlPos[PointIndex + 1] - controlPos[PointIndex]) * t;
            Vector2 t2 = controlPos[PointIndex + 1] + (controlPos[PointIndex + 2] - controlPos[PointIndex + 1]) * t;
            Vector2 c1 = t1 + (t2 - t1) * t;

            line.points2.Add(c1);
            t += speed;
            line.Draw();
            if (t >= 1)
            {
                PointIndex += 2;
                t = 0;
            }
        }
    }
}
