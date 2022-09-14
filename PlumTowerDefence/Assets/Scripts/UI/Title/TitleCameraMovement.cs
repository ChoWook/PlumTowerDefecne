using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleCameraMovement : MonoBehaviour
{
    [Range(0f, 90f)]
    [SerializeField] float CameraAngle = 15;

    [Range(0f, 30f)]
    [SerializeField] float RotationTime = 6.5f;

    [Range(0f, 10f)]
    [SerializeField] float StopTime = 0.2f;

    [SerializeField] Ease AnimationEase = Ease.InOutQuad;

    Camera _Camera;



    private void Awake()
    {
        _Camera = Camera.main;

        _Camera.transform.rotation = Quaternion.Euler(new Vector3(0, -90 + CameraAngle, 0));

        StartCoroutine(IE_CameraMoveAnimation());
    }

    IEnumerator IE_CameraMoveAnimation()
    {
        while (true)
        {
            CameraAngle = -CameraAngle;
            _Camera.transform.DOKill();
            _Camera.transform.DORotate(new Vector3(0, -90 + CameraAngle, 0), RotationTime).SetEase(AnimationEase);

            yield return new WaitForSeconds(RotationTime + StopTime);
        }

    }
}
