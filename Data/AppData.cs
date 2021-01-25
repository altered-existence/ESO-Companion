using DataAccessLibrary.Models;
using DataAccessLibrary.SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ESOCompanion.Data
{
    public class AppData
    {
        public static string role;
        private readonly IUserData _userData;
        private readonly ISQLiteDataAccess _companionDB;
        private readonly ICharacterData _characterData;
        private readonly IStyleData _styleData;
        public AppData(IUserData userData, ISQLiteDataAccess companionDB, ICharacterData characterData, IStyleData styleData, bool isRegistered = false)
        {
            _userData = userData;
            _companionDB = companionDB;
            this.isRegistered = isRegistered;
            _characterData = characterData;
            _styleData = styleData;
        }
        public DataFileModel DataFile { get; set; }
        private List<UserModel> _users { get; set; }
        public static UserModel loadedUser { get; set; }
        public List<CharacterModel> usersCharacters { get; set; }
        public static CharacterModel loadedCharacter { get; set; }
        public List<StyleModel> loadedCharacterStyles { get; set; }
        public bool isAdminLoggedIn { get; set; }
        public bool isRegistered { get; set; }
        public bool isLoggedIn { get; set; }
        public string defaultDatabasePath { get; set; } = "\\db\\";
        public string userDatabaseFile { get; set; } = "";
        public static string defaultDatabasePathSuffix { get; set; } = ".db;Version=3;";
        public async Task<List<UserModel>> GetUserList()
        {
            _users = await _userData.GetUsers();
            return _users;
        }
        public CharacterModel LoadCharacter(CharacterModel character)
        {
            loadedCharacter = character;
            return loadedCharacter;
        }
        public List<StyleModel> GetCharactersStyleList(CharacterModel character)
        {
            loadedCharacterStyles = loadedCharacter.charStyles;
            return loadedCharacterStyles;
        }
        #region Experimental
        public static void UnloadUser()
        {
            loadedUser = new UserModel
            {
                emailAddress = "",
                userName = "new",
                role = "new",
                userPassword = "password"
            };
        }
        public async Task CheckForFirstTimeUse()
        {
            // ----------------------------------------------------------
            // Further logic needed for: once path is received from user
            // ----------------------------------------------------------
            Console.WriteLine(_companionDB.connectionString);
            if (_companionDB.CreateDatabaseAndTable(_companionDB.UserConnectionString, false))
            {
                if (!_userData.CreateUsersTable().IsFaulted)
                {
                    await _userData.CreateDefaultUser();
                    await _characterData.CreateCharactersTable();
                    await _characterData.CreateDefaultCharacter();
                    await _styleData.CreateStylesTable();
                }
            }
        }
        #endregion
    }
}
