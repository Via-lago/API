using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Client.Models;
public class University
{
    public Guid Guid { get; set; }
    [Required]
    public string Code { get; set; }
    [Required]
    public string Name { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }

}