/*
    Author:     AndySun
    Date:       2015-08-11
    Description:二次B样曲线
    ChangeLog:
        2015-8-12
            Added:
                1.添加随机生成控制点列表
                2.将绘制面板置于UI面板之下
*/
using UnityEngine;
using Vectrosity;
using System.Collections.Generic;

public class B : MonoBehaviour
{
    bool setparent = false;//将Vectrosity的Canvas面板置于UI Canvas面板之下，便于统一管理
    public Transform UIParent;//要放置的UI Canvas面板
    public float speed = 0.01f;//曲线加载的速度
    public Material mat;//曲线材质
    public int randomNum = 20;//随机生成的控制点数量
    public float MAX_VALUE = 430f;//随机生成的控制点最大高度
    public float MIN_VALUE = 350f;//随机生成的控制点最小高度

    List<Vector2> controlPos;//控制点列表
    int PointIndex = 0;//当前操作的控制点索引
    float t = 0;//Bezier曲线插值绘制的进度
    VectorLine line;

    void Start()
    {
        line = new VectorLine("Bezier", new List<Vector2>(), mat, 2, LineType.Continuous);
        controlPos = new List<Vector2>();

        #region 曲线起始走向
        controlPos.Add(new Vector2(0, 0));
        controlPos.Add(new Vector2(100, 600));
        controlPos.Add(new Vector2(150, 400));
        #endregion

        #region 曲线随机生成
        float lastX = 160f;
        for (int i = 0; i < randomNum; i++)
        {
            lastX += Random.Range(5, 15);
            controlPos.Add(new Vector2(lastX, Random.Range(MIN_VALUE, MAX_VALUE)));
        }
        #endregion
        //曲线的最终走向
        controlPos.Add(new Vector2(400, 500));
    }

    void Update()
    {
        if (!setparent)
        {
            if (VectorLine.canvas)
            {
                VectorLine.canvas.transform.parent = UIParent;
                setparent = true;
            }
            return;
        }
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
