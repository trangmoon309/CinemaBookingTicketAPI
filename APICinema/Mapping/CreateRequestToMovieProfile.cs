using APICinema.Models;
using AutoMapper;
using Contracts.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Mapping
{
    public class CreateRequestToMovieProfile : Profile
    {
        public CreateRequestToMovieProfile()
        {
            CreateMap<CreateMovieRequest, Movie>();
        }
    }
}
