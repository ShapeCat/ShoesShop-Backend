using AutoMapper;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Persistence;
using Xunit;

namespace ShoesShop.Tests.Core
{
    [Collection("QueryCollection")]
    public class AbstractQueryTests
    {
        protected IUnitOfWork UnitOfWork { get; }
        protected IMapper Mapper { get; }

        public AbstractQueryTests(QueryFixture fixture)
        {
            UnitOfWork = new UnitOfWork(fixture.DbContext, false);
            Mapper = fixture.Mapper;
        }
    }
}
