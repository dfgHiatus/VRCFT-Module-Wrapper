using BaseX;
using FrooxEngine;
using VRCFaceTracking.Core.Params.Expressions;

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
            eyes.IsEyeTrackingActive = Engine.Current.InputInterface.VR_Active;
            eyes.IsDeviceActive = Engine.Current.InputInterface.VR_Active;

            UpdateEye(
                eyes.LeftEye,
                Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.LeftEyeX),
                Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.LeftEyeY),
                Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.LeftEyeLid),
                Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.EyeWideLeft),
                Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.EyeSquintLeft),
                Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.EyesDilation)
                );

            UpdateEye(
                eyes.RightEye,
                Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.RightEyeX),
                Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.RightEyeY),
                Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.RightEyeLid),
                Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.EyeWideLeft),
                Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.EyeSquintLeft),
                Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.EyesDilation)
                );

            UpdateEye(
                eyes.CombinedEye,
                Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.EyesX),
                Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.EyesY),
                Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.CombinedEyeLid),
                Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.EyeWideLeft),
                Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.EyeSquintLeft),
                Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.EyesDilation)
                );

            eyes.ComputeCombinedEyeParameters();
            eyes.FinishUpdate();
        }
        
        private void UpdateEye(
            Eye eye,
            float lookX,
            float lookY,
            float openness,
            float widen,
            float squeeze,
            float dilation
            )
        {
            eye.IsDeviceActive = Engine.Current.InputInterface.VR_Active;
            eye.IsTracking = Engine.Current.InputInterface.VR_Active;

            if (eye.IsTracking)
            {
                eye.UpdateWithDirection(Project2DTo3D(lookX, lookY));
                eye.RawPosition = float3.Zero;
                eye.PupilDiameter = dilation;

                eye.Openness = openness;
                eye.Widen = widen;
                eye.Squeeze = squeeze;
                eye.Frown = 0f;
            }
        }

        private float3 Project2DTo3D(float x, float y) => new float3(MathX.Tan(x), MathX.Tan(y), 1f).Normalized;
    }
}
