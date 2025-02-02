using AutoMapper;
using WebStore.DTOs;
using WebStore.Models;

namespace WebStore.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<ProductStockDTO, Product>();
            CreateMap<Product , ProductStockDTO> ();

            CreateMap<ReportDTO, Report>();
            CreateMap<Report , ReportDTO> ();

            CreateMap<ProductRequestDTO, Product>();
            CreateMap<Product, ProductRequestDTO>();

            CreateMap<Brand, BrandRequestDTO>();
            CreateMap<BrandRequestDTO, Brand>();

            CreateMap<Brand, BrandResponseDTO>();
            CreateMap<BrandResponseDTO, Brand>();

            CreateMap<Category, CategoryRequestDTO>();
            CreateMap<CategoryRequestDTO, Category>();

            CreateMap<Category, CategoryResponseDTO>();
            CreateMap<CategoryResponseDTO, Category>();

            CreateMap<Color, ColorRequestDTO>();
            CreateMap<ColorRequestDTO, Color>();

            CreateMap<Color, ColorResponseDTO>();
            CreateMap<ColorResponseDTO, Color>();

            CreateMap<Gender, GenderRequestDTO>();
            CreateMap<GenderRequestDTO, Gender>();

            CreateMap<Gender, GenderResponseDTO>();
            CreateMap<GenderResponseDTO, Gender>();

            CreateMap<Size, SizeRequestDTO>();
            CreateMap<SizeRequestDTO, Size>();

            CreateMap<Size, SizeResponseDTO>();
            CreateMap<SizeResponseDTO, Size>();

            CreateMap<Product, ProductRequestDTO>();
            CreateMap<ProductRequestDTO, Product>();

            CreateMap<Product, ProductResponseDTO>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : null))
            .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender != null ? src.Gender.Name : null))
            .ForMember(dest => dest.ColorName, opt => opt.MapFrom(src => src.Color != null ? src.Color.Name : null))
            .ForMember(dest => dest.SizeName, opt => opt.MapFrom(src => src.Size != null ? src.Size.Name : null));

            CreateMap<Order, OrderRequestDTO>();
            CreateMap<OrderRequestDTO, Order>()
              .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<UserResponseDTO, User>();
            CreateMap<Order, OrderResponseDTO>()
               .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
               .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
               .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));


            // Map between OrderItem and OrderItemRequestDTO
            CreateMap<OrderItem, OrderItemRequestDTO>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId)); // Map the ProductId
            CreateMap<OrderItemRequestDTO, OrderItem>()
                .ForMember(dest => dest.Product, opt => opt.Ignore()); // Product entity should not be mapped directly from DTO


            CreateMap<OrderItem, OrderItemResponseDTO>();
            CreateMap<OrderItemResponseDTO, OrderItem>();


            CreateMap<User, UserResponseDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)); // Explicitly map Id



        }
    }
}
