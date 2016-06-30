using Stampit.Entity;
using Stampit.Logic.Interface;
using Stampit.Logic.Interface.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.DataAccess
{
    public class StampRepository : BaseRepository<Stamp>, IStampRepository
    {
    }
}
