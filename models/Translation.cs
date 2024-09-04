using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_ods_mace_erasmus.models;

public class Translation{

    public Guid id { get; set; }
    public Guid user_id { get; set; }
    public required byte translation_state { get; set; }
    public required string language_code { get; set; }
    public required string translation_json { get; set; }
    [JsonIgnore]
    public User? submited_by_user { get; set; }

}