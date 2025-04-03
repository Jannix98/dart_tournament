using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Infrastructure.JSON
{
    internal static class PathManager
    {
        public static string GetAndCreatePath(string fileName)
        {
            string filePath = null;
            try
            {
                string basePath = GetBasePath();
                // Stelle sicher, dass das Verzeichnis existiert
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }

                // Vollständigen Dateipfad erstellen
                filePath = Path.Combine(basePath, fileName);

                // Datei erstellen, falls sie nicht existiert
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Erstellen der Datei: {ex.Message}");
            }

            return filePath;
        }

        private static string GetBasePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DartTournament", "data");
        }
    }
}
