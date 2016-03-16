using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.AutoMapper
{
    /// <summary>
    /// All classes that inherit this interface will automatically create a mapping.
    /// </summary>
    public interface IAutoMaping
    {
        void CreateMapping();
    }
}
