using System;
using System.Windows.Forms;
using DiscordRPC;  // Ensure this namespace is included

namespace StarZLauncher.Classes
{
    public static class DiscordRichPresenceManager
    {
        private static readonly string ClientId = "1323549148393377875";
        private static DiscordRpcClient? discordClient;

        public static DiscordRpcClient DiscordClient
        {
            get
            {
                discordClient ??= new DiscordRpcClient(ClientId);
                return discordClient;
            }
        }

        public static void SetPresence(string details = "")
        {
            try
            {
                string state = ConfigManager.GetDiscordRPCIdleStatus();
                DiscordClient.SetPresence(new RichPresence
                {
                    State = state,
                    Details = details,
                    Timestamps = Timestamps.Now,
                    Assets = new Assets
                    {
                        LargeImageKey = "Luna",
                        LargeImageText = "Lumina Launcher",
                        SmallImageKey = "minecraft",
                        SmallImageText = "Minecraft Launcher For Windows"
                    },
                    Buttons = new DiscordRPC.Button[]  // Fully qualify the Button class here
                    {
                        new DiscordRPC.Button
                        {
                            Label = "Download Launcher",
                            Url = "https://projectlumina.netlify.app"
                        },
                        new DiscordRPC.Button
                        {
                            Label = "Discord",
                            Url = "https://projectlumina.netlify.app/discord"
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LogManager.Log($"{ex.Message}", "DiscordClient.txt");
            }
        }

        public static void IdlePresence(string state)
        {
            try
            {
                DiscordClient.UpdateState(state);
                DiscordClient.UpdateDetails("");
            }
            catch (Exception ex)
            {
                LogManager.Log($"{ex.Message}", "DiscordClient.txt");
            }
        }

        public static void TerminatePresence()
        {
            if (DiscordClient.IsDisposed) return;
            try
            {
                DiscordClient.ClearPresence();
                DiscordClient.Deinitialize();
                DiscordClient.Dispose();
            }
            catch (Exception ex)
            {
                LogManager.Log($"{ex.Message}", "DiscordClient.txt");
            }
        }
    }
}
