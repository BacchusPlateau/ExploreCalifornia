using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExploreCalifornia1.Models
{
    public class Post
    {
        public long Id { get; set; }
        private string _key;
        public string Key
        {
            get
            {
                _key = _key ?? Regex.Replace(Title.ToLower(), "[^a-z0-9]", "-");
                return _key;
            }
            set { _key = value; }
        }
        [Display(Name = "Post Title")]
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Hey dummy, 5-100 chars here.")]
        public string Title { get; set; }
        public string Author { get; set; }
        [Required]
        [MinLength(100, ErrorMessage = "Dummy, it has to be longer.")]
        public string Body { get; set; }
        public DateTime Posted { get; set; }
    }
}
