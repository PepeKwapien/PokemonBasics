using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Tests.TestHelpers
{
    internal class PokemonDbSetHelper
    {
        public static Mock<DbSet<TEntity>> SetUpDbSetMock<TEntity>(List<TEntity> mockCollection) where TEntity : class
        {
            var dbSetMock = new Mock<DbSet<TEntity>>();

            var queryableCollection = mockCollection.AsQueryable();

            dbSetMock.As<IQueryable<TEntity>>().Setup(set => set.Provider).Returns(queryableCollection.Provider);
            dbSetMock.As<IQueryable<TEntity>>().Setup(set => set.Expression).Returns(queryableCollection.Expression);
            dbSetMock.As<IQueryable<TEntity>>().Setup(set => set.ElementType).Returns(queryableCollection.ElementType);
            dbSetMock.As<IQueryable<TEntity>>().Setup(set => set.GetEnumerator()).Returns(() => queryableCollection.GetEnumerator());

            return dbSetMock;
        }
    }
}
