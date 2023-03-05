using BaseX;
using FrooxEngine;
using VRCFTModuleWrapper.VRCFT;

namespace VRCFTModuleWrapper
{
    internal class MouthDevice : IInputDriver
    {
        private Mouth mouth;
        public int UpdateOrder => 100;

        public void CollectDeviceInfos(DataTreeList list)
        {
            DataTreeDictionary dataTreeDictionary = new DataTreeDictionary();
            dataTreeDictionary.Add("Name", "VRCFT Eye Module");
            dataTreeDictionary.Add("Type", "Mouth Tracking");
            dataTreeDictionary.Add("Model", "VRCFT");
            list.Add(dataTreeDictionary);
        }

        public void RegisterInputs(InputInterface inputInterface)
        {
            mouth = new Mouth(inputInterface, "VRCFT Mouth Tracking");
        }

        public void UpdateInputs(float deltaTime)
        {
            if (UnifiedLibManager.LipStatus != ModuleState.Uninitialized)
            {
                mouth.IsDeviceActive = Engine.Current.InputInterface.VR_Active;
                mouth.IsTracking = UnifiedLibManager.LipStatus != ModuleState.Uninitialized;

                mouth.CheekLeftPuffSuck = 
                    UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.CheekPuffLeft] -
                    UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.CheekSuck];

                mouth.CheekRightPuffSuck =
                    UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.CheekPuffRight] -
                    UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.CheekSuck];

                mouth.Jaw = new float3(
                    UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.JawLeft] -
                    UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.JawRight],
                    UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.MouthApeShape],
                    UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.JawForward]); // TODO Review

                mouth.JawOpen = UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.JawOpen];

                //mouth.LipBottomOverturn
                //mouth.LipBottomOverUnder
                //mouth.LipLowerHorizontal
                //mouth.LipLowerLeftRaise
                //mouth.LipLowerRightRaise

                mouth.MouthLeftSmileFrown =
                    UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.MouthSmileLeft] -
                    UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.MouthSadLeft];

                mouth.MouthPout = UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.MouthPout];

                mouth.MouthRightSmileFrown =
                    UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.MouthSmileRight] -
                    UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.MouthSadRight];

                mouth.Tongue = new float3(
                    UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.TongueLeft] -
                    UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.MouthSadRight],
                    UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.TongueUp] -
                    UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.TongueDown],
                    MathX.Clamp01(
                        UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.TongueLongStep1] +
                        UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.TongueLongStep2]));

                mouth.TongueRoll = UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.TongueRoll];
            }
        }
    }
}
