using System.ComponentModel.DataAnnotations;
using CarwashMonitor.Enum;
using CarwashMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace CarwashMonitor.Model
{
    public class Box
    {
        public Guid Id { get; set; }
        public BoxStatus? Status { get; set; }
        public Station? Station { get; set; }    
        public Guid? StationId { get; set; }    
    }
}
