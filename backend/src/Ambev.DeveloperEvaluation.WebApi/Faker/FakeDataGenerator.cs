using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Queries;
using Bogus;
using Bogus.Extensions.Brazil;

namespace Ambev.DeveloperEvaluation.WebApi.Faker
{
    public class FakeDataGenerator
    {
        public static List<ProductExternalQuery> GenerateFakeProductsQuery(int count)
        {
            var productsFaker = new Faker<ProductExternalQuery>()
                .RuleFor(x => x.id, f => Guid.NewGuid().ToString())
                .RuleFor(x => x.Price, f => f.Finance.Amount(50.0m, 5000.0m)) 
                .RuleFor(x => x.Name, f => f.Commerce.ProductName())
                .RuleFor(x => x.Category, f => f.Commerce.Categories(1)[0]);
            
            var data =  productsFaker.Generate(count);

            return data;
        }

        public static List<BranchExternalQuery> GenerateFakeBranchesQuery(int count)
        {
            var branchFaker = new Faker<BranchExternalQuery>()
               .RuleFor(x => x.id, f => Guid.NewGuid().ToString())
               .RuleFor(x => x.Name, f => f.Company.Cnpj());

            var data = branchFaker.Generate(count);

            return data;
        }

        public static List<UserExternalQuery> GenerateFakeUsersQuery(int count)
        {
            var userFaker = new Faker<UserExternalQuery>()
               .RuleFor(x => x.id, f => Guid.NewGuid().ToString())
               .RuleFor(x => x.Name, f => f.Name.FullName())
               .RuleFor(x => x.Email, f => f.Internet.Email());

            var data = userFaker.Generate(count);

            return data;
        }
    }
}
