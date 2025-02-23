using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Godot;

public partial class JSONLoader : Node
{
    public static JSONLoader Instance {get; private set;}
    private Dictionary<string, string> _JSONFilesPathDict = new () {
        {"loading_screens_data", "res://Assets/JSONs/loading_screens_data.json"},
        {"random_texts", "res://Assets/JSONs/random_texts.json"},
        {"settings", "res://Assets/JSONs/settings.json"}
    };
    public override void _EnterTree(){
        if(Instance != null){
            QueueFree();
            return;
        }
        Instance = this;
    }
    private string ReadJSONFile(string JSONFile){
        string path = _JSONFilesPathDict[JSONFile];
		if(OS.HasFeature("editor")){
			path = ProjectSettings.GlobalizePath(path);
		}
		else{
			path = OS.GetExecutablePath().GetBaseDir().PathJoin(path.Remove(0, 6));
		}
        return File.ReadAllText(path);
    }
    public T DeserializeJSONElement<T>(string JSONFile, string rootProperty){
        string JSONText = ReadJSONFile(JSONFile);

        using JsonDocument doc = JsonDocument.Parse(JSONText);
        JsonElement root = doc.RootElement;
        JsonElement property = root.GetProperty(rootProperty);

        return JsonSerializer.Deserialize<T>(property.GetRawText());
    }
}