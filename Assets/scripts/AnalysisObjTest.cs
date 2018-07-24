using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class AnalysisObjTest : MonoBehaviour {
    const string FilePath = @"Assets\Obj\cube.txt";

    // 顶点匹配字符
    const string verticesMatch = @"\A^v\b";
    // uv匹配字符
    const string uvMatch = @"\A^vt\b";
    // 法线匹配字符
    const string normalsMatch = @"\A^vn\b";
    // 顺序组匹配字符
    const string fMatch = @"\A^f\b";

    // 解析出的字符串
    string objStrs = "";

    // 顶点匹配
    Regex verticesRegex = new Regex(verticesMatch);
    // uv匹配
    Regex uvRegex = new Regex(uvMatch);
    // 法线匹配
    Regex normalsRegex = new Regex(normalsMatch);
    // 顺序组匹配
    Regex fRegex = new Regex(fMatch);

    // 文件中顶点
    List<Vector3> txtVertices = new List<Vector3>();
    // 顶点
    List<Vector3> vertices = new List<Vector3>();
    // 顶点顺序
    List<int> triangles = new List<int>();
    // 文件中的uv
    List<Vector2> txtUv = new List<Vector2>();
    // uv
    List<Vector2> uv = new List<Vector2>();
    // 文件中的法线
    List<Vector3> txtNormals = new List<Vector3>();
    // 法线
    List<Vector3> normals = new List<Vector3>();
    // 顺序组
    List<string> fTemp = new List<string>();

    // Use this for initialization
    void Start () {
        AnalysisTxtFile();
        Debug.Log(objStrs);
        DrawCube();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 画正方体
    /// </summary>
    void DrawCube()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uv.ToArray();
        mesh.normals = normals.ToArray();
    }

    /// <summary>
    /// 解析obj.txt文件内容
    /// </summary>
    void AnalysisTxtFile()
    {
        string[] strs = File.ReadAllLines(FilePath);
        for (int i = 0; i < strs.Length; i++)
        {
            if(verticesRegex.Match(strs[i]).Value != "")
            {
                string[] temp = strs[i].Split(' ');
                txtVertices.Add(new Vector3(float.Parse(temp[1]), float.Parse(temp[2]), float.Parse(temp[3])));
                continue;
            }
            else if (uvRegex.Match(strs[i]).Value != "")
            {
                string[] temp = strs[i].Split(' ');
                txtUv.Add(new Vector2(float.Parse(temp[1]), float.Parse(temp[2])));
                continue;
            }
            else if (normalsRegex.Match(strs[i]).Value != "")
            {
                string[] temp = strs[i].Split(' ');
                txtNormals.Add(new Vector3(float.Parse(temp[1]), float.Parse(temp[2]), float.Parse(temp[3])));
                continue;
            }
            else if (fRegex.Match(strs[i]).Value != "")
            {
                string[] temp = strs[i].Split(' ');
                fTemp.Add(temp[1]);
                fTemp.Add(temp[2]);
                fTemp.Add(temp[3]);
                fTemp.Add(temp[4]);

                // 顶点
                vertices.Add(txtVertices[int.Parse(temp[1].Split('/')[0]) - 1]);
                vertices.Add(txtVertices[int.Parse(temp[2].Split('/')[0]) - 1]);
                vertices.Add(txtVertices[int.Parse(temp[3].Split('/')[0]) - 1]);
                vertices.Add(txtVertices[int.Parse(temp[4].Split('/')[0]) - 1]);
                // uv
                uv.Add(txtUv[int.Parse(temp[1].Split('/')[1]) - 1]);
                uv.Add(txtUv[int.Parse(temp[2].Split('/')[1]) - 1]);
                uv.Add(txtUv[int.Parse(temp[3].Split('/')[1]) - 1]);
                uv.Add(txtUv[int.Parse(temp[4].Split('/')[1]) - 1]);
                // 法线
                normals.Add(txtNormals[int.Parse(temp[1].Split('/')[2]) - 1]);
                normals.Add(txtNormals[int.Parse(temp[2].Split('/')[2]) - 1]);
                normals.Add(txtNormals[int.Parse(temp[3].Split('/')[2]) - 1]);
                normals.Add(txtNormals[int.Parse(temp[4].Split('/')[2]) - 1]);
                // 顶点顺序
                triangles.Add(int.Parse(temp[1].Split('/')[0]) - 1);
                triangles.Add(int.Parse(temp[2].Split('/')[0]) - 1);
                triangles.Add(int.Parse(temp[3].Split('/')[0]) - 1);
                triangles.Add(int.Parse(temp[1].Split('/')[0]) - 1);
                triangles.Add(int.Parse(temp[3].Split('/')[0]) - 1);
                triangles.Add(int.Parse(temp[4].Split('/')[0]) - 1);
                continue;
            }
            objStrs += strs[i];
            objStrs += "\n";
        }
    }
}
