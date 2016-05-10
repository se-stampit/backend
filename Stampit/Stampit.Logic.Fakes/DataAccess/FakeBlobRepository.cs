using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stampit.Entity;
using System.Reflection;
using System.IO;

namespace Stampit.Logic.Fakes
{
    public class FakeBlobRepository : FakeBaseRepository<Blob>, IBlobRepository
    {
        public FakeBlobRepository()
        {
            new Action(async () => await InitImages()).Invoke();
            //Only Fakedata, this is why this call is ok
        }

        private async Task InitImages()
        {
            foreach (var blob in Data)
            {
                using (var fs = Assembly.GetExecutingAssembly().GetManifestResourceStream(blob.Filename))
                {
                    using (var ms = new MemoryStream())
                    {
                        await fs.CopyToAsync(ms);
                        blob.ContentLength = ms.Length;
                        blob.Content = ms.ToArray();
                    }
                }
            }
        }

        protected override IList<Blob> Data { get; } = new List<Blob>
        {
            new Blob
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                CreatedAt = DateTime.Now,
                Filename = "Stampit.Logic.Fakes.CoffeeRoom.png",
                ContentType = "image/png"
            },
            new Blob
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                CreatedAt = DateTime.Now,
                Filename = "Stampit.Logic.Fakes.KebapHouse.png",
                ContentType = "image/png"
            }
        };
    }
}
