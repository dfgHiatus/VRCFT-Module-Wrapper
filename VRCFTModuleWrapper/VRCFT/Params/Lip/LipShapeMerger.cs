﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace VRCFTModuleWrapper.VRCFT
{
    public static class LipShapeMerger
    {
        private static readonly Dictionary<string, ICombinedShape> MergedShapes =
            new Dictionary<string, ICombinedShape>
            {
                {"JawX", new PositiveNegativeShape(LipShape_v2.JawRight, LipShape_v2.JawLeft)},
                {"MouthUpper", new PositiveNegativeShape(LipShape_v2.MouthUpperRight, LipShape_v2.MouthUpperLeft)},
                {"MouthLower", new PositiveNegativeShape(LipShape_v2.MouthLowerRight, LipShape_v2.MouthLowerLeft)},
                {"MouthX", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthUpperRight, LipShape_v2.MouthLowerRight}, new LipShape_v2[]{LipShape_v2.MouthUpperLeft, LipShape_v2.MouthLowerLeft}, true)},
                {"SmileSadRight", new PositiveNegativeShape(LipShape_v2.MouthSmileRight, LipShape_v2.MouthSadRight)},
                {"SmileSadLeft", new PositiveNegativeShape(LipShape_v2.MouthSmileLeft, LipShape_v2.MouthSadLeft)},
                {"SmileSad", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthSmileLeft, LipShape_v2.MouthSmileRight}, new LipShape_v2[]{LipShape_v2.MouthSadLeft, LipShape_v2.MouthSadRight})},
                {"TongueY", new PositiveNegativeShape(LipShape_v2.TongueUp, LipShape_v2.TongueDown)},
                {"TongueX", new PositiveNegativeShape(LipShape_v2.TongueRight, LipShape_v2.TongueLeft)},
                {"PuffSuckRight", new PositiveNegativeShape(LipShape_v2.CheekPuffRight, LipShape_v2.CheekSuck)},
                {"PuffSuckLeft", new PositiveNegativeShape(LipShape_v2.CheekPuffLeft, LipShape_v2.CheekSuck)},
                {"PuffSuck", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.CheekPuffLeft, LipShape_v2.CheekPuffRight}, new LipShape_v2[]{LipShape_v2.CheekSuck}, true)},

                //Additional combined shapes created with the help of the VRCFT Discord!

                //JawOpen based params
                {"JawOpenApe", new PositiveNegativeShape(LipShape_v2.JawOpen, LipShape_v2.MouthApeShape)},
                {"JawOpenPuff", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.JawOpen}, new LipShape_v2[]{LipShape_v2.CheekPuffLeft, LipShape_v2.CheekPuffRight})},
                {"JawOpenPuffRight", new PositiveNegativeShape(LipShape_v2.JawOpen, LipShape_v2.CheekPuffRight)},
                {"JawOpenPuffLeft", new PositiveNegativeShape(LipShape_v2.JawOpen, LipShape_v2.CheekPuffLeft)},
                {"JawOpenSuck", new PositiveNegativeShape(LipShape_v2.JawOpen, LipShape_v2.CheekSuck)},
                {"JawOpenForward", new PositiveNegativeShape(LipShape_v2.JawOpen, LipShape_v2.JawForward)},
                {"JawOpenOverlay", new PositiveNegativeShape(LipShape_v2.JawOpen, LipShape_v2.MouthLowerOverlay)},

                //MouthUpperUpRight based params
                {"MouthUpperUpRightUpperInside", new PositiveNegativeShape(LipShape_v2.MouthUpperUpRight, LipShape_v2.MouthUpperInside)},
                {"MouthUpperUpRightPuffRight", new PositiveNegativeShape(LipShape_v2.MouthUpperUpRight, LipShape_v2.CheekPuffRight)},
                {"MouthUpperUpRightApe", new PositiveNegativeShape(LipShape_v2.MouthUpperUpRight, LipShape_v2.MouthApeShape)},
                {"MouthUpperUpRightPout", new PositiveNegativeShape(LipShape_v2.MouthUpperUpRight, LipShape_v2.MouthPout)},
                {"MouthUpperUpRightOverlay", new PositiveNegativeShape(LipShape_v2.MouthUpperUpRight, LipShape_v2.MouthLowerOverlay)},
                {"MouthUpperUpRightSuck", new PositiveNegativeShape(LipShape_v2.MouthUpperUpRight, LipShape_v2.CheekSuck)},
                
                //MouthUpperUpLeft based params
                {"MouthUpperUpLeftUpperInside", new PositiveNegativeShape(LipShape_v2.MouthUpperUpLeft, LipShape_v2.MouthUpperInside)},
                {"MouthUpperUpLeftPuffLeft", new PositiveNegativeShape(LipShape_v2.MouthUpperUpLeft, LipShape_v2.CheekPuffLeft)},
                {"MouthUpperUpLeftApe", new PositiveNegativeShape(LipShape_v2.MouthUpperUpLeft, LipShape_v2.MouthApeShape)},
                {"MouthUpperUpLeftPout", new PositiveNegativeShape(LipShape_v2.MouthUpperUpLeft, LipShape_v2.MouthPout)},
                {"MouthUpperUpLeftOverlay", new PositiveNegativeShape(LipShape_v2.MouthUpperUpLeft, LipShape_v2.MouthLowerOverlay)},
                {"MouthUpperUpLeftSuck", new PositiveNegativeShape(LipShape_v2.MouthUpperUpLeft, LipShape_v2.CheekSuck)},

                // MouthUpperUp Left+Right base params
                {"MouthUpperUpUpperInside", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthUpperUpLeft, LipShape_v2.MouthUpperUpRight}, new LipShape_v2[]{LipShape_v2.MouthUpperInside })},
                {"MouthUpperUpInside", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthUpperUpLeft, LipShape_v2.MouthUpperUpRight}, new LipShape_v2[]{LipShape_v2.MouthUpperInside, LipShape_v2.MouthLowerInside}, true)},
                {"MouthUpperUpPuff", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthUpperUpLeft, LipShape_v2.MouthUpperUpRight}, new LipShape_v2[]{LipShape_v2.CheekPuffLeft, LipShape_v2.CheekPuffRight})},
                {"MouthUpperUpPuffLeft", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthUpperUpLeft, LipShape_v2.MouthUpperUpRight}, new LipShape_v2[]{LipShape_v2.CheekPuffLeft})},
                {"MouthUpperUpPuffRight", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthUpperUpLeft, LipShape_v2.MouthUpperUpRight}, new LipShape_v2[]{LipShape_v2.CheekPuffRight})},
                {"MouthUpperUpApe", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthUpperUpLeft, LipShape_v2.MouthUpperUpRight}, new LipShape_v2[]{LipShape_v2.MouthApeShape})},
                {"MouthUpperUpPout", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthUpperUpLeft, LipShape_v2.MouthUpperUpRight}, new LipShape_v2[]{LipShape_v2.MouthPout})},
                {"MouthUpperUpOverlay", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthUpperUpLeft, LipShape_v2.MouthUpperUpRight}, new LipShape_v2[]{LipShape_v2.MouthLowerOverlay})},
                {"MouthUpperUpSuck", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthUpperUpLeft, LipShape_v2.MouthUpperUpRight}, new LipShape_v2[]{LipShape_v2.CheekSuck})},

                //MouthLowerDownRight based params
                {"MouthLowerDownRightLowerInside", new PositiveNegativeShape(LipShape_v2.MouthLowerDownRight, LipShape_v2.MouthLowerInside)},
                {"MouthLowerDownRightPuffRight", new PositiveNegativeShape(LipShape_v2.MouthLowerDownRight, LipShape_v2.CheekPuffRight)},
                {"MouthLowerDownRightApe", new PositiveNegativeShape(LipShape_v2.MouthLowerDownRight, LipShape_v2.MouthApeShape)},
                {"MouthLowerDownRightPout", new PositiveNegativeShape(LipShape_v2.MouthLowerDownRight, LipShape_v2.MouthPout)},
                {"MouthLowerDownRightOverlay", new PositiveNegativeShape(LipShape_v2.MouthLowerDownRight, LipShape_v2.MouthLowerOverlay)},
                {"MouthLowerDownRightSuck", new PositiveNegativeShape(LipShape_v2.MouthLowerDownRight, LipShape_v2.CheekSuck)},

                //MouthLowerDownLeft based params
                {"MouthLowerDownLeftLowerInside", new PositiveNegativeShape(LipShape_v2.MouthLowerDownLeft, LipShape_v2.MouthLowerInside)},
                {"MouthLowerDownLeftPuffLeft", new PositiveNegativeShape(LipShape_v2.MouthLowerDownLeft, LipShape_v2.CheekPuffLeft)},
                {"MouthLowerDownLeftApe", new PositiveNegativeShape(LipShape_v2.MouthLowerDownLeft, LipShape_v2.MouthApeShape)},
                {"MouthLowerDownLeftPout", new PositiveNegativeShape(LipShape_v2.MouthLowerDownLeft, LipShape_v2.MouthPout)},
                {"MouthLowerDownLeftOverlay", new PositiveNegativeShape(LipShape_v2.MouthLowerDownLeft, LipShape_v2.MouthLowerOverlay)},
                {"MouthLowerDownLeftSuck", new PositiveNegativeShape(LipShape_v2.MouthLowerDownLeft, LipShape_v2.CheekSuck)},

                // MouthLowerDown Left+Right base params
                {"MouthLowerDownLowerInside", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthLowerDownLeft, LipShape_v2.MouthLowerDownRight}, new LipShape_v2[]{LipShape_v2.MouthLowerInside})},
                {"MouthLowerDownInside", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthLowerDownLeft, LipShape_v2.MouthLowerDownRight}, new LipShape_v2[]{LipShape_v2.MouthUpperInside, LipShape_v2.MouthLowerInside}, true)},
                {"MouthLowerDownPuff", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthLowerDownLeft, LipShape_v2.MouthLowerDownRight}, new LipShape_v2[]{LipShape_v2.CheekPuffLeft, LipShape_v2.CheekPuffRight})},
                {"MouthLowerDownPuffLeft", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthLowerDownLeft, LipShape_v2.MouthLowerDownRight}, new LipShape_v2[]{LipShape_v2.CheekPuffLeft})},
                {"MouthLowerDownPuffRight", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthLowerDownLeft, LipShape_v2.MouthLowerDownRight}, new LipShape_v2[]{LipShape_v2.CheekPuffRight})},
                {"MouthLowerDownApe", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthLowerDownLeft, LipShape_v2.MouthLowerDownRight}, new LipShape_v2[]{LipShape_v2.MouthApeShape})},
                {"MouthLowerDownPout", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthLowerDownLeft, LipShape_v2.MouthLowerDownRight}, new LipShape_v2[]{LipShape_v2.MouthPout})},
                {"MouthLowerDownOverlay", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthLowerDownLeft, LipShape_v2.MouthLowerDownRight}, new LipShape_v2[]{LipShape_v2.MouthLowerOverlay})},
                {"MouthLowerDownSuck", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthLowerDownLeft, LipShape_v2.MouthLowerDownRight}, new LipShape_v2[]{LipShape_v2.CheekSuck})},

				// MouthInsideOverturn based params
				{"MouthUpperInsideOverturn", new PositiveNegativeShape(LipShape_v2.MouthUpperInside, LipShape_v2.MouthUpperOverturn)},
                {"MouthLowerInsideOverturn", new PositiveNegativeShape(LipShape_v2.MouthLowerInside, LipShape_v2.MouthLowerOverturn)},
				
                //SmileRight based params; Recommend using these if you already have SmileSadLeft setup!
                {"SmileRightUpperOverturn", new PositiveNegativeShape(LipShape_v2.MouthSmileRight, LipShape_v2.MouthUpperOverturn)},
                {"SmileRightLowerOverturn", new PositiveNegativeShape(LipShape_v2.MouthSmileRight, LipShape_v2.MouthLowerOverturn)},
                {"SmileRightOverturn", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthSmileRight}, new LipShape_v2[]{LipShape_v2.MouthUpperOverturn, LipShape_v2.MouthLowerOverturn})},
                {"SmileRightApe", new PositiveNegativeShape(LipShape_v2.MouthSmileRight, LipShape_v2.MouthApeShape)},
                {"SmileRightOverlay", new PositiveNegativeShape(LipShape_v2.MouthSmileRight, LipShape_v2.MouthLowerOverlay)},
                {"SmileRightPout", new PositiveNegativeShape(LipShape_v2.MouthSmileRight, LipShape_v2.MouthPout)},

                //SmileLeft based params; Recommend using these if you already have SmileSadRight setup!
                {"SmileLeftUpperOverturn", new PositiveNegativeShape(LipShape_v2.MouthSmileLeft, LipShape_v2.MouthUpperOverturn)},
                {"SmileLeftLowerOverturn", new PositiveNegativeShape(LipShape_v2.MouthSmileLeft, LipShape_v2.MouthLowerOverturn)},
                {"SmileLeftOverturn", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthSmileLeft}, new LipShape_v2[]{LipShape_v2.MouthUpperOverturn, LipShape_v2.MouthLowerOverturn})},
                {"SmileLeftApe", new PositiveNegativeShape(LipShape_v2.MouthSmileLeft, LipShape_v2.MouthApeShape)},
                {"SmileLeftOverlay", new PositiveNegativeShape(LipShape_v2.MouthSmileLeft, LipShape_v2.MouthLowerOverlay)},
                {"SmileLeftPout", new PositiveNegativeShape(LipShape_v2.MouthSmileLeft, LipShape_v2.MouthPout)},

                //Smile Left+Right based params
                {"SmileUpperOverturn", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthSmileLeft, LipShape_v2.MouthSmileRight}, new LipShape_v2[]{LipShape_v2.MouthUpperOverturn})},
                {"SmileLowerOverturn", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthSmileLeft, LipShape_v2.MouthSmileRight}, new LipShape_v2[]{LipShape_v2.MouthLowerOverturn})},
                {"SmileOverturn", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthSmileLeft, LipShape_v2.MouthSmileRight}, new LipShape_v2[]{LipShape_v2.MouthUpperOverturn, LipShape_v2.MouthLowerOverturn})},
                {"SmileApe", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthSmileLeft, LipShape_v2.MouthSmileRight}, new LipShape_v2[]{LipShape_v2.MouthApeShape})},
                {"SmileOverlay", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthSmileLeft, LipShape_v2.MouthSmileRight}, new LipShape_v2[]{LipShape_v2.MouthLowerOverlay})},
                {"SmilePout", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.MouthSmileLeft, LipShape_v2.MouthSmileRight}, new LipShape_v2[]{LipShape_v2.MouthPout})},

                //CheekPuffRight based params
                {"PuffRightUpperOverturn", new PositiveNegativeShape(LipShape_v2.CheekPuffRight, LipShape_v2.MouthUpperOverturn)},
                {"PuffRightLowerOverturn", new PositiveNegativeShape(LipShape_v2.CheekPuffRight, LipShape_v2.MouthLowerOverturn)},
                {"PuffRightOverturn", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.CheekPuffRight}, new LipShape_v2[]{LipShape_v2.MouthUpperOverturn, LipShape_v2.MouthLowerOverturn}, true)},

                //CheekPuffLeft based params
                {"PuffLeftUpperOverturn", new PositiveNegativeShape(LipShape_v2.CheekPuffLeft, LipShape_v2.MouthUpperOverturn)},
                {"PuffLeftLowerOverturn", new PositiveNegativeShape(LipShape_v2.CheekPuffLeft, LipShape_v2.MouthLowerOverturn)},
                {"PuffLeftOverturn", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.CheekPuffLeft}, new LipShape_v2[]{LipShape_v2.MouthUpperOverturn, LipShape_v2.MouthLowerOverturn}, true)},

                //CheekPuff Left+Right based params
                {"PuffUpperOverturn", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.CheekPuffRight, LipShape_v2.CheekPuffLeft}, new LipShape_v2[]{LipShape_v2.MouthUpperOverturn})},
                {"PuffLowerOverturn", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.CheekPuffRight, LipShape_v2.CheekPuffLeft}, new LipShape_v2[]{LipShape_v2.MouthLowerOverturn})},
                {"PuffOverturn", new PositiveNegativeAveragedShape(new LipShape_v2[]{LipShape_v2.CheekPuffRight, LipShape_v2.CheekPuffLeft}, new LipShape_v2[]{LipShape_v2.MouthUpperOverturn, LipShape_v2.MouthLowerOverturn}, true)},

                //Combine both TongueSteps (-1 fully in, 0 on edge, 1 fully out)
                {"TongueSteps", new PositiveNegativeShape(LipShape_v2.TongueLongStep1, LipShape_v2.TongueLongStep2, true)},
            };

        // Make a list called LipParameters containing the results from both GetOptimizedLipParameters and GetAllLipParameters, and add GetLipActivatedStatus
        public static readonly IParameter[] AllLipParameters =
            GetAllLipShapes().Union(GetOptimizedLipParameters()).Union(GetLipActivatedStatus()).ToArray();

        public static bool IsLipShapeName(string name) => MergedShapes.ContainsKey(name) || Enum.TryParse(name, out LipShape_v2 shape);

        private static IEnumerable<EParam> GetOptimizedLipParameters() => MergedShapes
            .Select(shape => new EParam((eye, lip) =>
                shape.Value.GetBlendedLipShape(lip.LatestShapes), shape.Key, 0.0f));

        private static IEnumerable<EParam> GetAllLipShapes() =>
            ((LipShape_v2[])Enum.GetValues(typeof(LipShape_v2))).ToList().Select(shape =>
                new EParam((eye, lip) => lip.LatestShapes[(int)shape],
                    shape.ToString(), 0.0f));

        private static IEnumerable<IParameter> GetLipActivatedStatus() => new List<IParameter>
        {
            new BoolParameter(v2 => UnifiedLibManager.LipStatus.Equals(ModuleState.Active), "LipTrackingActive"),
        };
    }
}
