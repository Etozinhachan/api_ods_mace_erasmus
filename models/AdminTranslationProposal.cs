using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_ods_mace_erasmus.models;

public class AdminTranslationProposal{

    public byte? translation_state { get; set; }
    public string? language_code { get; set; }
    public string? translation_json { get; set; }

}