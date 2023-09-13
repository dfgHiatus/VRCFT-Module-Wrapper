using BaseX;
using FrooxEngine;
using VRCFaceTracking.Core.Params.Expressions;

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
            mouth.IsDeviceActive = Engine.Current.InputInterface.VR_Active;
            mouth.IsTracking = Engine.Current.InputInterface.VR_Active;

            mouth.CheekLeftPuffSuck =
                 Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.CheekPuffLeft) -
                 Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.CheekSuckLeft);

            mouth.CheekRightPuffSuck =
                 Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.CheekPuffRight) -
                 Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.CheekSuckRight);

            mouth.Jaw = new float3(
                 (Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.JawLeft) -
                 Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.JawRight)),
                 0f,
                 Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.JawForward));

            mouth.JawOpen =  Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.JawOpen);

            //mouth.LipBottomOverturn
            //mouth.LipBottomOverUnder
            //mouth.LipLowerHorizontal
            //mouth.LipLowerLeftRaise
            //mouth.LipLowerRightRaise

            mouth.MouthLeftSmileFrown =
                 Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.MouthCornerPullLeft) -
                 Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.MouthFrownLeft);

            // mouth.MouthPout =  Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.MouthPout);

            mouth.MouthRightSmileFrown =
                 Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.MouthCornerPullRight) -
                 Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.MouthFrownRight);

            mouth.Tongue = new float3(
                 (Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.TongueLeft) -
                 Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.TongueRight)),
                 (Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.TongueUp) -
                 Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.TongueDown)),
                 Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.TongueOut));

            mouth.TongueRoll =  Expressions.VRCFTExpressionMap.GetByKey1(UnifiedExpressions.TongueRoll);
        }
    }
}
