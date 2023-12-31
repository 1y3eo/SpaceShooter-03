using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform targetTr;
    private Transform camTr;

    [Range(2.0f, 20.0f)]
    public float distance = 10.0f;

    [Range(0.0f, 10.0f)]
    public float height = 2.0f;

    // 반응 속도
    public float damping = 10.0f;

    public float targetOffset = 2.0f;

    // smoothDamp에서 사용할 변수
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        camTr = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        Vector3 pos = targetTr.position + (-targetTr.forward * distance) + (Vector3.up * height);

        // camTr.position = Vector3.Slerp(camTr.position, pos, Time.deltaTime * damping);
        // Slerp(시작 위치, 목표 위치, 시간 t)
        camTr.position = Vector3.SmoothDamp(camTr.position, pos, ref velocity, Time.deltaTime * damping); 
        // SmoothDamp(시작 위치, 목표 위치, 현재 속도, 시간 t)

        camTr.LookAt(targetTr.position + (targetTr.up * targetOffset));
    }
}
