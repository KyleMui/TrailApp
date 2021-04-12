using System.Collections.Generic;
using Packt.Shared;
namespace ParkMvc.Models {
 public class HomeIndexViewModel {
    public int VisitorCount;
    public IList<ParkTable> Parks{ get; set; } 
 }
}