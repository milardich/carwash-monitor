using System.ComponentModel.DataAnnotations;
using CarwashMonitor.Enums;
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
        public List<WashCycle?> WashCycles { get; set; } = [];
    }
}
