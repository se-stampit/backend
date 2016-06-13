using Stampit.Entity;
using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.Fakes
{
    public class FakeStampcardRepository : FakeBaseRepository<Stampcard>, IStampcardRepository
    {
        private IProductRepository ProductRepository { get; }
        private IEnduserRepository EnduserRepository { get; }

        public FakeStampcardRepository(IProductRepository productRepository, IEnduserRepository enduserRepository)
        {
            this.ProductRepository = productRepository;
            this.EnduserRepository = enduserRepository;

            var products = ProductRepository.GetAllAsync(0).Result.ToList();
            var enduser = EnduserRepository.GetAllAsync(0).Result.First();

            for (int i = 0; i < products.Count; i++)
            {
                Data[i].Product = products[i];
                Data[i].ProductId = products[i].Id;
                Data[i].Enduser = enduser;
                Data[i].EnduserId = enduser.Id;
            }
        }

        protected override IList<Stampcard> Data { get; } = new List<Stampcard>
        {
            new Stampcard
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                CreatedAt = DateTime.Now,
                IsRedeemed = false,
                Stamps = new List<Stamp>()
            },
            new Stampcard
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                CreatedAt = DateTime.Now,
                IsRedeemed = false,
                Stamps = new List<Stamp>()
            },
            new Stampcard
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                CreatedAt = DateTime.Now,
                IsRedeemed = false,
                Stamps = new List<Stamp>()
            },
            new Stampcard
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                CreatedAt = DateTime.Now,
                IsRedeemed = false,
                Stamps = new List<Stamp>()
            }
        };

        public Task<IEnumerable<Stampcard>> GetAllStampcards(Enduser user, int page, int pagesize = 10)
        {
            return Task.FromResult
                (
                    (from entity in Data
                     where entity.EnduserId == user?.Id
                        && !string.IsNullOrEmpty(user?.Id)
                     orderby entity.Stamps.Count descending
                     select entity).Skip(page * pagesize).Take(pagesize)
                );
        }

        public Task<IEnumerable<Stampcard>> GetAllStampcardsFromProduct(Enduser user, Product product)
        {
            return Task.FromResult
                (
                     (from entity in Data
                      where entity.EnduserId == user?.Id
                         && !string.IsNullOrEmpty(user?.Id)
                         && entity.ProductId == product?.Id
                         && !string.IsNullOrEmpty(product?.Id)
                      orderby entity.Stamps.Count
                      select entity)
                    as IEnumerable<Stampcard>
                );
        }

        //TODO
        public Task<int> CountStampcardsFromCompany(Company company)
        {
            return Task.FromResult(20);
        }

        //TODO
        public Task<int> CountRedeemedStampcardsFromCompany(Company company)
        {
            return Task.FromResult(15);
        }

        //TODO
        //X-Achsis, Y-Achsis
        public Task<IDictionary<Product, IDictionary<int, int>>> GetAllStampcardsFromCompany(Company company)
        {
            return Task.FromResult(new Dictionary<Product, IDictionary<int, int>>()
            {
                { Data[0].Product, 
                  new Dictionary<int, int>() {
                      {1,10},
                      {2,9},
                      {3,8},
                      {4,7},
                      {5,6},
                      {6,5},
                      {7,4},
                      {8,3},
                      {9,2},
                      {10,1}
                }},
                { Data[1].Product,
                  new Dictionary<int, int>() {
                      {1,10},
                      {2,9},
                      {3,8},
                      {4,7},
                      {5,6},
                      {6,5},
                      {7,4},
                      {8,3},
                      {9,2},
                      {10,1}
                }},
                { Data[2].Product,
                  new Dictionary<int, int>() {
                      {1,10},
                      {2,9},
                      {3,8},
                      {4,7},
                      {5,6},
                      {6,5},
                      {7,4},
                      {8,3},
                      {9,2},
                      {10,1}
                }},
                { Data[3].Product,
                  new Dictionary<int, int>() {
                      {1,10},
                      {2,9},
                      {3,8},
                      {4,7},
                      {5,6},
                      {6,5},
                      {7,4},
                      {8,3},
                      {9,2},
                      {10,1}
                }}
            } as IDictionary<Product, IDictionary<int, int>>);
        }
    }
}
