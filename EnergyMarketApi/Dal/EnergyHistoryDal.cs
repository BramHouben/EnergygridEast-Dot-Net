using EnergyMarketApi.Dal.Interface;
using EnergyMarketApi.Model.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyMarketApi.Dal
{
    public class EnergyHistoryDal : IEnergyHistoryDal
    {
        private readonly DataContext _context;

        public EnergyHistoryDal(DataContext context)
        {
            _context = context;
        }

        public async Task Add(EnergyHistoryDto energyHistory)
        {
            await _context.Energy.AddAsync(energyHistory);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(List<Guid> uuidCollection)
        {
            if (!uuidCollection.Any())
            {
                throw new NoNullAllowedException();
            }

            List<EnergyHistoryDto> historyToRemove = await _context.Energy
                .Where(e => uuidCollection
                    .Contains(e.Uuid))
                .ToListAsync();

            _context.Energy.RemoveRange(historyToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task<List<EnergyHistoryDto>> All(int total)
        {
            return await _context.Energy
                .Take(total)
                .ToListAsync();
        }
    }
}