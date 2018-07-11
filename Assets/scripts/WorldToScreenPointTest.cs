using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    public GameObject cube;

	// Use this for initialization
	void Start () {
        SetPos();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void SetPos()
    {
        Matrix4x4 angle = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, Mathf.Cos(-30 / Mathf.Rad2Deg), Mathf.Sin(-30 / Mathf.Rad2Deg), 0), new Vector4(0, -Mathf.Sin(-30 / Mathf.Rad2Deg), Mathf.Cos(-30 / Mathf.Rad2Deg), 0), new Vector4(0, 0, 0, 1));
        Matrix4x4 pos = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, -10, 10, 1));
        var a = angle * pos;
        Matrix4x4 x = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, -1, 0), new Vector4(0, 0, 0, 1));
        var b = x * a;

        var c = b * new Vector4(9, 4, 6, 1);

        float fov = Camera.main.fieldOfView;

        float far = Camera.main.farClipPlane;
        float near = Camera.main.nearClipPlane;

        float fov2 = (fov / 2) * Mathf.Deg2Rad;

        float nearHeight = 2 * near * Mathf.Tan(fov2);
        float farHeight = 2 * far * Mathf.Tan(fov2);

        float aspect = 16f / 9f;

        float dx = c.x * ((near / (nearHeight / 2)) / aspect);
        float dy = c.y * (near / (nearHeight / 2));

        float sx = Mathf.Lerp(0, 1920, (dx / Mathf.Abs(c.z) + 1) / 2);
        float sy = Mathf.Lerp(0, 1080, (dy / Mathf.Abs(c.z) + 1) / 2);

        Debug.Log(sx +" : "+ sy);
        Debug.Log(Camera.main.WorldToScreenPoint(cube.transform.position));
    }
}
