using Cinemachine;
using KBCore.Refs;
using System;
using System.Collections;
using UnityEngine;

namespace DescentII
{
    public class CameraManager : ValidatedMonoBehaviour
    {
        [Header("References")]
        [SerializeField, Anywhere] InputReader input;
        [SerializeField, Anywhere] CinemachineFreeLook freeLookCam;

        [Header("Settings")]
        [SerializeField, Range(0.5f, 3f)] float speedMultilier = 1f;

        bool isRMBPressed;
        bool cameraMovementLock;

        private void OnEnable()
        {
            input.Look += OnLook;
            input.EnableMouseControlCamera += OnEnableMouseControlCamera;
            input.DisableMouseControlCamera += OnDisableMouseControlCamera;
        }

        private void OnDisable()
        {
            input.Look -= OnLook;
            input.EnableMouseControlCamera -= OnEnableMouseControlCamera;
            input.DisableMouseControlCamera -= OnDisableMouseControlCamera;
        }

        private void OnEnableMouseControlCamera()
        {
            isRMBPressed = true;

            // Lock the cursor to the center of the screen and hide it
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            StartCoroutine(DisableMouseForFrame());
        }

        private IEnumerator DisableMouseForFrame()
        {
            cameraMovementLock = true;
            yield return new WaitForEndOfFrame();
            cameraMovementLock = false;
        }

        private void OnDisableMouseControlCamera()
        {
            isRMBPressed = false;

            // Unlock the cursor and make it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Reset the camera axis to prevent jumping when re-enabling mouse control
            freeLookCam.m_XAxis.m_InputAxisValue = 0f;
            freeLookCam.m_YAxis.m_InputAxisValue = 0f;
        }

        private void OnLook(Vector2 cameraMovement, bool isDeviceMouse)
        {
            if (cameraMovementLock) return;

            if (isDeviceMouse && !isRMBPressed) return;

            // If the device is mouse use fixedDeltaTime, otherwise use deltaTime
            float deviceMultiplier = isDeviceMouse ? Time.fixedDeltaTime : Time.deltaTime;

            // Set the camera axis values
            freeLookCam.m_XAxis.m_InputAxisValue = cameraMovement.x * speedMultilier * deviceMultiplier;
            freeLookCam.m_YAxis.m_InputAxisValue = cameraMovement.y * speedMultilier * deviceMultiplier;
        }
    }
}