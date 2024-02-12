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
			CreateMap<PokemonDto, Pokemon>();
			CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
			CreateMap<CountryDto, Country>();
			CreateMap<Country, CountryDto>();
			CreateMap<Owner, OwnerDto>();
			CreateMap<OwnerDto, Owner>();
			CreateMap<Review, ReviewDto>();
			CreateMap<ReviewDto, Review>();
			CreateMap<Reviewer, ReviewerDto>();
			CreateMap<ReviewerDto, Reviewer>();
        }
    }
}

