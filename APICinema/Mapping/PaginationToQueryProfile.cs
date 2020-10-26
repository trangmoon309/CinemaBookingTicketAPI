using APICinema.Pagination;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Mapping
{
    public class PaginationToQueryProfile : Profile
    {
        public PaginationToQueryProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
        }
    }
}
