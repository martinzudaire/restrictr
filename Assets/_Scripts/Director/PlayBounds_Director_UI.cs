using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using Valve.VR;

public partial class PlayBounds_Director
{
    public static PlayBounds_Director instance;

    private void Awake() {
        instance = this;
    }

    // Methods for Easy UI.
    public void SetHideMainWindow(bool hideWin) 
	{
		if(hideWin)
		{
			menuOverlay.dontForceRenderTexCam = false;
			menuRig.menuRigCamera.enabled = false;

			winC.HideUnityWindow();
			winC.ShowTrayIcon();
		}
		else
		{
			menuOverlay.dontForceRenderTexCam = true;
			menuRig.menuRigCamera.enabled = true;

			winC.ShowUnityWindow();
			winC.HideTrayIcon();
		}
	}

	public void OnShowWindow()
	{
		menuRig.hideMainWindowToggle.isOn = false;
	}

	public void LinkOpacityWithTwist(bool linked)
	{
		twistTied = linked;
	}

	public void SetFloorOverlayDevice(int dev)
	{
		switch(dev)
		{
			case 1:
				floorOverlayDevice = Unity_Overlay.OverlayTrackedDevice.RightHand;
			break;

			case 2:
				floorOverlayDevice = Unity_Overlay.OverlayTrackedDevice.LeftHand;
			break;

			default:
				floorOverlayDevice = Unity_Overlay.OverlayTrackedDevice.None;
			break;
		}

		if(floorOverlay.deviceToTrack != floorOverlayDevice)
			floorOverlay.deviceToTrack = floorOverlayDevice;

		//SetFloorOverlayScale(prefs.Scale);
		//SetFloorOverlayHeight(prefs.Height);
	}

	public void SetFloorOverlayControllerSide(bool flip)
	{
		flipSides = flip;
		// SetFloorOverlayHeight(prefs.Height);
	}

	/*public void SetFloorOverlayScale(float scale) 
	{
		if(floorOverlayDevice != Unity_Overlay.OverlayTrackedDevice.None)
			if(floorOverlay.widthInMeters != prefs.Scale * floorOverlayHandScale)
				scale *= floorOverlayHandScale;

		if(floorOverlay.widthInMeters != scale)
			floorOverlay.widthInMeters = scale;
	}

	public void SetFloorOverlayHeight(float height) 
	{
		var foT = floorOverlay.transform;

		if(floorOverlayDevice != Unity_Overlay.OverlayTrackedDevice.None)
			height *= floorOverlayHandScale;

		if(floorOverlayDevice != Unity_Overlay.OverlayTrackedDevice.None && flipSides)
			height *= -1f;

		var heightV = new Vector3(foT.position.x, height, foT.position.z);
		foT.position = heightV;

		floorOverlayHeight = height;
	}

	public void SetFloorOverlayFollowSpeed(float speed)
	{
		floorOverlayFollowSpeedRatio = speed;
	}*/

	/*public void SetManifestAutoLaunch(bool autoLaunch)
	{
		if(handler != null && handler.Applications != null)
			handler.Applications.SetApplicationAutoLaunch(appKey, autoLaunch);
	}

	public bool GetManifestAutoLaunch()
	{
		return (handler != null && handler.Applications != null) ? handler.Applications.GetApplicationAutoLaunch(appKey) : false; 
	}*/
	
	public void SetManifestAutoLaunch(bool autoLaunch)
	{
		if(handler != null && handler.Applications != null)
			handler.Applications.SetApplicationAutoLaunch(appKey, autoLaunch);
	}

    public void SetApplicationLaunchOnStart(bool bLaunchOnStart) {
        if (handler != null && handler.Applications != null)
        {
            Debug.Log("Is Application installed: "+handler.Applications.IsApplicationInstalled(appKey));
            //if (handler.Applications.IsApplicationInstalled(appKey)) {
                RegisterApplicationManifest();
            //}
            //if (handler.Applications.IsApplicationInstalled(appKey)) {
                handler.Applications.SetApplicationAutoLaunch(appKey, bLaunchOnStart);
            //}
        }
    }

    public void RegisterApplicationManifest() {
        if (handler != null && handler.Applications != null) {
            //if (!handler.Applications.IsApplicationInstalled(appKey)) {
                handler.Applications.AddApplicationManifest(Application.dataPath + "/../manifest.vrmanifest", false);
            //}
        }
    }
    public void UnregisterApplicationManifest() {
        if (handler != null && handler.Applications != null) {
            //if (handler.Applications.IsApplicationInstalled(appKey)) {
                handler.Applications.RemoveApplicationManifest(Application.dataPath + "/../manifest.vrmanifest");
            //}
        }
    }
    public bool IsApplicationManifestRegistered() {
        if (handler != null && handler.Applications != null)
            return handler.Applications.IsApplicationInstalled(appKey);
        else
            return false;
    }
}