using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AandelenApplicatie.Models.Message
{
    public class Message
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TypeMessage Type { get; set; }
        public int CompanyId { get; set; }
    }

    public enum TypeMessage { Succes, Fail }
}