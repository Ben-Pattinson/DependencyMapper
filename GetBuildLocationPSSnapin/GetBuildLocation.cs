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
using System.Threading.Tasks;
using System.Management.Automation;
using System.Reflection;
using Mono.Cecil;
using System.IO;

namespace GetBuildLocationPSSnapIn
{
    [Cmdlet(VerbsCommon.Get,"BuildLocation")]
    public class GetBuildLocation : Cmdlet
    {
        [Parameter(Position=0,Mandatory=true, ValueFromPipeline=true, ValueFromPipelineByPropertyName=true,HelpMessage="An array of paths to the binaries to obtain the build location for.")]
        [ValidateNotNullOrEmpty]        
        public string[] FullName 
        {
            get { return _PathsOfBinariesToLoad; }
            set {_PathsOfBinariesToLoad = value;} 
        }
        [Parameter(Mandatory=false)]        
        public SwitchParameter ShowErrors { get; set; }

        private string[] _PathsOfBinariesToLoad;

        protected override void ProcessRecord()
        {
            foreach (string fullPath in _PathsOfBinariesToLoad)
            {
                AssemblyDefinition assemblyDefinition = null;
                string path ="";
                DotNetDLLInfo netDLLInfo = new DotNetDLLInfo();

                try
                {
                    assemblyDefinition = AssemblyDefinition.ReadAssembly(fullPath);
                }
                catch (Exception ex)
                {
                    if (ShowErrors)
                    {
                        WriteError(new ErrorRecord(ex, String.Format("Unable to load binary - this may not be a .net binary Path:{0}", fullPath), ErrorCategory.InvalidData, fullPath));
                    }
                }



                try
                {
                    path = GetPdbPathForAssembly(assemblyDefinition);

                    if (!string.IsNullOrEmpty(path))
                    {
                        netDLLInfo.BuildLocation=Path.GetDirectoryName(path);                        
                    }
                    
                }
                catch (Exception ex)
                {
                    if(ShowErrors)
                        WriteError(new ErrorRecord(ex, "Unable to extract build path", ErrorCategory.NotSpecified, fullPath));
                }

                                
                netDLLInfo.Name = Path.GetFileName(fullPath);
                netDLLInfo.NameSpace = assemblyDefinition.Name.Name;
                WriteObject(netDLLInfo);
            }
        }


        public static string GetPdbPathForAssembly(AssemblyDefinition assembly)
        {            
            byte[] debugHeaderBuffer = null;
            assembly.MainModule.GetDebugHeader(out debugHeaderBuffer);

            if (debugHeaderBuffer != null)
            {
                Int32 pdbPathOffset = 0x18;
                Byte[] byteArray = debugHeaderBuffer.Skip(pdbPathOffset).TakeWhile(currentbyte => currentbyte != 0).ToArray();

                if (byteArray.Length > 0)
                {                    
                    return Encoding.UTF8.GetString(byteArray);
                }
                return null;
            }
            return null;          
        }

    }


}
