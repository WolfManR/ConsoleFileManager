using Thundire.FileManager.Core;

new FilesManagerSystem()
    .Configure(config =>
    {
        config.OnClose = Close;
    })
    .Start();


static void Close()
{

}