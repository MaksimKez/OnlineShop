using BLL.Dtos;
using BLL.FluentValidation;
using BLL.Mappers;
using BLL.Services;
using DAL;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using proj.Mappers;
using proj.ViewModels;

namespace proj
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IMapper<UserDto, UserEntity>, UserMapper>();
            builder.Services.AddScoped<IMapper<ProductDto, ProductEntity>, ProductMapper>();
            builder.Services.AddScoped<IMapper<OrderDto, OrderEntity>, OrderMapper>();
            builder.Services.AddScoped<IMapper<CartDto, CartEntity>, CartMapper>();
            builder.Services.AddScoped<IMapper<CartItemDto, CartItemEntity>, CartItemMapper>();
            
            builder.Services.AddScoped<IMapperVMs<CartItemViewModel, CartItemDto>, CartItemMapperVm>();
            builder.Services.AddScoped<IMapperVMs<CartViewModel, CartDto>, CartMapperVm>();
            builder.Services.AddScoped<IMapperVMs<UserViewModel, UserDto>, UserMapperVm>();
            builder.Services.AddScoped<IMapperVMs<ProductViewModel, ProductDto>, ProductMapperVm>();
            builder.Services.AddScoped<IMapperVMs<OrderViewModel, OrderDto>, OrderMapperVm>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<ICartItemService, CartItemService>();
            
            builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<OrderValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CartValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CartItemValidator>();
            builder.Services.AddFluentValidationAutoValidation();
            
            builder.Services.AddDbContext<ApplicationDbContext>(x =>
            {
                x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
