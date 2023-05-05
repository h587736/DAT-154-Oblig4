using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library3.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string RequestType { get; set; }
        public DateTime DateRequested { get; set; }
        public string Description { get; set; }
        public DateTime? DateCompleted { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }
    }

}
