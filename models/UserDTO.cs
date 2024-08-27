using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_ods_mace_erasmus.models;

public class UserDTO{

    public required string UserName { get; set; }
    public required string passHash { get; set; }


}