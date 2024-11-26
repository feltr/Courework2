using Coursework.Core.Abstractions;
using Coursework.Core.Models;
using Coursework.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.DataAccess.Repositories
{
    public class PechRepository : IPechRepository
    {
        private readonly PechDbContext _context;
        public PechRepository(PechDbContext context)
        {
            _context = context;
        }
        public async Task<List<Pech>> Get()
        {
            var pechEntities = await _context.Pech
                .AsNoTracking()
                .ToListAsync();
            var pech = pechEntities
                .Select(p => Pech.Create(p.Id, p.tPech, p.height, p.tNach, p.kTeplo, p.Teplo, p.p, p.tPov, p.kTeploOtd).pech)
                .ToList();

            return pech;
        }
        public async Task<Guid> Create(Pech pech)
        {
            var pechEntity = new PechEntity
            {
                Id = pech.Id,
                tPech = pech.TPech,
                height = pech.Height,
                tNach = pech.TNach,
                kTeplo = pech.KTeplo,
                Teplo = pech.Teplo,
                p = pech.P,
                tPov = pech.TPov,
                kTeploOtd = pech.KTeploOtd,
                VremNagr = Pech.CalculateVremNagr(pech.TPech, pech.Height, pech.TNach, pech.KTeplo, pech.Teplo, pech.P, pech.TPov, pech.KTeploOtd),
                T = Pech.CalculateTKon(pech.TPech, pech.Height, pech.TNach, pech.KTeplo, pech.Teplo, pech.P, pech.TPov, pech.KTeploOtd)
            };
            await _context.Pech.AddAsync(pechEntity);
            await _context.SaveChangesAsync();

            return pechEntity.Id;
        }

        public async Task<Guid> Update(Guid id, float tPech, float height, float tNach, float kTeplo, float Teplo, float p, float tPov, float kTeploOtd)
        {
            await _context.Pech
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.tPech, p => tPech)
                .SetProperty(p => p.height, p => height)
                .SetProperty(p => p.tNach, p => tNach)
                .SetProperty(p => p.kTeplo, p => kTeplo)
                .SetProperty(p => p.Teplo, p => Teplo)
                .SetProperty(_p => _p.p, _p => p)
                .SetProperty(p => p.tPov, p => tPov)
                .SetProperty(p => p.kTeploOtd, p => kTeploOtd)
                .SetProperty(_p => _p.VremNagr, _p => Pech.CalculateVremNagr(tPech, height, tNach, kTeplo, Teplo, p, tPov, kTeploOtd))
                .SetProperty(_p => _p.T, _p => Pech.CalculateTKon(tPech, height, tNach, kTeplo, Teplo, p, tPov, kTeploOtd))
                );
            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Pech
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();
            return id;
        }
    }
}
