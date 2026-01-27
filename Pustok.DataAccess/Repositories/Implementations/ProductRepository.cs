using Pustok.Core.Entities;
using Pustok.DataAccess.Contexts;
using Pustok.DataAccess.Repositories.Abstractions;
using Pustok.DataAccess.Repositories.Implementations.Generic;

namespace Pustok.DataAccess.Repositories.Implementations;

internal class ProductRepository(AppDbContext _context) : Repository<Product>(_context), IProductRepository
{
}
