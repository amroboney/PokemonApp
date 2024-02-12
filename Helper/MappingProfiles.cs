using System;
using AutoMapper;
using PokemonApp.Data.Dto;
using PokemonApp.Models;

namespace PokemonApp.Helper
{
	public class MappingProfiles:  Profile
	{
		public MappingProfiles()
		{
			CreateMap<Pokemon, PokemonDto>();
			CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
			CreateMap<CountryDto, Country>();
        }
    }
}

