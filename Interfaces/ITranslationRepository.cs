using api_ods_mace_erasmus.models;

namespace api_ods_mace_erasmus.Interfaces;

public interface ITranslationRepository
{

    public bool TranslationExists(Guid id);
    public Translation? GetTranslation(Guid id);
    public ICollection<Translation> GetAllTranslations();
    public ICollection<Translation> GetAllTranslationsFromUserId(Guid user_id);
    public void AddTranslation(Translation translation);
    public void TranslationModified(Translation translation);
    public void DeleteTranslation(Translation translation);
    public void SaveChanges();

}