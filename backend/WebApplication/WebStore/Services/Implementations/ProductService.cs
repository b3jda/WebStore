using AutoMapper;
using WebStore.DTOs;
using WebStore.Models;
using WebStore.Repositories.Interfaces;
using WebStore.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebStore.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IColorRepository _colorRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IBrandRepository brandRepository,
            IGenderRepository genderRepository,
            IColorRepository colorRepository,
            ISizeRepository sizeRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _genderRepository = genderRepository;
            _colorRepository = colorRepository;
            _sizeRepository = sizeRepository;
            _mapper = mapper;
        }

        public async Task<ProductResponseDTO> GetProductById(int productId)
        {
            var product = await _productRepository.GetProductById(productId);
            return _mapper.Map<ProductResponseDTO>(product);
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProducts();
            return _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
        }

        public async Task<ProductResponseDTO> AddProduct(ProductRequestDTO productRequest)
        {
            var category = await _categoryRepository.GetCategoryByName(productRequest.CategoryName);
            var brand = await _brandRepository.GetBrandByName(productRequest.BrandName);
            var gender = await _genderRepository.GetGenderByName(productRequest.GenderName);
            var color = await _colorRepository.GetColorByName(productRequest.ColorName);
            var size = await _sizeRepository.GetSizeByName(productRequest.SizeName);

            if (category == null)
                throw new Exception("Category not found");
            if (brand == null)
                throw new Exception("Brand not found");
            if (gender == null)
                throw new Exception("Gender not found");
            if (color == null)
                throw new Exception("Color not found");
            if (size == null)
                throw new Exception("Size not found");

            var product = new Product
            {
                Name = productRequest.Name,
                Description = productRequest.Description,
                Price = productRequest.Price,
                Quantity = productRequest.Quantity,
                CategoryId = category.Id,
                BrandId = brand.Id,
                GenderId = gender.Id,
                ColorId = color.Id,
                SizeId = size.Id
            };

            await _productRepository.AddProduct(product);

            return _mapper.Map<ProductResponseDTO>(product);
        }

        public async Task UpdateProduct(ProductRequestDTO productRequest, int productId)
        {
            var existingProduct = await _productRepository.GetProductById(productId);
            if (existingProduct == null)
                throw new Exception("Product not found.");

            var category = await _categoryRepository.GetCategoryByName(productRequest.CategoryName);
            var brand = await _brandRepository.GetBrandByName(productRequest.BrandName);
            var gender = await _genderRepository.GetGenderByName(productRequest.GenderName);
            var color = await _colorRepository.GetColorByName(productRequest.ColorName);
            var size = await _sizeRepository.GetSizeByName(productRequest.SizeName);

            if (category == null)
                throw new Exception("Category not found");
            if (brand == null)
                throw new Exception("Brand not found");
            if (gender == null)
                throw new Exception("Gender not found");
            if (color == null)
                throw new Exception("Color not found");
            if (size == null)
                throw new Exception("Size not found");

            existingProduct.Name = productRequest.Name;
            existingProduct.Description = productRequest.Description;
            existingProduct.Price = productRequest.Price;
            existingProduct.Quantity = productRequest.Quantity;
            existingProduct.CategoryId = category.Id;
            existingProduct.BrandId = brand.Id;
            existingProduct.GenderId = gender.Id;
            existingProduct.ColorId = color.Id;
            existingProduct.SizeId = size.Id;

            await _productRepository.UpdateProduct(existingProduct, productId);
        }

        public async Task DeleteProduct(int productId)
        {
            await _productRepository.DeleteProduct(productId);
        }

        public async Task ApplyDiscount(int productId, double discountPercentage)
        {
            await _productRepository.ApplyDiscount(productId, discountPercentage);
        }

        public async Task<IEnumerable<ProductResponseDTO>> SearchProducts(
            string category, string gender, string brand, decimal? minPrice, decimal? maxPrice, string size, string color, bool? inStock)
        {
            var products = await _productRepository.SearchProducts(category, gender, brand, minPrice, maxPrice, size, color, inStock);
            return _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
        }

        public async Task<ProductStockDTO> GetRealTimeProductStock(int productId)
        {
            var productStock = await _productRepository.GetRealTimeProductStock(productId);
            return _mapper.Map<ProductStockDTO>(productStock);
        }
    }
}
