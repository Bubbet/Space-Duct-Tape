using Assets.Scripts.Networking;
using BepInEx.Configuration;
using LaunchPadBooster.Networking;

namespace Space_Duct_Tape
{
    public class ConfigSync : ModNetworkMessage<ConfigSync>
    {
        public override void Deserialize(RocketBinaryReader reader)
        {
            var count = reader.ReadInt32();
            for (var i = 0; i < count; i++)
            {
                var section = reader.ReadString();
                var key = reader.ReadString();
                var type = (ConfigType)reader.ReadInt16();
                switch (type)
                {
                    case ConfigType.Bool:
                        ((ConfigEntry<bool>)Main.Instance.Config[section, key]).Value = reader.ReadBoolean();
                        break;
                    case ConfigType.Int:
                        ((ConfigEntry<int>)Main.Instance.Config[section, key]).Value = reader.ReadInt32();
                        break;
                    case ConfigType.Float:
                        ((ConfigEntry<float>)Main.Instance.Config[section, key]).Value = reader.ReadSingle();
                        break;
                    case ConfigType.String:
                        ((ConfigEntry<string>)Main.Instance.Config[section, key]).Value = reader.ReadString();
                        break;
                    default:
                        throw new System.NotImplementedException();
                }
            }
        }

        public override void Serialize(RocketBinaryWriter writer)
        {
            writer.WriteInt32(Main.Instance.Config.Count);
            foreach (var pair in Main.Instance.Config)
            {
                writer.WriteString(pair.Key.Section);
                writer.WriteString(pair.Key.Key);
                switch (pair.Value)
                {
                    case ConfigEntry<bool> v:
                        writer.WriteInt16((short) ConfigType.Bool);
                        writer.WriteBoolean(v.Value);
                        break;
                    case ConfigEntry<int> v:
                        writer.WriteInt16((short) ConfigType.Int);
                        writer.WriteInt32(v.Value);
                        break;
                    case ConfigEntry<float> v:
                        writer.WriteInt16((short) ConfigType.Float);
                        writer.WriteSingle(v.Value);
                        break;
                    case ConfigEntry<string> v:
                        writer.WriteInt16((short) ConfigType.String);
                        writer.WriteString(v.Value);
                        break;
                    default:
                        throw new System.NotImplementedException();
                }
            }
        }
    }

    public enum ConfigType
    {
        Bool,
        Int,
        Float,
        String,
    }
}