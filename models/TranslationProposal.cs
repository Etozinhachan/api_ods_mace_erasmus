using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_ods_mace_erasmus.models;

public class TranslationProposal{


    public required string language_code { get; set; }
    public required string translation_json { get; set; }

}