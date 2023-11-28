using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Reviews.OutputVMs
{
    public class ReviewVmProfile : Profile
    {
        public ReviewVmProfile()
        {
            CreateMap<Review, ReviewVm>().ForMember(x => x.ReviewId, y => y.MapFrom(y => y.ReviewId))
                                         .ForMember(x => x.ModelId, y => y.MapFrom(y => y.ModelId))
                                         .ForMember(x => x.UserId, y => y.MapFrom(y => y.UserId))
                                         .ForMember(x => x.PublishDate, y => y.MapFrom(y => y.PublishDate))
                                         .ForMember(x => x.Rating, y => y.MapFrom(y => y.Rating))
                                         .ForMember(x => x.Comment, y => y.MapFrom(y => y.Comment));
        }
    }
}
