using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_ods_mace_erasmus.models;

public class LoginBadResponse{

    
    public required string title { get; set; }
    public required int status { get; set; }
    public string? detail { get; set; }

}