using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshTest : MonoBehaviour {

    public GameObject sp;
    public Material mat;

	// Use this for initialization
	void Start () {
        Draw6();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 三角形面
    /// </summary>
    public void Draw1()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        //设置顶点
        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) };

        mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
        //设置三角形顶点顺序，顺时针设置
        mesh.triangles = new int[] { 0, 1, 2 };
    }

    /// <summary>
    /// 正方形面
    /// </summary>
    public void Draw2()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        //设置顶点
        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) ,new Vector3(1,0,0)};
        //设置正方形形顶点顺序，顺时针设置
        mesh.triangles = new int[]
        { 0, 1, 2,
        0,2,3
        };
    }

    /// <summary>
    /// 圆面
    /// </summary>
    public void Draw3()
    {
        // 半径
        float radius = 2;
        // 分隔数
        int segments = 1000;
        // 圆心坐标
        Vector3 centerCircle = new Vector3(2, 2, 0);

        //顶点
        Vector3[] vertices = new Vector3[segments + 1];
        vertices[0] = centerCircle;
        float deltaAngle = Mathf.Deg2Rad * 360f / segments;
        float currentAngle = 0;
        for (int i = 1; i < vertices.Length; i++)
        {
            float cosA = Mathf.Cos(currentAngle);
            float sinA = Mathf.Sin(currentAngle);
            vertices[i] = new Vector3(cosA * radius + centerCircle.x, sinA * radius + centerCircle.y, 0);
            currentAngle += deltaAngle;
        }

        //三角形顶点
        int[] triangles = new int[segments * 3];
        for (int i = 0, j = 1; i < segments * 3 - 3; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j;
        }
        triangles[segments * 3 - 3] = 0;
        triangles[segments * 3 - 2] = 1;
        triangles[segments * 3 - 1] = segments;


        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    /// <summary>
    /// 正方体
    /// </summary>
    public void Draw4()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        //设置顶点
        mesh.vertices = new Vector3[]
        {   new Vector3(0, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(1, 1, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 1, 1),
            new Vector3(1, 1, 1),
            new Vector3(1, 0, 1),
            new Vector3(0, 0, 1),
        };
        //设置三角形顶点顺序，顺时针设置
        mesh.triangles = new int[]
                {
            0,2,1,
            0,3,2,
            3,4,2,
            4,5,2,
            4,7,5,
            7,6,5,
            7,0,1,
            6,7,1,
            4,3,0,
            4,0,7,
            2,5,6,
            2,6,1

        };
    }

    /// <summary>
    /// 正八面体
    /// </summary>
    public void Draw5()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        //设置顶点
        mesh.vertices = new Vector3[] {
            Vector3.down,
            Vector3.forward,
            Vector3.left,
            Vector3.back,
            Vector3.right,
            Vector3.up
        };

        //设置三角形顶点顺序，顺时针设置
        mesh.triangles = new int[]
        {
            0, 2, 1,
            0, 3, 2,
            0, 4, 3,
            0, 1, 4,


            5, 1, 2,
            5, 2, 3,
            5, 3, 4,
            5, 4, 1

        };
    }

    /// <summary>
    /// 画球
    /// </summary>
    public void Draw6()
    {
        int R = 50;
        int L = 50;

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = mat;
        mesh.Clear();

        // 半径
        float radius = 10f;
        // 分隔数
        int segments = 100;

        //顶点
        List<Vector3> vertices = new List<Vector3>();

        //三角形顶点
        List<int> triangles = new List<int>();

        float deltaAngle = Mathf.Deg2Rad * 360f / segments;
        float h = 0.0f;
        float r = 0.0f;
        float s = Mathf.PI * radius * radius;
        for (int j =0;j<25;j++)
        {
            float currentAngle = 0;
            float s1 = s / 25 * j;
            r = Mathf.Sqrt(s1 / Mathf.PI);
            for (int i = 0; i < 100; i++)
            {
                float cosA = Mathf.Cos(currentAngle);
                float sinA = Mathf.Sin(currentAngle);
                vertices.Add(new Vector3(cosA * r, -Mathf.Sqrt(radius * radius - r * r), sinA * r));
                currentAngle += deltaAngle;

                //GameObject go = new GameObject();
                //go.transform.position = vertices[i];
                //go.AddComponent<MeshFilter>().mesh = sp.GetComponent<MeshFilter>().mesh;
                //go.AddComponent<MeshRenderer>();
                //go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }

            h += radius / 25;
        }

        for (int j = 25; j >= 0; j--)
        {
            float currentAngle = 0;
            h += radius / 25;
            float s1 = s / 25 * j;
            r = Mathf.Sqrt(s1 / Mathf.PI);
            for (int i = 0; i < 100; i++)
            {
                
                float cosA = Mathf.Cos(currentAngle);
                float sinA = Mathf.Sin(currentAngle);
                vertices.Add(new Vector3(cosA * r, Mathf.Sqrt(radius * radius - r * r), sinA * r));
                currentAngle += deltaAngle;

                //GameObject go = new GameObject();
                //go.transform.position = vertices[i];
                //go.AddComponent<MeshFilter>().mesh = sp.GetComponent<MeshFilter>().mesh;
                //go.AddComponent<MeshRenderer>();
                //go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
        }

        for(int j = 0; j < 50; j++)
        {
            for (int i = 0; i < 99; i++)
            {
                triangles.Add(j * 100 + i);
                triangles.Add(j * 100 + i + 1);
                triangles.Add((j + 1) * 100 + i);
                triangles.Add(j * 100 + i + 1);
                triangles.Add((j + 1) * 100 + i + 1);
                triangles.Add((j + 1) * 100 + i);
                
            }

            triangles.Add(j * 100 + 99);
            triangles.Add(j * 100 + 0);
            triangles.Add((j + 1) * 100 + 99);
            triangles.Add(j * 100 + 0);
            triangles.Add((j + 1) * 100);
            triangles.Add((j + 1) * 100 + 99);
        }

        List<Vector2> uv = new List<Vector2>();

        float vOffset = 1.0f / 50;
        float uOffset = 1.0f / 100;

        for (int i = 0; i < 51; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                uv.Add(new Vector2(j * uOffset, i * vOffset));
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uv.ToArray();
    }
}
