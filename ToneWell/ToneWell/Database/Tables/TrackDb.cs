
using System.ComponentModel.DataAnnotations;

namespace ToneWell.Database.Tables
{
    public class TrackDb
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public string FilePath { get; set; }
    }
}
