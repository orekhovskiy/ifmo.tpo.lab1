using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ifmo.tpo.lab1.Models
{
    public class ResponseJson
    {
        /*public object Batchcomplete;
        public object Continue;*/
        public Query Query;
    }

    public class Query
    {
        public List<CategoryMember> CategoryMembers;
    }

    public class CategoryMember
    {
        public int Pageid;
        public int Ns;
        public string Title;
    }
}
