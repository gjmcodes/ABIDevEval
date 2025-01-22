using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Sale> CreateAsync(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return sale;
        }


        public async Task<Sale> UpdateAsync(Sale sale)
        {
            _context.Sales.Attach(sale);
            await _context.SaveChangesAsync();

            return sale;
        }
        public async Task<Sale> UpdateAsync(Guid id, Sale sale)
        {
            var saleDb = await _context.Sales
                .Include(x => x.Items)
                .Include(x => x.SaleBranch)
                .Include(x => x.SaleCustomer)
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.SalesItems.RemoveRange(saleDb.Items);

            saleDb.UpdateSale(sale);
            await _context.SaveChangesAsync();

            return saleDb;
        }

        public async Task<Sale> GetById(Guid id)
        {
            var sale = await _context.Sales
                .Include(x => x.SaleBranch)
                .Include(x => x.Items)
                .Include(x => x.SaleCustomer)
                .FirstOrDefaultAsync(x => x.Id == id);

            return sale;
        }


    }
}
