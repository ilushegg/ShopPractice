using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ViewModel
{
    public class ProductViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public IFormFile? Picture { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
