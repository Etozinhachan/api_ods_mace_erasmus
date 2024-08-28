namespace api_ods_mace_erasmus.models;

public class UserLoginResponse{
    public required string jwt_token { get; set; }
    public required bool isAdmin { get; set; }
}