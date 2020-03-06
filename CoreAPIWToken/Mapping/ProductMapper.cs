using AutoMapper;
using CoreAPIWToken.Domain.Models;
using CoreAPIWToken.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIWToken.Mapping
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductResource>();
            CreateMap<ProductResource, Product>();
            CreateMap<UserResource, User>();
            CreateMap<User, UserResource>();
           
        }
    }
}
