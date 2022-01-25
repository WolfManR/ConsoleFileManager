using Thundire.FileManager.Core.Configurations;

namespace Thundire.FileManager.Core;

public class ThundireFileManager
{
    public Configuration Configuration { get; set; }
    public IFilesManager FilesManager { get; set; }
    public INotifyService NotifyService { get; set; }
    public IRenderer Renderer { get; set; }
    public ILifeTimeService LifeTimeService { get; set; }
    public ICommandsRepository CommandsRepository { get; set; }

    private Lazy<List<Action<ThundireFileManager>>> OnInitializedActions = new(() => new());

    public ThundireFileManager OnInitialized(Action<ThundireFileManager> behavior)
    {
        OnInitializedActions.Value.Add(behavior);
        return this;
    }

    public void Initialize()
    {
        if(!OnInitializedActions.IsValueCreated) return;

        OnInitializedActions.Value.Aggregate(this, (manager, action) =>
        {
            action.Invoke(manager);
            return manager;
        });
    }
}