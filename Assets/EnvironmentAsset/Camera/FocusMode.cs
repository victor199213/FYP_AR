using UnityEngine;
using System.Collections;
using Vuforia;

public class FocusMode : MonoBehaviour
{
    void Start()
    {
        //initializing vuforia camera API

        var vuforia = VuforiaARController.Instance;
        vuforia.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        vuforia.RegisterOnPauseCallback(OnPaused);
        Application.targetFrameRate = 30;
    }


    private void OnVuforiaStarted()
    {
        // Set autofocus
        CameraDevice.Instance.SetFocusMode(
            CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }


    private void OnPaused(bool paused)
    {
        if (!paused) 
        {
            // Set autofocus
            CameraDevice.Instance.SetFocusMode(
            CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
    }
}