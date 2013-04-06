using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using System.ComponentModel;

namespace GetBuildLocationPSSnapIn
{
    [RunInstaller(true)]
    public class Installer : PSSnapIn
    {
        public override string Name
        {
            get { return "BenPattinson.GetBuildLocation.PSSnapIn"; }
        }

        public override string Vendor
        {
            get { return "Ben.Pattinson"; }
        }

        public override string Description
        {
            get { return "This uses Mono Cecil to extract the original build location stored in a .net binary as the pdb location. This is usefull if you have some dependecies for a project and have no idea which project produced them."; }
        }
    }
}
