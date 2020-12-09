using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// rt上添加该脚本，支持拖拽模型
public class CP_Drag3D : MonoBehaviour {
    public Graphic graphic;

    private void Awake() {
        if (graphic != null) {
            graphic = GetComponent<Graphic>();
        }
    }
}

public class RT3d {
    public GameObject gameObject { get; protected set; }

    public Transform transform {
        get { return gameObject.transform; }
    }

    public Camera camera { get; protected set; }
    public Transform pos { get; protected set; }

    public RenderTexture rt { get; protected set; }

    #region 上次设置

    protected int width;
    protected int height;
    private int depthBuffer;
    private RenderTextureFormat format = RenderTextureFormat.Default;

    #endregion

    public RT3d Parse(GameObject scene) {
        this.gameObject = scene;
        this.camera = scene.transform.Find("Camera").GetComponent<Camera>();
        this.pos = scene.transform.Find("Pos");
        return this;
    }

    public void Clear() {
        ReleaseCameraRT();
        ReleaseRT();

        camera = null;
        pos = null;

        if (gameObject != null) {
            GameObject.Destroy(gameObject);
            gameObject = null;
        }
    }

    // 支持拖拽?
    public void GetRT(RawImage rawImage, bool supportOp, int width, int height, int depthBuffer,
        RenderTextureFormat format,
        float scale) {
        if (rawImage != null) {
            rawImage.texture = GetRT(width, height, depthBuffer, format, scale);

            if (supportOp) {
                if (!rawImage.TryGetComponent<CP_Drag3D>(out CP_Drag3D _)) {
                    rawImage.gameObject.AddComponent<CP_Drag3D>();
                }
            }
        }
    }

    public RenderTexture GetRT(int width, int height, int depthBuffer, RenderTextureFormat format, float scale) {
        width = width > 0 ? width : Screen.width;
        height = height > 0 ? height : Screen.height;

        width = (int) (width * scale);
        height = (int) (height * scale);

        // 和已有的参数不匹配，直接Release
        if (width != this.width || height != this.height || format != this.format || depthBuffer != this.depthBuffer) {
            ReleaseRT();

            this.width = width;
            this.height = height;
            this.format = format;
            this.depthBuffer = depthBuffer;

            rt = RenderTexture.GetTemporary(width, height, depthBuffer, format);
            camera.targetTexture = rt;
        }

        return rt;
    }

    public void ReleaseCameraRT() {
        if (camera != null) {
            camera.targetTexture = null;
        }
    }

    public void ReleaseRT() {
        if (rt != null) {
            RenderTexture.ReleaseTemporary(rt);
            rt = null;
        }
    }
}