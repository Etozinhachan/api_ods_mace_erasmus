using Microsoft.EntityFrameworkCore;
using api_ods_mace_erasmus.data;
using api_ods_mace_erasmus.Interfaces;
using api_ods_mace_erasmus.models;

namespace api_ods_mace_erasmus.Repositories;

public class ActivityRepository : IActivityRepository
{
    private readonly DbDataContext _context;

    public ActivityRepository(DbDataContext context)
    {
        _context = context;
    }

    public bool ActivityExists(Guid id)
    {
        return _context.Activities.Any(a => a.id == id);
    }

    public void ActivityModified(Activity activity)
    {
        _context.Entry(activity).State = EntityState.Modified;
        SaveChanges();
    }

    public void AddActivity(Activity activity)
    {
        _context.Activities.Add(activity);
        SaveChanges();
    }

    public void DeleteActivity(Activity activity)
    {
        _context.Remove(activity);
        SaveChanges();
    }

    public Activity? getActivity(Guid id)
    {
        return _context.Activities.Where(a => a.id == id).FirstOrDefault();
    }

    public ICollection<Activity> GetAllActivities()
    {
        return _context.Activities.ToList();
    }

    public ICollection<Activity> GetAllActivitiesFromUserId(Guid user_id)
    {
        return _context.Activities.Where(a => a.user_id == user_id).ToList();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}