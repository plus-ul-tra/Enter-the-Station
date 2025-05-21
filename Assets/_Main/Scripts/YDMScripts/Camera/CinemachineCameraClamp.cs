using UnityEngine;
using Unity.Cinemachine;

[SaveDuringPlay]
public class CinemachineCameraClamp : CinemachineExtension
{
    public Vector2 minPos; // (xMin, yMin)
    public Vector2 maxPos; // (xMax, yMax)
    //카메라 이동제한
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Finalize)
        {
            Vector3 p = state.RawPosition;

            p.x = Mathf.Clamp(p.x, minPos.x, maxPos.x);
            p.y = Mathf.Clamp(p.y, minPos.y, maxPos.y);
            state.RawPosition = p;
        }
    }
}
