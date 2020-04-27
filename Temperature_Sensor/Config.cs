using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Temperature_Sensor {
    public class ConfigFile<T> where T: new() {
        public string FileXml { get; }
        public T Data { get; set; }
        public string Name {
            get { return Path.GetFileName(FileXml).Split('.')[0]; }
        }

        public ConfigFile(string xml) {
            this.FileXml = xml;
        }
    }

    public class Config {
        private readonly BaseLib.Logger m_log;
        public ConfigFile<Setting> Setting { get; set; }

        public Config(BaseLib.Logger logger) {
            m_log = logger;
            Setting = new ConfigFile<Setting>(".\\Configs\\Setting.xml");
        }

        public void LoadConfigAll() {
            LoadConfig(Setting);
        }

        public void LoadConfig<T>(ConfigFile<T> config) where T : new() {
            try {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (FileStream reader = new FileStream(config.FileXml, FileMode.Open)) {
                    config.Data = (T)serializer.Deserialize(reader);
                    reader.Close();
                }
            } catch (Exception ex) {
                m_log.TraceError("Using default "+ config.Name +" because of failed to load, reason: " + ex.Message);
                config.Data = new T();
                throw new ApplicationException(config.Name + ": loading error");
            }
        }

        public void SaveConfig<T>(ConfigFile<T> config) where T : new() {
            if (config == null || config.Data == null) {
                throw new ArgumentNullException(nameof(config.Data));
            }
            try {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (TextWriter writer = new StreamWriter(config.FileXml)) {
                    xmlSerializer.Serialize(writer, config.Data);
                    writer.Close();
                }
            } catch (Exception ex) {
                m_log.TraceError("Save "+ config.Name +" error, reason: " + ex.Message);
                throw new ApplicationException(config.Name + ": saving error");
            }
        }
    }
}
