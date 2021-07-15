using System;
using System.ComponentModel;

namespace EGGY_TCC_IDENTITY.ViewModels
{
    public class OngApoiadorViewModel 
    {
        public int ID_ONG_APOIADOR { get; set; }
        public int ID_APOIADOR { get; set; }        
        public int ID_ONG { get; set; }
        public ApoidorViewModel Apoidor { get; set; }
        public OngViewModel Ong { get; set; }
    }
}