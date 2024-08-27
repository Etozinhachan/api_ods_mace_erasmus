using api_ods_mace_erasmus.models;

namespace api_ods_mace_erasmus.Interfaces;

public interface IActivityRepository
{
    /*
    public bool chatExists(Guid id);
    public bool isFinal(ChatSucessfullResponse aiResponse);
    public ChatSucessfullResponse? getLastAiResponse(Chat chat);
    public UserPrompt getLastUserPrompt(Chat chat);
    public Chat? getChatByConvoId(Guid id);
    public ICollection<UserPrompt> getAllUserPrompts();
    public ICollection<UserPrompt> getAllUserPrompts(Guid user_id);
    public ICollection<ChatSucessfullResponse> getAllAiResponses();
    public ICollection<ChatSucessfullResponse> getAllAiResponses(Guid user_id);
    public ICollection<Chat> getAllUserChats(Guid user_id);
    public void AddUserPrompt(UserPrompt userPrompt);
    public void AddAiResponse(ChatSucessfullResponse AiResponse);
    public void AddChat(Chat chat);
    public void aiResponseModified(ChatSucessfullResponse chatResponse);
    public void chatModified(Chat chat);
    public void deleteChat(Chat chat);
*/
    public bool ActivityExists(Guid id);
    public Activity? getActivity(Guid id);
    public ICollection<Activity> GetAllActivities();
    public ICollection<Activity> GetAllActivitiesFromUserId(Guid user_id);
    public void AddActivity(Activity activity);
    public void ActivityModified(Activity activity);
    public void DeleteActivity(Activity activity);
    public void SaveChanges();

}