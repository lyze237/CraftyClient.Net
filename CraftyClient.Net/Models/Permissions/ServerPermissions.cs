namespace CraftyClientNet.Models.Permissions;

[Flags]
public enum ServerPermissions
{
    COMMANDS = 128, 
    TERMINAL = 64,
    LOGS = 32,
    SCHEDULE = 16,
    BACKUP = 8,
    FILES = 4,
    CONFIG = 2,
    PLAYERS = 1,
}