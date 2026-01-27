using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pustok.Business.Exceptions;
using Pustok.Business.Services.Abstractions;
using Pustok.Core.Entities;
using Pustok.DataAccess.Repositories.Abstractions;

namespace Pustok.Business.Services.Implementations;

internal class ProductService : IProductService
{

    private readonly IProductRepository _repository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;

    public ProductService(IProductRepository repository, IMapper mapper, ICloudinaryService cloudinaryService, ICategoryRepository categoryRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
        _categoryRepository = categoryRepository;
    }

    public async Task CreateAsync(ProductCreateDto dto)
    {
        var isExistCategory = await _categoryRepository.AnyAsync(x => x.Id == dto.CategoryId);

        if (!isExistCategory)
            throw new NotFoundException("Category is not found");


        var product = _mapper.Map<Product>(dto);

        var imagePath = await _cloudinaryService.FileUploadAsync(dto.Image);

        product.ImagePath = imagePath;

        await _repository.AddAsync(product);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product is null)
            throw new NotFoundException("Product is not found");


        await _cloudinaryService.FileDeleteAsync(product.ImagePath);

        _repository.Delete(product);
        await _repository.SaveChangesAsync();
    }

    public async Task<List<ProductGetDto>> GetAllAsync()
    {
        var products = await _repository.GetAll().Include(x => x.Category).ToListAsync();

        var dtos = _mapper.Map<List<ProductGetDto>>(products);

        return dtos;
    }

    public async Task<ProductGetDto?> GetByIdAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product is null)
            throw new NotFoundException("Product is not found");

        var dto = _mapper.Map<ProductGetDto>(product);

        return dto;
    }

    public async Task UpdateAsync(ProductUpdateDto dto)
    {

        var isExistCategory = await _categoryRepository.AnyAsync(x => x.Id == dto.CategoryId);

        if (!isExistCategory)
            throw new NotFoundException("Category is not found");


        var existItem = await _repository.GetByIdAsync(dto.Id);

        if (existItem is null)
            throw new NotFoundException("Product is not found");


        existItem = _mapper.Map(dto, existItem);

        if (dto.Image is { })
        {
            await _cloudinaryService.FileDeleteAsync(existItem.ImagePath);

            var imagePath = await _cloudinaryService.FileUploadAsync(dto.Image);
            existItem.ImagePath = imagePath;
        }

        _repository.Update(existItem);
        await _repository.SaveChangesAsync();
    }
}
