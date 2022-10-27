using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Common.Models;


public class BaseDto
{
    public BaseDto()
    {
    }

    public List<Link> Links { get; set; } = new List<Link>();

}
