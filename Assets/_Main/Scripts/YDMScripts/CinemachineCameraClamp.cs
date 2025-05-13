using UnityEngine;
using Unity.Cinemachine;

[SaveDuringPlay]
public class CinemachineCameraClamp : CinemachineExtension
{
    public Vector2 minPos; // (xMin, yMin)
    public Vector2 maxPos; // (xMax, yMax)
    //ī�޶� �̵�����
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Finalize)
        {
            //camera state ����� �� �����Ŀ� Ʈ������ ���� 
            //camera state : ��ġ��ȸ�������� ���� ��
            //Finalize �ܰ��, Body(Ʈ�������� �̵�), Aim(�ٶ�), Noise(��鸲), Extension ���� ���� ����
            // Finalize �ܰ��� CorrectedPosition �Ǵ� RawPosition ��
            //RawPosition �� ���������� ī�޶� �����ϡ� ���� ��ǥ
            //CorrectedPosition �� Cinemachine ���������ο��� Aim(Composer), Noise ���� ���� ó���� ���� ���� ��ġ

            Vector3 p = state.RawPosition;

            p.x = Mathf.Clamp(p.x, minPos.x, maxPos.x);
            p.y = Mathf.Clamp(p.y, minPos.y, maxPos.y);
            state.RawPosition = p;
        }
    }
}
