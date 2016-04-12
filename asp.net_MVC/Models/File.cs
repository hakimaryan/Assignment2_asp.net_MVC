using System.ComponentModel.DataAnnotations;

namespace asp.net_MVC.Models
{
    public class File
    {
        public int FileId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public FileType FileType { get; set; }
        public int PetId { get; set; }
        public virtual Pet Pet { get; set; }
    }
}

