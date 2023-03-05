using BaseX;
using FrooxEngine;
using VRCFTModuleWrapper.VRCFT;

namespace VRCFTModuleWrapper
{
    internal class EyeDevice : IInputDriver
    {
        private Eyes eyes;
        public int UpdateOrder => 100;

        public void CollectDeviceInfos(DataTreeList list)
        {
            DataTreeDictionary dataTreeDictionary = new DataTreeDictionary();
            dataTreeDictionary.Add("Name", "VRCFT Eye Module");
            dataTreeDictionary.Add("Type", "Eye Tracking");
            dataTreeDictionary.Add("Model", "VRCFT");
            list.Add(dataTreeDictionary);
        }

        public void RegisterInputs(InputInterface inputInterface)
        {
            eyes = new Eyes(inputInterface, "VRCFT Eye Tracking");
        }

        public void UpdateInputs(float deltaTime)
        {
            if (UnifiedLibManager.EyeStatus != ModuleState.Uninitialized)
            {
                eyes.IsEyeTrackingActive = Engine.Current.InputInterface.VR_Active;

                UpdateEye(UnifiedTrackingData.LatestEyeData.Left, eyes.LeftEye);
                UpdateEye(UnifiedTrackingData.LatestEyeData.Right, eyes.RightEye);
                UpdateEye(UnifiedTrackingData.LatestEyeData.Combined, eyes.CombinedEye);
                
                eyes.ComputeCombinedEyeParameters();
                eyes.FinishUpdate();
            }
        }
        
        private void UpdateEye(VRCFT.Eye data, FrooxEngine.Eye eye)
        {
            eye.IsDeviceActive = Engine.Current.InputInterface.VR_Active;
            eye.IsTracking = UnifiedLibManager.EyeStatus != ModuleState.Uninitialized;

            if (eye.IsTracking)
            {
                eye.UpdateWithDirection(Project2DTo3D(data.Look.x, data.Look.y));
                eye.RawPosition = float3.Zero;
                eye.PupilDiameter = UnifiedTrackingData.LatestEyeData.EyesPupilDiameter; // EyesDilation ?
            }

            eye.Openness = data.Openness;
            eye.Widen = data.Widen;
            eye.Squeeze = data.Squeeze;
            eye.Frown = 0f;
        }

        private float3 Project2DTo3D(float x, float y) => new float3(MathX.Tan(x), MathX.Tan(y), 1f).Normalized;
    }
}
