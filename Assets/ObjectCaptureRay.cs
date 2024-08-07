using UnityEngine;

public class ObjectCaptureRay : MonoBehaviour
{
    public Transform handTransform; // 手的 Transform
    public Transform endTransform; // 射线的终点 Transform
    public LineRenderer lineRenderer; // LineRenderer 组件
    public int numPoints = 50; // LineRenderer 点数量
    public float curvature = 0.5f; // 曲度控制参数

    private Vector3[] linePoints; // LineRenderer 点数组

    void Start()
    {
        linePoints = new Vector3[numPoints];
    }

    void Update()
    {
        Vector3 startPoint = handTransform.position;
        Vector3 endPoint = endTransform.position;

        // 计算抛物线点
        for (int i = 0; i < numPoints; i++)
        {
            float t = (float)i / (numPoints - 1);
            linePoints[i] = CalculateArcPoint(startPoint, endPoint, t);
        }

        // 更新 LineRenderer
        lineRenderer.positionCount = numPoints;
        lineRenderer.SetPositions(linePoints);
    }

    Vector3 CalculateArcPoint(Vector3 startPoint, Vector3 endPoint, float t)
    {
        // 使用简单的抛物线公式计算弧度点
        Vector3 point = Vector3.Lerp(startPoint, endPoint, t);
        point.y += Mathf.Sin(t * Mathf.PI) * Vector3.Distance(startPoint, endPoint) * curvature;
        return point;
    }
}
