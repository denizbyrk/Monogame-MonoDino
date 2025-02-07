using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MonoDino {
    public class Sounds {

        //set the volume level for sounds
        public static float volume = 1f;

        //create dictionaires to store sound effects and their instances
        private static Dictionary<string, SoundEffect> SoundEffects;
        public static Dictionary<string, SoundEffectInstance> SoundEffectInstances;

        //load sound effects and create instances for each
        public static void Load() {

            Sounds.SoundEffects = new Dictionary<string, SoundEffect> {

                {"die", Main.contentManager.Load<SoundEffect>("Assets\\die")},
                {"jump", Main.contentManager.Load<SoundEffect>("Assets\\jump")},
                {"point", Main.contentManager.Load<SoundEffect>("Assets\\point")}
            };

            Sounds.SoundEffectInstances = new Dictionary<string, SoundEffectInstance>();

            foreach (var s in Sounds.SoundEffects) {

                Sounds.SoundEffectInstances.Add(s.Key, s.Value.CreateInstance());
            }
        }

        //update the volume level of the sounds
        public static void Update() {

            Sounds.volume = MathHelper.Clamp(Sounds.volume, 0f, 1f);

            foreach (var s in Sounds.SoundEffectInstances) {

                s.Value.Volume = Sounds.volume;
            }
        }
    }
}