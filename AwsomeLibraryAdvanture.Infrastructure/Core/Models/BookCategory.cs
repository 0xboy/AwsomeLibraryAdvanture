using System;
using System.Collections.Generic;
using System.Text;

namespace AwsomeLibraryAdvanture.Infrastructure.Core.Models
{
    public class BookCategory
    {
        public int Id { get; set; }

        public int? BaseId { get; set; }

        public string Name { get; set; }

        public List<BookCategory> SubCategories { get; set; }

        public BookCategory BaseCategory { get; set; }
    }
}
