using Coursework.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Core.Abstractions
{
    public interface IPechService
    {
        Task<Guid> CreatePech(Pech pech);
        Task<Guid> UpdatePech(Guid id, float tPech, float height, float tNach, float kTeplo, float Teplo, float p, float tPov, float kTeploOtd);
        Task<Guid> DeletePech(Guid id);
        Task<List<Pech>> GetAllPech();
    }
}
