using MelonLoader;
using BTD_Mod_Helper;
using ScaleTowers;
using BTD_Mod_Helper.Api.ModOptions;
using BTD_Mod_Helper.Api.Enums;
using Il2CppAssets.Scripts.Models.Towers.Mods;
using Il2CppAssets.Scripts.Models;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

[assembly: MelonInfo(typeof(ScaleTowers.ScaleTowers), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace ScaleTowers;

public class ScaleTowers : BloonsTD6Mod
{
    private static readonly ModSettingDouble ScaleTowersDouble = new(2)
    {
        displayName = "Scale Towers",
        icon = VanillaSprites.SmallMonkeysModeIcon,
        min = 0.1,
        max = 5,
        slider = true
    };
    //private static readonly ModSettingBool ScaleCostBool = new(true)
    //{
    //    displayName = "Scale tower cost",
    //    icon = VanillaSprites.DoubleCashModeShop,
    //};
    private static readonly ModSettingBool ScaleVisualBool = new(false)
    {
        displayName = "Just visual scale",
        icon = VanillaSprites.BigBloonModeIcon2
    };
    private static readonly ModSettingBool ScaleRadiusBool = new(true)
    {
        displayName = "Scale tower footprint",
        icon = VanillaSprites.MonkeyCityUpgradeIcon
    };
    public override void OnNewGameModel(GameModel gameModel, Il2CppSystem.Collections.Generic.List<ModModel> mods)
    {
        foreach (var towerModel in gameModel.towers)
        {
            towerModel.displayScale = ScaleTowersDouble;
            if (ScaleVisualBool == false)
            {
                towerModel.range *= ScaleTowersDouble;
                foreach(var weaponModel in towerModel.GetWeapons())
                {
                    weaponModel.projectile.scale *= ScaleTowersDouble;
                    weaponModel.projectile.pierce *= ScaleTowersDouble;
                    weaponModel.rate /= ScaleTowersDouble;
                    if (weaponModel.projectile.HasBehavior<TravelStraitModel>())
                    {
                        weaponModel.projectile.GetBehavior<TravelStraitModel>().speed *= ScaleTowersDouble;
                        weaponModel.projectile.GetBehavior<TravelStraitModel>().lifespan *= ScaleTowersDouble;
                    }
                    if (weaponModel.projectile.HasBehavior<TravelCurvyModel>())
                    {
                        weaponModel.projectile.GetBehavior<TravelCurvyModel>().speed *= ScaleTowersDouble;
                        weaponModel.projectile.GetBehavior<TravelCurvyModel>().lifespan *= ScaleTowersDouble;
                    }
                    if (weaponModel.projectile.HasBehavior<DamageModel>())
                    {
                        weaponModel.projectile.GetBehavior<DamageModel>().damage *= ScaleTowersDouble;
                    }
                }
                foreach(var attackModel in towerModel.GetAttackModels())
                {
                    attackModel.range *= ScaleTowersDouble;
                }
                if (ScaleRadiusBool == true)
                {
                    towerModel.radius *= ScaleTowersDouble;
                }
            }
        }
    }
}