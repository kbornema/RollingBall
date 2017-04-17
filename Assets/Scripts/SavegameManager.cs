using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;

public sealed class SavegameManager : AManager<SavegameManager>
{
    public class Event : UnityEvent<Savegame> { }

    private const string DEBUG_SLOT_NAME = "default";

    private string GetFullFilepath(string fileName){ return string.Concat(folderPath, fileName, fileEnding); }
    private string folderPath { get { return Application.persistentDataPath + "/"; } }
    private string fileEnding { get { return ".sav"; } }

    private List<Savegame> allSavegames;
    private Savegame currentSavegame;

    [HideInInspector]
    public Event BeforeCurrentSavegameSave = new Event();

    protected override void OnAwake()
    {
        GetAllSaveGames();
        
        if (this.allSavegames.Count == 0)
        {
            allSavegames.Add(CreateNewSavegame(DEBUG_SLOT_NAME));
        }

        if (!ChangeCurrentSavegame(DEBUG_SLOT_NAME))
        {
            Debug.LogWarning("Could not load " + DEBUG_SLOT_NAME + " as default!");
        }

    }

    private bool ChangeCurrentSavegame(string savegameName)
    {
        for (int i = 0; i < allSavegames.Count; i++)
        {
            if(allSavegames[i].SlotName == savegameName)
            {
                currentSavegame = allSavegames[i];
                return true;
            }
        }

        return false;
    }

    private Savegame CreateNewSavegame(string slotName)
    {
        return CreateNewSavegame(new Savegame(slotName));
    }

    private Savegame CreateNewSavegame(Savegame saveGame)
    {
        SaveSavegame(saveGame);
        return saveGame;
    }

    private List<Savegame> GetAllSaveGames()
    {
        if (this.allSavegames != null)
            return this.allSavegames;

        else
        {
            this.allSavegames = new List<Savegame>();
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);

            FileInfo[] files = directoryInfo.GetFiles("*" + fileEnding);

            for (int i = 0; i < files.Length; i++)
            {
                Savegame tmpSaveGame = LoadSavegame(files[i].FullName);

                if (tmpSaveGame != null)
                    this.allSavegames.Add(tmpSaveGame);
            }
            
            return allSavegames;
        }
    }

    public bool SaveCurrentSavegame()
    {
        if (currentSavegame != null)
        {
            BeforeCurrentSavegameSave.Invoke(currentSavegame);
            return SaveSavegame(currentSavegame);
        }

        else
        {
            Debug.LogError("No Savegame loaded currently!");
            return false;
        }
    }

    public Savegame GetCurrentSavegame()
    {
        return currentSavegame;
    }

    private Savegame LoadSavegame(string path)
    {
        if (!File.Exists(path))
        {
            Debug.Log("File " + path + " does not exist!");
            return null;
        }

        else
        {
            Savegame saveGame = LoadSavegameBinary(path);

            return saveGame;
        }
    }

    private bool SaveSavegame(Savegame saveGame)
    {
        Debug.Assert(saveGame != null, "Savegame was null!");
        Debug.Assert(saveGame.SlotName != null);

        string fullFilePath = GetFullFilepath(saveGame.SlotName);

        return SaveSavegameBinary(saveGame, fullFilePath);

    }

    private static bool SaveSavegameBinary(Savegame saveGame, string fullFilePath)
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Create(fullFilePath);
            formatter.Serialize(file, saveGame);
            file.Close();

            //Debug.Log("Slot " + saveGame.SlotName + " saved! At " + fullFilePath);
            return true;
        }

        catch (System.Exception e)
        {
            Debug.LogError("Error saving slot: " + saveGame.SlotName + " to file!");
            Debug.Log(e.Message);
            return false;
        }
    }

    private static Savegame LoadSavegameBinary(string path)
    {

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            Savegame loadedSaveGame = (Savegame)formatter.Deserialize(file);
            file.Close();

            return loadedSaveGame;
        }

        catch (System.Exception e)
        {
            Debug.LogError("Error loading savegame at: " + path);
            Debug.LogError(e.Message);
            return null;
        }
    }
}
