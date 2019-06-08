package com.minecraft.servermanager.host;

import java.util.logging.Level;

import org.bukkit.Bukkit;
import org.bukkit.command.Command;
import org.bukkit.command.CommandSender;
import org.bukkit.command.ConsoleCommandSender;
import org.bukkit.entity.Player;
import org.bukkit.plugin.java.JavaPlugin;


public class mainClass extends JavaPlugin {
	@Override
	public void onEnable()
	{
		Bukkit.getLogger().log(Level.INFO, "Minecraft Server Manager has started logging.");
	}
	@Override
	public boolean onCommand(CommandSender sender,Command cmd ,String commandLabel,String[] args)
	{
		if (sender instanceof ConsoleCommandSender ) {
			if ((commandLabel.equalsIgnoreCase("callFunction"))){
				if (args.length >=1) {
					switch (args[0]) {
						case "getPlayerList":{
							String result="";
							for (Player player : Bukkit.getOnlinePlayers()) {
							 result = result + " " + player.getName();
							}
							result=result.trim();
							result=result.replace(' ', '|');
							Bukkit.getLogger().log(Level.INFO, result);
						}
						default:
					}
				}
			}
		}
		return true;
	}
	@Override
	public void onDisable() {
		Bukkit.getLogger().log(Level.INFO, "Minecraft Server Manager has ended logging.");
	}
}
