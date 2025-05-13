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
            //camera state 계산을 다 끝낸후에 트랜스폼 적용 
            //camera state : 위치·회전·렌즈 정보 등
            //Finalize 단계란, Body(트랜스포저 이동), Aim(바라봄), Noise(흔들림), Extension 연산 이후 적용
            // Finalize 단계의 CorrectedPosition 또는 RawPosition 중
            //RawPosition 은 최종적으로 카메라가 “놓일” 월드 좌표
            //CorrectedPosition 은 Cinemachine 파이프라인에서 Aim(Composer), Noise 등의 보정 처리가 끝난 뒤의 위치

            Vector3 p = state.RawPosition;

            p.x = Mathf.Clamp(p.x, minPos.x, maxPos.x);
            p.y = Mathf.Clamp(p.y, minPos.y, maxPos.y);
            state.RawPosition = p;
        }
    }
}
