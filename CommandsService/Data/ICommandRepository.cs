using CommandsService.Models;

namespace CommandsService.Data
{
    public interface ICommandRepository
    {
        bool SaveChanges();

        //Platforms
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform platform);
        bool PlatformExits(int platformId);
        bool ExternalPlatformExists(int externalPlatformId);

        //Commands
        IEnumerable<Command> GetAllCommandsForPlatform(int platformId);
        Command GetCommand(int platformId, int commandId);
        void CreateCommand(int platformId, Command command);

    }
}