using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_ods_mace_erasmus.models;

public class ActivityProposal{


    public required string country { get; set; }
    public required decimal latitude { get; set; }
    public required decimal longitude { get; set; } 
    public required string ods { get; set; }
    public required string type { get; set; }
    public required string explanation { get; set; }
    public required List<string> image_uris { get; set; }

}