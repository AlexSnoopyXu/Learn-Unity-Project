using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

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

    // 顶点
    List<Vector3> vertices = new List<Vector3>();
    // 顶点顺序
    List<int> triangles = new List<int>();
    // uv
    List<Vector2> uv = new List<Vector2>();
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
        //mesh.uv = uv.ToArray();
        //mesh.normals = normals.ToArray();
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
                vertices.Add(new Vector3(float.Parse(temp[1]), float.Parse(temp[2]), float.Parse(temp[3])));
                continue;
            }
            else if (uvRegex.Match(strs[i]).Value != "")
            {
                string[] temp = strs[i].Split(' ');
                uv.Add(new Vector2(float.Parse(temp[1]), float.Parse(temp[2])));
                continue;
            }
            else if (normalsRegex.Match(strs[i]).Value != "")
            {
                string[] temp = strs[i].Split(' ');
                normals.Add(new Vector3(float.Parse(temp[1]), float.Parse(temp[2]), float.Parse(temp[3])));
                continue;
            }
            else if (fRegex.Match(strs[i]).Value != "")
            {
                string[] temp = strs[i].Split(' ');
                fTemp.Add(temp[1]);
                fTemp.Add(temp[2]);
                fTemp.Add(temp[3]);
                fTemp.Add(temp[4]);
                triangles.Add(int.Parse(temp[1].Split('/')[0]));
                triangles.Add(int.Parse(temp[2].Split('/')[0]));
                triangles.Add(int.Parse(temp[3].Split('/')[0]));
                triangles.Add(int.Parse(temp[1].Split('/')[0]));
                triangles.Add(int.Parse(temp[3].Split('/')[0]));
                triangles.Add(int.Parse(temp[4].Split('/')[0]));
                continue;
            }
            objStrs += strs[i];
            objStrs += "\n";
        }
    }
}
