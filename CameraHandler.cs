using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    const string HORIZONTAL_MOVEMENT = "Horizontal";
    const string VERTICAL_MOVEMENT = "Vertical";

    [SerializeField] private int speedMultiplier = 0;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    void Start()
    {
    }

    void Update()
    {
        HandleXAndYMovement();

        ZoomInOut();
    }

    private void ZoomInOut()
    {
        var isPressingCtrlPlus = Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.K);
        var isPressingCtrlMinus = Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.J);

        if (isPressingCtrlPlus)
        {
            ZoomInOutCalculus(EOperations.SUM);
        }
        else if(isPressingCtrlMinus)
        {
            ZoomInOutCalculus(EOperations.SUBTRACT);
        }
    }

    private void ZoomInOutCalculus(EOperations operation)
    {
        int minSize = 10;
        int maxSize = 30;
        float targetOrthographicSize = default;

        float increaseAmount = 30;

        increaseAmount *= Time.deltaTime;

        targetOrthographicSize = SumOrSubtract(operation, targetOrthographicSize, increaseAmount);
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minSize, maxSize);

        virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, targetOrthographicSize, 0.5f);
    }

    private float SumOrSubtract(EOperations operation, float targetOrthographicSize, float increaseAmount)
    {
        if(EOperations.SUM == operation)
        {
            targetOrthographicSize = virtualCamera.m_Lens.OrthographicSize + increaseAmount;
        }
        else if(EOperations.SUBTRACT == operation)
        {
            targetOrthographicSize = virtualCamera.m_Lens.OrthographicSize - increaseAmount;
        }

        return targetOrthographicSize;
    }

    private void HandleXAndYMovement()
    {
        var horizontalMovement = Input.GetAxisRaw(HORIZONTAL_MOVEMENT);
        var verticalMovement = Input.GetAxisRaw(VERTICAL_MOVEMENT);

        var vectorMovement = new Vector3(horizontalMovement, verticalMovement).normalized;

        transform.position += vectorMovement * speedMultiplier * Time.deltaTime;
    }
}
