using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShortUrls.Models
{
    public class Click
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }

        public int BookmarkId { get; set; }

        [ForeignKey("BookmarkId")]
        public virtual Bookmark Bookmark { get; set; }
    }
}