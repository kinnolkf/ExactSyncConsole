using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public interface Iuploader
    {
        bool UploadFile(string fileName);

        Dictionary<string, DateTime> UpdateUploadedFile(string fileName, DateTime uploadedDate);
    }
}
