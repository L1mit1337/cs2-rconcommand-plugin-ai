using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Modules.Commands;
using System.Text.Json;

namespace RconCommand;

public class RconConfig
{
    public List<string> AdminSteamIds { get; set; } = new();
}

[MinimumApiVersion(100)]
public class RconCommand : BasePlugin
{
    private RconConfig Config { get; set; } = new();
    
    public override string ModuleName => "RCON Command Plugin";
    public override string ModuleVersion => "0.0.1a";
    public override string ModuleAuthor => "L1MIT1337";
    public override string ModuleDescription => "RCON Command Plugin Author's Github: https://github.com/L1MIT1337/";

    public override void Load(bool hotReload)
    {
        Console.WriteLine("RCON插件已加载");
        
        // 加载配置文件
        string configPath = Path.Join(ModuleDirectory, "..\\..\\configs\\plugins\\RconCommandPlugin\\rcon_admin_config.json");
        Config = JsonSerializer.Deserialize<RconConfig>(File.ReadAllText(configPath))!;
        
        
        AddCommand(".rcon", "Execute RCON command", CommandRcon);
    }

    private void CommandRcon(CCSPlayerController? player, CommandInfo command)
    {
        // 检查玩家是否有管理员权限
        if (player == null || !IsAdmin(player))
        {
            player?.PrintToConsole($" \x02你没有权限执行此命令！");
            player?.PrintToChat($" \x02你没有权限执行此命令！");
            return;
        }

        // 获取命令参数
        string rconCommand = string.Join(" ", command.ArgString);
        
        if (string.IsNullOrEmpty(rconCommand))
        {
            player.PrintToConsole("用法: .rcon <命令>");
            player.PrintToChat("用法: .rcon <命令>");
            return;
        }

        // 执行RCON命令
        Server.ExecuteCommand(rconCommand);
        player.PrintToChat($" \x04已执行RCON命令: {rconCommand}"); // 使用绿色显示成功消息
    }

    private bool IsAdmin(CCSPlayerController player)
    {
        if (player == null) return false;
        
        // 获取玩家的 Steam ID
        string? steamId = player.SteamID.ToString();
        if (string.IsNullOrEmpty(steamId)) return false;

        // 检查是否在管理员列表中
        return Config.AdminSteamIds.Contains(steamId);
    }
}