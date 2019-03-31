using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ShadowMap : MonoBehaviour
{
    public Camera lightCamera;
    public Material depthMat;
    public Material shadowMat;
    public RenderTexture rt;

    private void Awake()
    {
        lightCamera.backgroundColor = Color.white;
        lightCamera.clearFlags = CameraClearFlags.Color; ;
        lightCamera.targetTexture = rt;
        lightCamera.enabled = false;
        
        Shader.SetGlobalTexture("_DepthTexture", rt);
        lightCamera.RenderWithShader(depthMat.shader, "RenderType");
    }

    private void Start()
    {
        Matrix4x4 worldToView = lightCamera.worldToCameraMatrix;
        Matrix4x4 projection = GL.GetGPUProjectionMatrix(lightCamera.projectionMatrix, false);
        Matrix4x4 transMatrix = projection * worldToView;
        shadowMat.SetMatrix("_lightTransMatris", transMatrix);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //Graphics.Blit(source, destination, depthMat);
    }
}
