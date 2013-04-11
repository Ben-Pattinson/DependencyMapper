/*===============================================================
#  The MIT License (MIT)
#  Copyright (c) 2013 Ben Pattinson
#
#  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
#
#  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
#
#  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#
#================================================================*/


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
