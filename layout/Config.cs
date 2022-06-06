using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layout
{
    class Config : kLib4x.General.Config.AppConfigBase
    {
        public string Conf1
        {
            get
            {
                return GetAppSetting("conf1");
            }
            set
            {
                SetAppSetting("conf1", value);
            }
        }

    }
}
