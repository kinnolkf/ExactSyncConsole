using CommonLib;
using ExactAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTransferFactory
{
    public class UploaderFactory
    {
        public UploaderFactory(string uploaderName)
        {
            uploaderSwitch = uploaderName;
        }
        public Iuploader GetUploader()
        {
            switch (uploaderSwitch)
            {
                case "ExactOnline":
                    return new ExactUploader();
                    break;
                default:
                    return null;
            }
        }

        string uploaderSwitch;
    }

    
}
