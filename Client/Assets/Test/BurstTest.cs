using Unity.Collections;
using Unity.Jobs;

using UnityEngine;

public class BurstTest : MonoBehaviour {
    private NativeArray<float> ret = new NativeArray<float>(2, Allocator.Temp);
    private JobHandle job = new JobHandle();
    //private readonly ComputeBuffer buffer = new ComputeBuffer(1, 2);

    public Camera camera;

    private void Awake() {
        //this.buffer.Release();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            Debug.LogError(this.camera.projectionMatrix);

            Debug.LogError(this.camera.projectionMatrix.GetRow(2));
        }
    }
}

