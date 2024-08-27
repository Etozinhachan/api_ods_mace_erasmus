using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_ods_mace_erasmus.models;

public class AdminActivityProposal{

    public byte? activity_state { get; set; } = null;
    public string? country { get; set; } = null;
    public decimal? latitude { get; set; } = null;
    public decimal? longitude { get; set; }  = null;
    public string? ods { get; set; } = null;
    public string? type { get; set; } = null;
    public string? explanation { get; set; } = null;
    public List<string>? image_uris { get; set; } = null;

}