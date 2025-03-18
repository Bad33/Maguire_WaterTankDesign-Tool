using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace WaterTankTool_WFA.Designer_Notes
{
    public static class NotesManager
    {
        public static DesignerNotes Notes { get; set; } = new DesignerNotes();
        private static readonly string _notesFilePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "notes.json"
);

        // Load notes from the JSON file
        public static void LoadNotes()
        {
            if (File.Exists(_notesFilePath))
            {
                var json = File.ReadAllText(_notesFilePath);
                var loadedNotes = JsonConvert.DeserializeObject<DesignerNotes>(json);
                if (loadedNotes != null)
                {
                    Notes = loadedNotes;
                }
            }
        }

        // Save notes to the JSON file
        public static void SaveNotes()
        {
            var json = JsonConvert.SerializeObject(Notes, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_notesFilePath, json);
        }

    }
}
