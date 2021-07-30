using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test2Dev.Models
{
    public class DatatablesServerModel
    {
        public int Draw { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public IEnumerable<Columns> Columns { get; set; }

        public IEnumerable<Order> Order { get; set; }
    }

    public class Search
    {

        public bool Regex { get; set; }
        public string Value { get; set; }
    }

    public class Order
    {
        public string Column { get; set; }
        public string Dir { get; set; }
    }

    public class Columns
    {

        public string Data { get; set; }

        public string Name { get; set; }

        public bool Searchable { get; set; }

        public bool Orderable { get; set; }

        public Search Search { get; set; }
    }
}