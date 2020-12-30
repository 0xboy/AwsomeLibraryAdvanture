using System;
using System.Collections.Generic;
using System.Text;

namespace AwsomeLibraryAdvanture.Infrastructure.Core.Models
{
    public class Authors
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Biography { get; set; }

        public List<Books> Books { get; set; }
    }
}
