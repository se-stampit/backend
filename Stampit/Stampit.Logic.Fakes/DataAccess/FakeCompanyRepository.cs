using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stampit.Entity;

namespace Stampit.Logic.Fakes
{
    public class FakeCompanyRepository : FakeBaseRepository<Company>, ICompanyRepository
    {
        private IBlobRepository BlobRepository { get; }

        public FakeCompanyRepository(IBlobRepository blobRepository)
        {
            this.BlobRepository = blobRepository;
            var blobs = this.BlobRepository.GetAllAsync(0).Result;
            Data.First().BlobId = blobs.FirstOrDefault().Id;
            Data.First().Blob = blobs.FirstOrDefault();
            Data.Last().BlobId = blobs.LastOrDefault().Id;
            Data.Last().Blob = blobs.LastOrDefault();
        }

        protected override IList<Company> Data { get; } = new List<Company>
        {
            new Company()
            {
                Id = "ID123",
                CreatedAt = DateTime.Now,
                Products = new List<Product>(),
                Businessusers = new List<Businessuser>(),
                Stores = new List<Store>(),
                ContactAddress = "Mayer's Home",
                CompanyName = "CoffeeRoom",
                ContactName = "CoffeeMaster",
                Description = "Nice coffee house"
            },
            new Company()
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                CreatedAt = DateTime.Now,
                Products = new List<Product>(),
                Businessusers = new List<Businessuser>(),
                Stores = new List<Store>(),
                ContactAddress = "Middle in Austria",
                CompanyName = "KebapHouse",
                ContactName = "KebapMan",
                Description = "Nice kebap house"
            }
        };
    }
}
