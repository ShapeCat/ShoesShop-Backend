using AutoMapper;
using ShoesShop.Application.Interfaces;
using ShoesShop.Persistence;
using Xunit;

namespace ShoesShop.Tests.Core
{
    [Collection("QueryCollection")]
    public class AbstractQueryTests
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        public AbstractQueryTests(QueryFixture fixture)
        {
            unitOfWork = new UnitOfWork(fixture.DbContext, false);
            mapper = fixture.Mapper;
        }
    }
}
