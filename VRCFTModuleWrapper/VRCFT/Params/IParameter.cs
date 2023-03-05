using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRCFTModuleWrapper.VRCFT
{
    public interface IParameter
    {
        void ResetParam(ConfigParser.Parameter[] newParams);

        OSCParams.BaseParam[] GetBase();
    }
}
