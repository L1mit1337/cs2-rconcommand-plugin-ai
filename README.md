# cs2-rconcommand-plugin-ai
这个插件是由AI 100%生成的简单rcon插件 只能实现在游戏内执行命令 做不到完全修复原版rcon

# 如何配置

配置文件在addons\counterstrikesharp\configs\plugins\RconCommandPlugin\rcon_admin_config.json

文件格式

```json
{
    "AdminSteamIds": [
        "your steamID64"
    ]
}
```

填上你的steamID64 你就可以使用.rcon命令 没有在AdminSteamIds里的用户是没有权限使用.rcon命令

# 如何使用

```
用法 .rcon <命令>
```

例如 .rcon map de_dust2
