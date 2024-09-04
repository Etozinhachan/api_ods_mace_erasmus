using Microsoft.EntityFrameworkCore;
using api_ods_mace_erasmus.data;
using api_ods_mace_erasmus.Interfaces;
using api_ods_mace_erasmus.models;

namespace api_ods_mace_erasmus.Repositories;

public class TranslationRepository : ITranslationRepository
{
    private readonly DbDataContext _context;

    public TranslationRepository(DbDataContext context)
    {
        _context = context;
    }

    public bool TranslationExists(Guid id)
    {
        return _context.Translations.Any(a => a.id == id);
    }

    public void TranslationModified(Translation translation)
    {
        _context.Entry(translation).State = EntityState.Modified;
        SaveChanges();
    }

    public void AddTranslation(Translation translation)
    {
        _context.Translations.Add(translation);
        SaveChanges();
    }

    public void DeleteTranslation(Translation translation)
    {
        _context.Remove(translation);
        SaveChanges();
    }

    public Translation? GetTranslation(Guid id)
    {
        return _context.Translations.Where(a => a.id == id).FirstOrDefault();
    }

    public ICollection<Translation> GetAllTranslations()
    {
        return _context.Translations.ToList();
    }

    public ICollection<Translation> GetAllTranslationsFromUserId(Guid user_id)
    {
        return _context.Translations.Where(a => a.user_id == user_id).ToList();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}