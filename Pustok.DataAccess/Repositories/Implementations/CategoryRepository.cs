using Pustok.DataAccess.Contexts;
using Pustok.DataAccess.Repositories.Abstractions;
using Pustok.DataAccess.Repositories.Implementations.Generic;

namespace Pustok.DataAccess.Repositories.Implementations;

internal class CategoryRepository(AppDbContext _context) : Repository<Category>(_context), ICategoryRepository
{
}
