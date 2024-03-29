﻿using VRCFaceTracking.Core.Params.Expressions;

namespace VRCFTModuleWrapper
{
    public class Expressions
    {
        public static readonly TwoKeyDictionary<UnifiedExpressions, string, float> VRCFTExpressionMap =
            new TwoKeyDictionary<UnifiedExpressions, string, float>()
        {
            { UnifiedExpressions.EyeSquintRight, "/avatar/parameters/EyeSquintRight", 0f},
            { UnifiedExpressions.EyeSquintLeft, "/avatar/parameters/EyeSquintLeft", 0f},
            { UnifiedExpressions.EyeWideRight, "/avatar/parameters/EyeWideRight", 0f},
            { UnifiedExpressions.EyeWideLeft, "/avatar/parameters/EyeWideLeft", 0f},
            { UnifiedExpressions.BrowPinchRight, "/avatar/parameters/BrowPinchRight", 0f},
            { UnifiedExpressions.BrowPinchLeft, "/avatar/parameters/BrowPinchLeft", 0f},
            { UnifiedExpressions.BrowLowererRight, "/avatar/parameters/BrowLowererRight", 0f},
            { UnifiedExpressions.BrowLowererLeft, "/avatar/parameters/BrowLowererLeft", 0f},
            { UnifiedExpressions.BrowInnerUpRight, "/avatar/parameters/BrowInnerUpRight", 0f},
            { UnifiedExpressions.BrowInnerUpLeft, "/avatar/parameters/BrowInnerUpLeft", 0f},
            { UnifiedExpressions.BrowOuterUpRight, "/avatar/parameters/BrowOuterUpRight", 0f},
            { UnifiedExpressions.BrowOuterUpLeft, "/avatar/parameters/BrowOuterUpLeft", 0f},
            { UnifiedExpressions.NasalDilationRight, "/avatar/parameters/NasalDilationRight", 0f},
            { UnifiedExpressions.NasalDilationLeft, "/avatar/parameters/NasalDilationLeft", 0f},
            { UnifiedExpressions.NasalConstrictRight, "/avatar/parameters/NasalConstrictRight", 0f},
            { UnifiedExpressions.NasalConstrictLeft, "/avatar/parameters/NasalConstrictLeft", 0f},
            { UnifiedExpressions.CheekSquintRight, "/avatar/parameters/CheekSquintRight", 0f},
            { UnifiedExpressions.CheekSquintLeft, "/avatar/parameters/CheekSquintLeft", 0f},
            { UnifiedExpressions.CheekPuffRight, "/avatar/parameters/CheekPuffRight", 0f},
            { UnifiedExpressions.CheekPuffLeft, "/avatar/parameters/CheekPuffLeft", 0f},
            { UnifiedExpressions.CheekSuckRight, "/avatar/parameters/CheekSuckRight", 0f},
            { UnifiedExpressions.CheekSuckLeft, "/avatar/parameters/CheekSuckLeft", 0f},
            { UnifiedExpressions.JawOpen, "/avatar/parameters/JawOpen", 0f},
            { UnifiedExpressions.JawRight, "/avatar/parameters/JawRight", 0f},
            { UnifiedExpressions.JawLeft, "/avatar/parameters/JawLeft", 0f},
            { UnifiedExpressions.JawForward, "/avatar/parameters/JawForward", 0f},
            { UnifiedExpressions.JawBackward, "/avatar/parameters/JawBackward", 0f},
            { UnifiedExpressions.JawClench, "/avatar/parameters/JawClench", 0f},
            { UnifiedExpressions.JawMandibleRaise, "/avatar/parameters/JawMandibleRaise", 0f},
            { UnifiedExpressions.MouthClosed, "/avatar/parameters/MouthClosed", 0f},
            { UnifiedExpressions.LipSuckUpperRight, "/avatar/parameters/LipSuckUpperRight", 0f},
            { UnifiedExpressions.LipSuckUpperLeft, "/avatar/parameters/LipSuckUpperLeft", 0f},
            { UnifiedExpressions.LipSuckLowerRight, "/avatar/parameters/LipSuckLowerRight", 0f},
            { UnifiedExpressions.LipSuckLowerLeft, "/avatar/parameters/LipSuckLowerLeft", 0f},
            { UnifiedExpressions.LipSuckCornerRight, "/avatar/parameters/LipSuckCornerRight", 0f},
            { UnifiedExpressions.LipSuckCornerLeft, "/avatar/parameters/LipSuckCornerLeft", 0f},
            { UnifiedExpressions.LipFunnelUpperRight, "/avatar/parameters/LipFunnelUpperRight", 0f},
            { UnifiedExpressions.LipFunnelUpperLeft, "/avatar/parameters/LipFunnelUpperLeft", 0f},
            { UnifiedExpressions.LipFunnelLowerRight, "/avatar/parameters/LipFunnelLowerRight", 0f},
            { UnifiedExpressions.LipFunnelLowerLeft, "/avatar/parameters/LipFunnelLowerLeft", 0f},
            { UnifiedExpressions.LipPuckerUpperRight, "/avatar/parameters/LipPuckerUpperRight", 0f},
            { UnifiedExpressions.LipPuckerUpperLeft, "/avatar/parameters/LipPuckerUpperLeft", 0f},
            { UnifiedExpressions.LipPuckerLowerRight, "/avatar/parameters/LipPuckerLowerRight", 0f},
            { UnifiedExpressions.LipPuckerLowerLeft, "/avatar/parameters/LipPuckerLowerLeft", 0f},
            { UnifiedExpressions.MouthUpperUpRight, "/avatar/parameters/MouthUpperUpRight", 0f},
            { UnifiedExpressions.MouthUpperUpLeft, "/avatar/parameters/MouthUpperUpLeft", 0f},
            { UnifiedExpressions.MouthUpperDeepenRight, "/avatar/parameters/MouthUpperDeepenRight", 0f},
            { UnifiedExpressions.MouthUpperDeepenLeft, "/avatar/parameters/MouthUpperDeepenLeft", 0f},
            { UnifiedExpressions.NoseSneerRight, "/avatar/parameters/NoseSneerRight", 0f},
            { UnifiedExpressions.NoseSneerLeft, "/avatar/parameters/NoseSneerLeft", 0f},
            { UnifiedExpressions.MouthLowerDownRight, "/avatar/parameters/MouthLowerDownRight", 0f},
            { UnifiedExpressions.MouthLowerDownLeft, "/avatar/parameters/MouthLowerDownLeft", 0f},
            { UnifiedExpressions.MouthUpperRight, "/avatar/parameters/MouthUpperRight", 0f},
            { UnifiedExpressions.MouthUpperLeft, "/avatar/parameters/MouthUpperLeft", 0f},
            { UnifiedExpressions.MouthLowerRight, "/avatar/parameters/MouthLowerRight", 0f},
            { UnifiedExpressions.MouthLowerLeft, "/avatar/parameters/MouthLowerLeft", 0f},
            { UnifiedExpressions.MouthCornerPullRight, "/avatar/parameters/MouthCornerPullRight", 0f},
            { UnifiedExpressions.MouthCornerPullLeft, "/avatar/parameters/MouthCornerPullLeft", 0f},
            { UnifiedExpressions.MouthCornerSlantRight, "/avatar/parameters/MouthCornerSlantRight", 0f},
            { UnifiedExpressions.MouthCornerSlantLeft, "/avatar/parameters/MouthCornerSlantLeft", 0f},
            { UnifiedExpressions.MouthFrownRight, "/avatar/parameters/MouthFrownRight", 0f},
            { UnifiedExpressions.MouthFrownLeft, "/avatar/parameters/MouthFrownLeft", 0f},
            { UnifiedExpressions.MouthStretchRight, "/avatar/parameters/MouthStretchRight", 0f},
            { UnifiedExpressions.MouthStretchLeft, "/avatar/parameters/MouthStretchLeft", 0f},
            { UnifiedExpressions.MouthDimpleRight, "/avatar/parameters/MouthDimpleRight", 0f},
            { UnifiedExpressions.MouthDimpleLeft, "/avatar/parameters/MouthDimpleLeft", 0f},
            { UnifiedExpressions.MouthRaiserUpper, "/avatar/parameters/MouthRaiserUpper", 0f},
            { UnifiedExpressions.MouthRaiserLower, "/avatar/parameters/MouthRaiserLower", 0f},
            { UnifiedExpressions.MouthPressRight, "/avatar/parameters/MouthPressRight", 0f},
            { UnifiedExpressions.MouthPressLeft, "/avatar/parameters/MouthPressLeft", 0f},
            { UnifiedExpressions.MouthTightenerRight, "/avatar/parameters/MouthTightenerRight", 0f},
            { UnifiedExpressions.MouthTightenerLeft, "/avatar/parameters/MouthTightenerLeft", 0f},
            { UnifiedExpressions.TongueOut, "/avatar/parameters/TongueOut", 0f},
            { UnifiedExpressions.TongueUp, "/avatar/parameters/TongueUp", 0f},
            { UnifiedExpressions.TongueDown, "/avatar/parameters/TongueDown", 0f},
            { UnifiedExpressions.TongueRight, "/avatar/parameters/TongueRight", 0f},
            { UnifiedExpressions.TongueLeft, "/avatar/parameters/TongueLeft", 0f},
            { UnifiedExpressions.TongueRoll, "/avatar/parameters/TongueRoll", 0f},
            { UnifiedExpressions.TongueBendDown, "/avatar/parameters/TongueBendDown", 0f},
            { UnifiedExpressions.TongueCurlUp, "/avatar/parameters/TongueCurlUp", 0f},
            { UnifiedExpressions.TongueSquish, "/avatar/parameters/TongueSquish", 0f},
            { UnifiedExpressions.TongueFlat, "/avatar/parameters/TongueFlat", 0f},
            { UnifiedExpressions.TongueTwistRight, "/avatar/parameters/TongueTwistRight", 0f},
            { UnifiedExpressions.TongueTwistLeft, "/avatar/parameters/TongueTwistLeft", 0f},
            { UnifiedExpressions.SoftPalateClose, "/avatar/parameters/SoftPalateClose", 0f},
            { UnifiedExpressions.ThroatSwallow, "/avatar/parameters/ThroatSwallow", 0f},
            { UnifiedExpressions.NeckFlexRight, "/avatar/parameters/NeckFlexRight", 0f},
            { UnifiedExpressions.NeckFlexLeft, "/avatar/parameters/NeckFlexLeft", 0f},
            { UnifiedExpressions.EyesX, "/avatar/parameters/EyesX", 0f},
            { UnifiedExpressions.EyesY, "/avatar/parameters/EyesY", 0f},
            { UnifiedExpressions.LeftEyeX, "/avatar/parameters/LeftEyeX", 0f},
            { UnifiedExpressions.LeftEyeY, "/avatar/parameters/LeftEyeY", 0f},
            { UnifiedExpressions.RightEyeX, "/avatar/parameters/RightEyeX", 0f},
            { UnifiedExpressions.RightEyeY, "/avatar/parameters/RightEyeY", 0f},
            { UnifiedExpressions.EyeWiden, "/avatar/parameters/EyeWiden", 0f},
            { UnifiedExpressions.EyeSqueeze, "/avatar/parameters/EyeSqueeze", 0f},
            { UnifiedExpressions.LeftEyeSqueeze, "/avatar/parameters/LeftEyeSqueeze", 0f},
            { UnifiedExpressions.RightEyeSqueeze, "/avatar/parameters/RightEyeSqueeze", 0f},
            { UnifiedExpressions.EyesDilation, "/avatar/parameters/EyesDilation", 0f},
            { UnifiedExpressions.EyesPupilDiameter, "/avatar/parameters/EyesPupilDiameter", 0f},
            { UnifiedExpressions.LeftEyeLid, "/avatar/parameters/LeftEyeLid", 0f},
            { UnifiedExpressions.RightEyeLid, "/avatar/parameters/RightEyeLid", 0f},
            { UnifiedExpressions.CombinedEyeLid, "/avatar/parameters/CombinedEyeLid", 0f }
        };
    }
}