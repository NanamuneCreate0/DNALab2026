using System.Collections.Generic;
using UnityEngine;

public class SpeedAutoScaleGraph : MonoBehaviour
{
    public Creature targetCreature;

    // グラフ窓 16:9、固定サイズ
    public Vector2 graphPosition = new Vector2(10, 10);
    private Vector2 graphSize => new Vector2(640, 360); // 16:9

    // 色
    public Color borderColor = Color.white;
    public Color backgroundColor = new Color(0, 0, 0, 0.3f);
    public Color lineColor = Color.green;

    private List<float> timeHistory = new List<float>();
    private List<float> speedHistory = new List<float>();

    void Update()
    {
        if (targetCreature == null) return;

        float t = Time.time;
        timeHistory.Add(t);
        speedHistory.Add(targetCreature.Speed);
    }

    void OnGUI()
    {
        if (targetCreature == null || speedHistory.Count < 2) return;

        float minTime = timeHistory[0];
        float maxTime = timeHistory[timeHistory.Count - 1];

        float minSpeed = float.MaxValue;
        float maxSpeed = float.MinValue;

        foreach (float s in speedHistory)
        {
            if (s < minSpeed) minSpeed = s;
            if (s > maxSpeed) maxSpeed = s;
        }

        // 過度に狭くならないようマージンを追加
        float speedMargin = (maxSpeed - minSpeed) * 0.1f;
        if (speedMargin == 0f) speedMargin = 1f;
        minSpeed -= speedMargin;
        maxSpeed += speedMargin;

        Vector2 size = graphSize;

        // 背景
        GUI.color = backgroundColor;
        GUI.DrawTexture(new Rect(graphPosition.x, graphPosition.y, size.x, size.y), Texture2D.whiteTexture);

        // 枠
        GUI.color = borderColor;
        GUI.DrawTexture(new Rect(graphPosition.x, graphPosition.y, size.x, 2), Texture2D.whiteTexture); // 上
        GUI.DrawTexture(new Rect(graphPosition.x, graphPosition.y + size.y - 2, size.x, 2), Texture2D.whiteTexture); // 下
        GUI.DrawTexture(new Rect(graphPosition.x, graphPosition.y, 2, size.y), Texture2D.whiteTexture); // 左
        GUI.DrawTexture(new Rect(graphPosition.x + size.x - 2, graphPosition.y, 2, size.y), Texture2D.whiteTexture); // 右

        // 線描画
        for (int i = 1; i < speedHistory.Count; i++)
        {
            float t0 = timeHistory[i - 1];
            float t1 = timeHistory[i];
            float s0 = speedHistory[i - 1];
            float s1 = speedHistory[i];

            // x軸スケーリング
            float x0 = graphPosition.x + (t0 - minTime) / (maxTime - minTime) * size.x;
            float x1 = graphPosition.x + (t1 - minTime) / (maxTime - minTime) * size.x;

            // y軸スケーリング（反転）
            float y0 = graphPosition.y + size.y - (s0 - minSpeed) / (maxSpeed - minSpeed) * size.y;
            float y1 = graphPosition.y + size.y - (s1 - minSpeed) / (maxSpeed - minSpeed) * size.y;

            DrawLine(new Vector2(x0, y0), new Vector2(x1, y1), lineColor, 2f);
        }
    }

    void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
    {
        Matrix4x4 matrix = GUI.matrix;
        Color savedColor = GUI.color;

        GUI.color = color;
        float angle = Vector3.Angle(pointB - pointA, Vector2.right);
        if (pointA.y > pointB.y) angle = -angle;

        GUIUtility.RotateAroundPivot(angle, pointA);
        GUI.DrawTexture(new Rect(pointA.x, pointA.y, (pointB - pointA).magnitude, width), Texture2D.whiteTexture);

        GUI.matrix = matrix;
        GUI.color = savedColor;
    }
}