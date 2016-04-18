using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mobet.Authorization.Models
{
    public class PassportUserViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string SubjectId { get; set; }

        public string HeadImageUrl { get; set; }
    }
}