using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class VoxelDataHandler
{
    public VoxelData data;
    float GlobalSceneScale;

    public VoxelDataHandler(float scale)
    {
        this.data = new VoxelData();
        this.GlobalSceneScale = scale;
    }

    public void Update()
    {
        if (SteamVR_Actions._default.InteractUI.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            System.Random r = new System.Random();
            Vector3 GridCoords = RealCoordsToGridCoords(SteamVR_Actions._default.SkeletonRightHand.localPosition);
            
            Debug.Log($"x={SteamVR_Actions._default.SkeletonLeftHand.localPosition.x}, y={SteamVR_Actions._default.SkeletonLeftHand.localPosition.y}, z={SteamVR_Actions._default.SkeletonLeftHand.localPosition.z} c={Time.deltaTime}");
            Debug.Log($"x={SteamVR_Actions._default.SkeletonRightHand.localPosition.x}, y={SteamVR_Actions._default.SkeletonRightHand.localPosition.y}, z={SteamVR_Actions._default.SkeletonRightHand.localPosition.z} c={Time.deltaTime}");
            Debug.Log($"x={GridCoords.x}, y={GridCoords.y}, z={GridCoords.z} c={Time.deltaTime}");
            if (data.GetCell((int)GridCoords.x, (int)GridCoords.y, (int)GridCoords.z) == 0)
            {
                data.ChangeData((int)GridCoords.x, (int)GridCoords.y, (int)GridCoords.z, 1);
            }
            else
            {
                data.ChangeData((int)GridCoords.x, (int)GridCoords.y, (int)GridCoords.z, 0);
            }
        }
    }

    private Vector3 RealCoordsToGridCoords(Vector3 handPosition)
    {
        Vector3 GridPosition = new Vector3(handPosition.x / GlobalSceneScale, handPosition.y / GlobalSceneScale, handPosition.z / GlobalSceneScale);
        if (GridPosition.x < 0 || GridPosition.x >= data.Width || GridPosition.y < 0 || GridPosition.y >= data.Depth || GridPosition.z < 0 || GridPosition.z >= data.Height)
        {
            return new Vector3(0, 0, 0);
        }
        else
        {
            return GridPosition;
        }
    }
}
