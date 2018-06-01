﻿using db.data;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
	partial class BehaviorDb
	{
		private _ Yazanahar = () => Behav ()
			.Init ("Yazanahar",
				new State (
					new HpLessTransition(0.20, "death1"),
					new SetAltTexture (2),
					new State (
						new State ("default",
							new ConditionalEffect(ConditionEffectIndex.Invincible),
							new ChatTransition ("default1", "Arise, Yazanahar, ARISE!")
						),
						new State ("default1",
							new ConditionalEffect(ConditionEffectIndex.Invincible),
							new Flash (0xFFFFFF, 2, 2),
							new TimedTransition (6000, "default2")
						),
						new State ("default2",
							new ConditionalEffect(ConditionEffectIndex.Invincible),
							new Taunt ("Entering this forbidden land comes with a toll.", "You do not seem worthy enough to enter these depths."),
							new TimedTransition (4000, "default3")
						),
						new State ("default3",
							new ConditionalEffect(ConditionEffectIndex.Invincible),
							new Taunt ("Stand back..Let me show you what I am made of...", "Flee. NOW! Before you end up another worthless spirit."),
							new NoPlayerWithinTransition (8, "startup")
						)
					),
					new State ("startup",
						new Taunt (true, "Power."),
						new SetAltTexture (0),
						new Shoot (10, 38, projectileIndex: 2, coolDown: 9999),
						new TimedTransition (4000, "spawnhelpers")
					),
					new State ("spawnhelpers",
						new InvisiToss ("Yazanahar Helper", 2, 0, coolDown: 9999999),
						new InvisiToss ("Yazanahar Helper", 2, 90, coolDown: 9999999),
						new InvisiToss ("Yazanahar Helper", 2, 180, coolDown: 9999999),
						new InvisiToss ("Yazanahar Helper", 2, 270, coolDown: 9999999),
						new Shoot (10, 14, shootAngle: 4, projectileIndex: 5, predictive: 2, coolDown: 2000),
						new TimedTransition (12000, "begin")
					),
					new State("begin",
						new Flash(0xFF0000, 1, 1),
						new TimedTransition(4000, "StartWaves1")
					),
					new State (
						new TimedTransition (8000, "WeirdMovement1"),
						new State ("StartWaves1",
							new Shoot (10, count: 7, shootAngle: 2, fixedAngle: 90, projectileIndex: 0, coolDown: 400),
							new Shoot (10, count: 7, shootAngle: 2, fixedAngle: 180, projectileIndex: 0, coolDown: 400),
							new Shoot (10, count: 7, shootAngle: 2, fixedAngle: 270, projectileIndex: 0, coolDown: 400),
							new Shoot (10, count: 7, shootAngle: 2, fixedAngle: 0, projectileIndex: 0, coolDown: 400),
							new TimedTransition (800, "StartWaves2")
						),
						new State ("StartWaves2",
							new Shoot (10, count: 7, shootAngle: 2, fixedAngle: 45, projectileIndex: 0, coolDown: 400),
							new Shoot (10, count: 7, shootAngle: 2, fixedAngle: 135, projectileIndex: 0, coolDown: 400),
							new Shoot (10, count: 7, shootAngle: 2, fixedAngle: 225, projectileIndex: 0, coolDown: 400),
							new Shoot (10, count: 7, shootAngle: 2, fixedAngle: 315, projectileIndex: 0, coolDown: 400),
							new TimedTransition (800, "StartWaves1")
						)
					),
					new State (
						new Shoot(10, 8, projectileIndex: 1, coolDown: 2000),
						new RemoveEntity(3, "Yazanahar Helper"),

						new State (
							new TimedTransition(24000, "Return"),
							new State ("WeirdMovement1",
								new Charge (2.5, range: 8, coolDown: 4600),
								new TimedTransition (4000, "Flash")
							),
							new State ("Flash",
								new BackAndForth(0.4, distance: 4),
								new Flash (0xFF0000, 1, 1),
								new TimedTransition (3000, "WeirdMovement2")
							),
							new State ("WeirdMovement2",
								new Grenade(1, 500, range: 8, coolDown: 1),
								new Shoot (10, count: 2, shootAngle: 18, projectileIndex: 3, coolDown: 1000),
								new Shoot (10, count: 8, shootAngle: 8, projectileIndex: 4, coolDown: 100),
								new TimedTransition (4000, "WeirdMovement1")
							)
						),
						new State(
							new ConditionalEffect (ConditionEffectIndex.Invincible),
							new State ("Return",
								new ReturnToSpawn(once: true, speed: 1.2),
								new TimedTransition (6000, "Reform")
							),
							new State ("Reform",
								new SetAltTexture (1),
								new InvisiToss ("Split Yazanahar", 2, 0, coolDown: 9999999),
								new InvisiToss ("Split Yazanahar", 2, 180, coolDown: 9999999),
								new InvisiToss ("Split Yazanahar", 2, 270, coolDown: 9999999),
								new TimedTransition (5000, "checker")
							),
							new State ("checker",
								new EntitiesNotExistsTransition(20, "Ongo", "Split Yazanahar")
							)
						),
						new State ("Ongo",
							new ConditionalEffect (ConditionEffectIndex.Armored),
							new SetAltTexture (0),
							new Prioritize(
								new StayCloseToSpawn(0.5, 3),
								new Wander(0.05)
							),
							new Grenade(2, 400, range: 8, coolDown: 200),
							new Shoot (10, 6, projectileIndex: 1, coolDown: 3000),
							new Shoot (10, 10, shootAngle: 10, projectileIndex: 5, coolDown: 4000),
							new Shoot (10, 5, shootAngle: 4, projectileIndex: 2, coolDown: 800),
							new TimedTransition (8000, "fast")
						),
						new State("fast",
							new Taunt("Your soul essence is a feast, {PLAYER}"),
							new ConditionalEffect(ConditionEffectIndex.Armored),
							new SetAltTexture(0),
							new Prioritize(
								new Orbit(0.7, 2, target:null)
							),
							new Shoot(10, 20, projectileIndex: 2, coolDown: 1000),
							new Shoot(10, 7, shootAngle: 12, predictive: 1, projectileIndex: 0, coolDown: 4000),
							new Shoot(10, 5, shootAngle: 4, projectileIndex: 2, coolDown: 800),
							new Shoot(10, 10, shootAngle: 4, projectileIndex: 4, coolDown: 1680),
							new TimedTransition(15000, "Return2")
						),
						new State("Return2",
							new Flash(0x000000, 2, 2),
							new ConditionalEffect(ConditionEffectIndex.Invincible),
							new ReturnToSpawn(once: true, speed: 1.2),
							new TimedTransition(6000, "DropDown")
						),
						new State("DropDown",
							new SpecificHeal(1, 1000, "Self", coolDown: 6000),
							new Shoot(10, 6, shootAngle: 22, projectileIndex: 6, coolDown: 800),
							new Shoot(10, 3, shootAngle: 8, projectileIndex: 2, coolDown: 200),
							new Shoot(10, 9, shootAngle: 8, projectileIndex: 4, predictive: 1, coolDown: 200, coolDownOffset: 400),
							new Taunt("You strange mortals think you have the bravery and courage of a true guardian?", "Cowards."),
							new ConditionalEffect(ConditionEffectIndex.Armored),
							new InvisiToss("Yazanahar Helper 2", 4, 0, coolDown: 9999999),
							new InvisiToss("Yazanahar Helper 2", 2, 45, coolDown: 9999999),
							new InvisiToss("Yazanahar Helper 2", 4, 90, coolDown: 9999999),
							new InvisiToss("Yazanahar Helper 2", 2, 135, coolDown: 9999999),
							new InvisiToss("Yazanahar Helper 2", 4, 180, coolDown: 9999999),
							new InvisiToss("Yazanahar Helper 2", 2, 225, coolDown: 9999999),
							new InvisiToss("Yazanahar Helper 2", 4, 270, coolDown: 9999999),
							new InvisiToss("Yazanahar Helper 2", 2, 315, coolDown: 9999999),
							new TimedTransition(24000, "tttt")
						),
						new State("tttt",
							new Order(6, "Yazanahar Helper 2", "Explode"),
							new Taunt("Havent died yet have you?!", "Your lives WILL be crushed!", "Forward you come, the graves you will be."),
							new ConditionalEffect(ConditionEffectIndex.Armored),
							new TimedTransition(4000, "swagche")
						),
						new State("swagche",
							new SpecificHeal(1, 800, "Self", coolDown: 1000),
							new Shoot(10, 24, projectileIndex: 5, coolDown: 3000),
							new Shoot(10, 4, projectileIndex: 4, predictive: 1, shootAngle: 12, coolDown: 1),
							new Shoot(10, 1, projectileIndex: 6, coolDown: 1),
							new Shoot(10, 1, projectileIndex: 6, coolDown: 1, coolDownOffset: 1),
							new TimedTransition(9000, "startup")
						),
						new State("death1",
							new Taunt(true, "..."),
							new Flash(0x000055, 1, 1),
							new ConditionalEffect(ConditionEffectIndex.Invincible),
							new RemoveEntity(20, "Yazanahar Helper"),
							new RemoveEntity(20, "Yazanahar Helper 2"),
							new ReturnToSpawn(once: true, speed: 2),
							new TimedTransition(6600, "death")
						),
						new State("death",
							new Suicide()
						)
					)
				),
				new MostDamagers(3,
					LootTemplates.RaidTokens()
				),
				new MostDamagers(3,
					new ItemLoot("Potion of Luck", 1.0),
					new ItemLoot("Greater Potion of Life", 1.0),
					new ItemLoot("Greater Potion of Mana", 1.0),
					new ItemLoot("Greater Potion of Vitality", 1.0),
					new ItemLoot("Greater Potion of Dexterity", 1.0),
					new ItemLoot("Greater Potion of Speed", 1.0),
					new ItemLoot("Greater Potion of Attack", 1.0),
					new ItemLoot("Greater Potion of Defense", 1.0),
					new ItemLoot("Greater Potion of Wisdom", 1.0),
					new ItemLoot("Overgrowth Lootbox", 1.0),
					new ItemLoot("Mayhem Lootbox", 1.0),
					new ItemLoot("Questing Package", 1.0),
					new ItemLoot("Onrane", 1.0)
				),
				new Threshold(0.010,
					new TierLoot(11, ItemType.Weapon, 0.1),
					new TierLoot(6, ItemType.Ability, 0.1),
					new TierLoot(11, ItemType.Armor, 0.1),
					new TierLoot(5, ItemType.Ring, 0.05),
					new TierLoot(12, ItemType.Armor, 0.05),
					new TierLoot(12, ItemType.Weapon, 0.05),
					new TierLoot(6, ItemType.Ring, 0.025),
					new ItemLoot("Mercy of Yazanahar", 0.0001),
					new ItemLoot("Exile's Resolve", 0.0001),
					new ItemLoot("Sor Crystal", 0.05),
					new ItemLoot("Xion Charge", 0.0001),
					new ItemLoot("Godslayer Sword", 0.01)
				)
			)

			.Init("Split Yazanahar",
				new State(
					new ConditionalEffect(ConditionEffectIndex.StasisImmune),
					new State("swag",
						new Prioritize(
							new StayCloseToSpawn(0.5, 3),
							new Wander(0.05)
						),
						new Shoot(10, count: 8, projectileIndex: 0, coolDown: 1400),
						new TimedTransition(8000, "time")
					),
					new State("time",
						new ConditionalEffect(ConditionEffectIndex.Invulnerable),
						new Shoot(10, count: 8, shootAngle: 6, projectileIndex: 1, coolDown: 1),
						new TimedTransition(4000, "swag")
					)
				)
			)
			.Init("Yazanahar Helper",
				new State(
					new ConditionalEffect(ConditionEffectIndex.StasisImmune),
					new ConditionalEffect (ConditionEffectIndex.Invincible),
					new State("swag",
						new Shoot(10, count: 1, projectileIndex: 0, coolDown: 1),
						new Shoot(10, count: 1, projectileIndex: 0, coolDown: 1, coolDownOffset: 1)
					)
				)
			)
			.Init("Yazanahar Helper 2",
				new State(
					new ConditionalEffect(ConditionEffectIndex.StasisImmune),
					new ConditionalEffect(ConditionEffectIndex.Invincible),
					new State("1",
						new Shoot(10, count: 1, fixedAngle: 0, projectileIndex: 0, coolDown: 1),
						new TimedTransition(4000, "2")
					),
					new State("2",
						new Shoot(10, count: 1, fixedAngle: 90, projectileIndex: 0, coolDown: 1),
						new TimedTransition(4000, "3")
					),
					new State("3",
						new Shoot(10, count: 1, fixedAngle: 180, projectileIndex: 0, coolDown: 1),
						new TimedTransition(4000, "4")
					),
					new State("4",
						new Shoot(10, count: 1, fixedAngle: 270, projectileIndex: 0, coolDown: 1),
						new TimedTransition(4000, "1")
					),

					new State("Explode",
						new Shoot(10, count: 12, projectileIndex: 0, coolDown: 99999),
						new Suicide()
					)
				)
			);
	}
}