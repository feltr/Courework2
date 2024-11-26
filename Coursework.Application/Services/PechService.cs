using Coursework.Core.Abstractions;
using Coursework.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Application.Services
{
    public class PechService : IPechService
    {
        private readonly IPechRepository _pechRepository;
        public PechService(IPechRepository pechRepository)
        {
            _pechRepository = pechRepository;
        }

        public async Task<List<Pech>> GetAllPech()
        {
            return await _pechRepository.Get();
        }

        public async Task<Guid> CreatePech(Pech pech)
        {
            return await _pechRepository.Create(pech);
        }

        public async Task<Guid> UpdatePech(Guid id, float tPech, float height, float tNach, float kTeplo, float Teplo, float p, float tPov, float kTeploOtd)
        {
            return await _pechRepository.Update(id, tPech, height, tNach, kTeplo, Teplo, p, tPov, kTeploOtd);
        }

        public async Task<Guid> DeletePech(Guid id)
        {
            return await _pechRepository.Delete(id);
        }
    }
}
