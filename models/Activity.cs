using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_ods_mace_erasmus.models;

public class Activity{

    public Guid id { get; set; }
    public Guid user_id { get; set; }
    //public required string ActivityName { get; set; }
    public required byte activity_state { get; set; }
    public required string country { get; set; }
    public required decimal latitude { get; set; }
    public required decimal longitude { get; set; } 
    public required string ods { get; set; }
    public required string type { get; set; }
    public required string explanation { get; set; }
    public required List<string> image_uris { get; set; }
    [JsonIgnore]
    public User? submited_by_user { get; set; }

}