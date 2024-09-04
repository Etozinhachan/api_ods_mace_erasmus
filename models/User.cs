using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_ods_mace_erasmus.models;

public class User{

    public Guid id { get; set; }
    public required string UserName { get; set; }
    public required string passHash { get; set; }
    public string? salt { get; set; }
    public bool isAdmin { get; set; } = false;
    public ICollection<Activity>? submitted_activities { get; }
    public ICollection<Translation>? submitted_translations { get; }

}