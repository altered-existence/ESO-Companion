using DataAccessLibrary.Models;
using DataAccessLibrary.SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESOCompanion.Data
{
    public class AppData
    {
        IUserData userData;
        ICharacterData characterData;
        IStyleData styleData;
        private List<UserModel> _users { get; set; }
        public static UserModel loadedUser { get; set; }
        public List<CharacterModel> usersCharacters { get; set; }
        public CharacterModel loadedCharacter { get; set; }
        public List<StyleModel> loadedCharacterStyles { get; set; }
        public bool isAdminLoggedIn { get; set; }
        public bool isRegistered { get; set; }
        public bool isLoggedIn { get; set; }
        public string defaultDatabasePath { get; set; } = "F:\\db\\";
        public string userDatabaseFile { get; set; }
        public string defaultDatabasePathSuffix { get; set; } = ";Version=3";
        public async Task<List<UserModel>> GetUserList()
        {
            _users = await userData.GetUsers();
            return _users;
        }
        public List<CharacterModel> GetUsersCharacterList(UserModel u)
        {
            usersCharacters = u.characters;
            return usersCharacters;
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
        public void UnloadUser()
        {
            loadedUser = new UserModel();
            usersCharacters = new List<CharacterModel>();
            loadedCharacter = new CharacterModel();
            loadedCharacterStyles = new List<StyleModel>();
            userDatabaseFile = "";
        }
        #endregion
    }
}