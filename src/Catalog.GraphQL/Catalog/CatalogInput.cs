using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.GraphQL.Catalog;
public class CatalogInput
{   
    public int Id { get; set; }

    public string Name { get; set; }
    
    public string Image { get; set; }
    
    public int? CatalogId { get; set; }
    
}
