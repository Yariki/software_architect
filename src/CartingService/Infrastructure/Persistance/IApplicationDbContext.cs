using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;

namespace CartingService.Infrastructure.Persistance
{

    public interface IApplicationDbContext
    {
        public ILiteDatabase Database { get; }
    }
}