using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkipOptionManager : MonoBehaviour
{
    [Header("��ŵ�ϱ� ��ư")]
    [SerializeField] private Button doSkip_Button;

    [Header("��ŵ Yes ��ư")]
    [SerializeField] private Button skipYes_Button;

    [Header("��ŵ No ��ư")]
    [SerializeField] private Button skipNo_Button;

    [Header("��ŵ â")]
    [SerializeField] private GameObject skipUI_Obj;

    [Header("��ŵ�ϰ� �������� �� �� �̸�")]
    [SerializeField] private String sceneName;

    [Header("�÷��̾� �߼Ҹ�")]
    [SerializeField] private PlayerFootsteps playerFootstpes;

    private void Start()
    {
        // �ʱ�ȭ
        skipUI_Obj.SetActive(false);

        // ��ŵ�ϱ� ��ư
        doSkip_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            if (playerFootstpes != null)
                playerFootstpes.StopfootstepsSound();

            // ���� ����
            Time.timeScale = 0f;

            // ��ŵâ ����
            skipUI_Obj.SetActive(true);
        });

        // ��ŵ ���� ��ư
        skipYes_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // ���� ����
            Time.timeScale = 1f;

            // Day1�� �̵��ϱ�
            DOTween.KillAll();
            SceneManager.LoadScene(sceneName);
        });

        // ��ŵ ���� ��ư
        skipNo_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // ���� �簳
            Time.timeScale = 1f;

            // ��ŵâ �ݱ�
            skipUI_Obj.SetActive(false);
        });
    }
}
