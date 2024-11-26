using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.DataAccess.Entites
{
    public class PechEntity
    {
        public Guid Id { get; set; }
        public float tPech { get; set; }
        public float height { get; set; }
        public float tNach { get; set; }
        public float kTeplo { get; set; }
        public float Teplo { get; set; }
        public float p { get; set; }
        public float tPov { get; set; }
        public float kTeploOtd { get; set; }
        public float VremNagr { get; set; }
        public float T { get; set; }
    }
}
