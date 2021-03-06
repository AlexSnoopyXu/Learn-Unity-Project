﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSkeletalAnimation : MonoBehaviour
{
    public class Bone
    {
        public Transform firstChild;
        public Transform parent;
        public Transform self;
    }
    private Mesh mesh;
    public Transform roots;
    private List<Matrix4x4> translate;
    private Transform[] bones;
    private List<Vector3> offset;
    public SkinnedMeshRenderer skinned;

    // Use this for initialization
    void Start()
    {
        translate = new List<Matrix4x4>();
        offset = new List<Vector3>();
        mesh = this.GetComponent<MeshFilter>().mesh;
        bones = skinned.bones;
        GetBones();
        GetOffset();
        Debug.Log("");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        BoneWeight[] boneWeights = mesh.boneWeights;
        GetBones();
        int length = mesh.vertices.Length;
        List<Vector3> newVertices = new List<Vector3>();
        for (int i = 0; i < length; i++)
        {
            newVertices.Add(translate[boneWeights[i].boneIndex0].MultiplyPoint(offset[i * 4]) * boneWeights[i].weight0 +
            translate[boneWeights[i].boneIndex1].MultiplyPoint(offset[i * 4 + 1]) * boneWeights[i].weight1 +
            translate[boneWeights[i].boneIndex2].MultiplyPoint(offset[i * 4 + 2]) * boneWeights[i].weight2 +
            translate[boneWeights[i].boneIndex3].MultiplyPoint(offset[i * 4 + 3]) * boneWeights[i].weight3);
        }
        mesh.SetVertices(newVertices);
    }

    private void GetOffset()
    {
        Vector3[] vertices = mesh.vertices;
        BoneWeight[] boneWeights = mesh.boneWeights;
        offset.Clear();
        for (int i = 0; i < vertices.Length; i++)
        {
            offset.Add(mesh.bindposes[boneWeights[i].boneIndex0].MultiplyPoint(vertices[i]));
            offset.Add(mesh.bindposes[boneWeights[i].boneIndex1].MultiplyPoint(vertices[i]));
            offset.Add(mesh.bindposes[boneWeights[i].boneIndex2].MultiplyPoint(vertices[i]));
            offset.Add(mesh.bindposes[boneWeights[i].boneIndex3].MultiplyPoint(vertices[i]));
        }
    }

    private void GetBones()
    {
        translate.Clear();
        
        for(int i=0;i< bones.Length; i++)
        {
            translate.Add(MakeMatrix(bones[i])); 
        }
    }

    private Matrix4x4 GetScaleMatrix(Vector3 scale)
    {
        return new Matrix4x4(
            new Vector4(scale.x, 0, 0, 0)
            , new Vector4(0, scale.y, 0, 0)
            , new Vector4(0, 0, scale.z, 0)
            , new Vector4(0, 0, 0, 1));
    }

    private Matrix4x4 GetRotateZMatrix(Vector3 euler)
    {
        return new Matrix4x4(
            new Vector4(Mathf.Cos(euler.z / Mathf.Rad2Deg), Mathf.Sin(euler.z / Mathf.Rad2Deg), 0, 0)
            , new Vector4(-Mathf.Sin(euler.z / Mathf.Rad2Deg), Mathf.Cos(euler.z / Mathf.Rad2Deg), 0, 0)
            , new Vector4(0, 0, 1, 0)
            , new Vector4(0, 0, 0, 1));
    }

    private Matrix4x4 GetRotateXMatrix(Vector3 euler)
    {
        return new Matrix4x4(
            new Vector4(1, 0, 0, 0)
            , new Vector4(0, Mathf.Cos(euler.x / Mathf.Rad2Deg), Mathf.Sin(euler.x / Mathf.Rad2Deg), 0)
            , new Vector4(0, -Mathf.Sin(euler.x / Mathf.Rad2Deg), Mathf.Cos(euler.x / Mathf.Rad2Deg), 0)
            , new Vector4(0, 0, 0, 1));
    }

    private Matrix4x4 GetRotateYMatrix(Vector3 euler)
    {
        return new Matrix4x4(
            new Vector4(Mathf.Cos(euler.y / Mathf.Rad2Deg), 0, -Mathf.Sin(euler.y / Mathf.Rad2Deg), 0)
            , new Vector4(0, 1, 0, 0)
            , new Vector4(Mathf.Sin(euler.y / Mathf.Rad2Deg), 0, Mathf.Cos(euler.y / Mathf.Rad2Deg), 0)
            , new Vector4(0, 0, 0, 1));
    }

    private Matrix4x4 GetTranslateMatrix(Vector3 position)
    {
        return new Matrix4x4(
            new Vector4(1, 0, 0, 0)
            , new Vector4(0, 1, 0, 0)
            , new Vector4(0, 0, 1, 0)
            , new Vector4(position.x, position.y, position.z, 1));
    }

    private Matrix4x4 MakeMatrix(Transform trans)
    {
        Matrix4x4 tran = GetTranslateMatrix(trans.localPosition)
            * GetRotateYMatrix(trans.localEulerAngles)
            * GetRotateXMatrix(trans.localEulerAngles)
            * GetRotateZMatrix(trans.localEulerAngles)
            * GetScaleMatrix(trans.lossyScale);
        if (HasParent(trans))
        {
            tran = MakeMatrix(trans.parent) * tran;
        }
        return tran;
        
    }

    private bool HasParent(Transform child)
    {
        if (Array.Exists<Transform>(bones, (bone) =>
        {
            if (child.parent == bone)
            {
                return true;
            }
            return false;
        })) {
            return true;
        }
        return false;
    }
}
