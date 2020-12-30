using System;
using System.Collections.Generic;
using System.Text;

namespace AwsomeLibraryAdvanture.Infrastructure.Core.Models
{
    public class Books
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ISBN { get; set; }

        public DateTime? AddedTime { get; set; }

        public string Publisher { get; set; }

        public Authors Author { get; set; }
    }
}
