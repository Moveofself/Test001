using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESEC2008
{
    public interface IbarCodeReader
    {
        string ReadStripAndGetStripId();
        void Close();

        void Start();

        void Initial(string Config);
    }

}
