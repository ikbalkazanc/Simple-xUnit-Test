using System;
using System.Collections.Generic;

#nullable disable

namespace UnitTestIntegration.Web.Model
{
    //Product Model
    public partial class Table
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
    }
}
