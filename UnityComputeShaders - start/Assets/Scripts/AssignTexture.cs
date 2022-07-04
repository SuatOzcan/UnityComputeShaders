﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignTexture : MonoBehaviour
{
    public ComputeShader shader;
    public int texResolution = 256;

    Renderer rend;
    RenderTexture outputTexture;
    int kernelHandle;

    private void Start()
    {
        outputTexture = new RenderTexture(texResolution, texResolution, 0);
        outputTexture.enableRandomWrite = true;
        outputTexture.Create();

        rend = GetComponent<Renderer>();
        rend.enabled = true;

        InitShader();

    }
    private void InitShader()
    {
        kernelHandle = shader.FindKernel("CSMain");
        shader.SetTexture(kernelHandle, "Result", outputTexture);
        rend.material.SetTexture("_MainTex", outputTexture);
        DispatchShader(texResolution / 16, texResolution / 16);
    }

    private void DispatchShader(int x, int y)
    {
        shader.Dispatch(kernelHandle, x, y, 1);

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.U))
        {
            DispatchShader(texResolution / 8, texResolution / 8);
        }
    }
}
