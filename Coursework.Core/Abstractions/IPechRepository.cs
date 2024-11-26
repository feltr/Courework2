using Coursework.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Core.Abstractions
{
    public interface IPechRepository
    {
        Task<Guid> Create(Pech pech);
        Task<Guid> Delete(Guid id);
        Task<List<Pech>> Get();
        Task<Guid> Update(Guid id, float tPech, float height, float tNach, float kTeplo, float Teplo, float p, float tPov, float kTeploOtd);
    }
}
