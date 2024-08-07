using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CurveLineRenderer : MonoBehaviour
{
    public AnimationCurve curve;  // 曲线
    public int resolution = 100;  // 线条解析度
    public Transform startPoint;    // 起点
    public Transform endPoint;      // 终点
    public float curvature = 1f;  // 弯曲程度

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // 设置 LineRenderer 的顶点数量
        lineRenderer.positionCount = resolution + 1;

        // 生成弧线顶点
        GenerateCurve();
    }

    void Update()
    {
        // 生成弧线顶点
        GenerateCurve();
    }

    void GenerateCurve()
    {
        // 计算起点到终点的方向和距离
        Vector3 direction = endPoint.position - startPoint.position;
        float distance = direction.magnitude;
        direction.Normalize();

        // 计算每个顶点的位置
        for (int i = 0; i <= resolution; i++)
        {
            float t = i / (float)resolution;  // 计算 t 在 0 到 1 之间
            float x = t * distance;  // 计算 x 位置
            float y = curve.Evaluate(t) * curvature;  // 根据曲线和弯曲程度计算 y 位置

            // 计算当前顶点的局部坐标
            Vector3 localPosition = new Vector3(x, y, 0f);

            // 计算当前顶点的世界坐标
            Vector3 worldPosition = startPoint.position + direction * localPosition.x + Vector3.up * localPosition.y;

            // 设置顶点位置
            lineRenderer.SetPosition(i, worldPosition);
        }
    }
}
